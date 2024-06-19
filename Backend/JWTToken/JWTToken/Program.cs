using JWTToken.Classes;
using Lab13.models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAppLab11.Classes;
using WebShablon2.Classes;
using System.Linq.Dynamic.Core;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Text;


var builder = WebApplication.CreateBuilder();
builder.Logging.ClearProviders();   // удаляем все провайдеры
builder.Logging.AddConsole();   // добавляем провайдер для логгирования на консоль
builder.Services.AddAuthorization();
builder.Services.AddCors(); // добавляем сервисы CORS
var connecctionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MySqlApplicationcs>(options => options.UseMySql(connecctionString, ServerVersion.AutoDetect(connecctionString)));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
// настраиваем CORS
app.UseCors(builder => builder.AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/ListShip", () =>
{
    MySqlApplicationcs db = new();
    var data = db.Ships.ToList();
    return Results.Json(data);
});

app.MapGet("/ListBasket/{name}", (string name) =>
{
    MySqlApplicationcs db = new();
    
    app.Logger.LogInformation("Начало...");
    List<Ships> ship = new List<Ships>();
    FormatName shablon = new FormatName(name, new DateTime(), "");
    shablon.Сheck();
    shablon.AdapdationFormat();
    Formregistration preAutoriz = shablon.ReturnData();
    Formregistration Autoriz = db.formregistration.FirstOrDefault(p => p.firstname == preAutoriz.firstname && p.lastname == preAutoriz.lastname && p.name == preAutoriz.name);
    var basket = db.Basket.FromSqlRaw("SELECT * FROM basket where id_User = "+Autoriz.id).ToList();
    for (int i = 0; i < basket.Count; i++)
    {
        ship.Add(db.Ships.FirstOrDefault(p => p.id == basket[i].id_Ship));
    }
    return Results.Json(ship);
});

app.MapGet("/PersonProfile/{name}", (string name) =>
{
    MySqlApplicationcs db = new();

    app.Logger.LogInformation("Начало...");
    Person person = new Person();
    FormatName shablon = new FormatName(name, new DateTime(), "");
    shablon.Сheck();
    shablon.AdapdationFormat();
    Formregistration preAutoriz = shablon.ReturnData();
    Formregistration Autoriz = db.formregistration.FirstOrDefault(p => p.firstname == preAutoriz.firstname && p.lastname == preAutoriz.lastname && p.name == preAutoriz.name);
    var user = db.Basket.FromSqlRaw("SELECT * FROM basket where id_User = " + Autoriz.id).ToList();
    person = new Person { Name = name,date = Autoriz.date,Phone = Autoriz.phone,City=Autoriz.city,Photo = Autoriz.photo };
    return Results.Json(person);
});

app.MapPost("/AddShipBasket", async (string? returnUrl, HttpContext context) =>
{
    app.Logger.LogInformation("Начало...");
    MySqlApplicationcs db = new();
    var form = await context.Request.ReadFromJsonAsync<BasketAdd>();
    Ships? ship = db.Ships.FirstOrDefault(p => p.name == form!.nameShip);
    app.Logger.LogInformation("Имя Корабля: " + form!.nameShip);
    FormatName shablon = new FormatName(form.nameUser, new DateTime(),"");
    shablon.Сheck();
    shablon.AdapdationFormat();
    Formregistration preAutoriz = shablon.ReturnData();
    Formregistration Autoriz = db.formregistration.FirstOrDefault(p => p.firstname == preAutoriz.firstname && p.lastname==preAutoriz.lastname && p.name == preAutoriz.name);

    Basket bass = new Basket { id_Ship=ship!.id,id_User=Autoriz!.id };
    var data = db.Basket.Add(bass);
    await db.SaveChangesAsync();
    return Results.Ok();
});

//ShipBasketInGame

app.MapDelete("/ShipBasketInGame", async (string? returnUrl, HttpContext context) =>
{
    app.Logger.LogInformation("Начало...");
    MySqlApplicationcs db = new();
    var form = await context.Request.ReadFromJsonAsync<BasketInGame>();
    //Ships? ship = db.Ships.FirstOrDefault(p => p.name == form!.nameShip);
    FormatName shablon = new FormatName(form.nameUser, new DateTime(), "");
    shablon.Сheck();
    shablon.AdapdationFormat();
    Formregistration preAutoriz = shablon.ReturnData();
    Formregistration Autoriz = db.formregistration.FirstOrDefault(p => p.firstname == preAutoriz.firstname && p.lastname == preAutoriz.lastname && p.name == preAutoriz.name);
    Basket bass = new Basket { id_Ship = 0, id_User = Autoriz!.id };

    app.Logger.LogInformation("Общая цена кораблей: " + form!.PriceTotal + "ID корабля: " + bass.id_Ship);
    //db.Basket.Remove(bass);
    db.Database.ExecuteSqlRaw("DELETE FROM basket WHERE id_User={0}", bass.id_User);
    db.Database.ExecuteSqlRaw("Update formregistration Set wallet=wallet-{0} WHERE id={1}", form.PriceTotal, bass.id_User);
    await db.SaveChangesAsync();
    return Results.Ok();
});


app.MapDelete("/DeleteShipBasket", async (string? returnUrl, HttpContext context) =>
{
    app.Logger.LogInformation("Начало...");
    MySqlApplicationcs db = new();
    var form = await context.Request.ReadFromJsonAsync<BasketAdd>();
    Ships? ship = db.Ships.FirstOrDefault(p => p.name == form!.nameShip);
    FormatName shablon = new FormatName(form.nameUser, new DateTime(), "");
    shablon.Сheck();
    shablon.AdapdationFormat();
    Formregistration preAutoriz = shablon.ReturnData();
    Formregistration Autoriz = db.formregistration.FirstOrDefault(p => p.firstname == preAutoriz.firstname && p.lastname == preAutoriz.lastname && p.name == preAutoriz.name);
    Basket bass = new Basket { id_Ship = ship!.id, id_User = Autoriz!.id };

    app.Logger.LogInformation("Имя Корабля: " + form!.nameShip + "ID корабля: "+ bass.id_Ship); 
    db.Basket.Remove(bass);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/ListCrew", () =>
{
    MySqlApplicationcs db = new();
    var data = db.Crew.ToList();
    return Results.Json(data);
});

app.MapGet("/ListWeapons", () =>
{
    MySqlApplicationcs db = new();
    var data = db.Weapons.ToList();
    return Results.Json(data);
});

app.MapPost("/RouteProfile", async (string? returnUrl, HttpContext context) =>
{
    app.Logger.LogInformation("Начало...");
    var form = await context.Request.ReadFromJsonAsync<Person>();
    app.Logger.LogInformation("Name: " + form.Name + " Pass: " + form.Password);
    SearchAccount account = new SearchAccount(form.Name,DateTime.Now,form.Password);
    Person? person = account.SelectToDataBaseUser();
    if (person is null)
        return Results.BadRequest();

    app.Logger.LogInformation("Name: " + person.Name + " Pass: " + person.Password +" Date: " + person.date);
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Name) };
    // создаем объект ClaimsIdentity
    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    var jwt = new JwtSecurityToken(
    issuer: AuthOptions.ISSUER,
    audience: AuthOptions.AUDIENCE,
    claims: claims,
    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),  // действие токена истекает через 2 минуты
    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    app.Logger.LogInformation(encodedJwt);

    //context.Response.Cookies.Append("X-Access-Token", encodedJwt);
    return Results.Ok(new ModelToken { access_token = encodedJwt, username = person.Name, date = person.date });
});

object? Remix = "";

app.MapPost("/RouteRegistration", async (context) => {

    //var response = context.Response;
    var request = context.Request;
    var message = "Некорректные данные";   // содержание сообщения по умолчанию
    var person = await request.ReadFromJsonAsync<Person>();
    //Person person = System.Text.Json.JsonSerializer.Deserialize<Person>(request.Body);
  
        if (person != null)
        {
            message = $"Name: {person.Name}  Password: {person.Password}";
            app.Logger.LogInformation(message);
        }
        
        ManipulationDate shablon = new FormatName(person.Name, person.date, person.Password);
        shablon.Manipulate();
        shablon = new FormatDate(person.Name, person.date, person.Password);
        shablon.Manipulate();
        shablon = new FormatPassword(person.Name, person.date, person.Password);
        shablon.Manipulate();
        string[] test = person.Name.Split(' ');
        Console.WriteLine("Телефон: "+person.Phone);
        //Console.WriteLine("Blooob: " + person.Photo);
        //string FilePath = person.Photo.ToString().Remove(0,5);
        Remix = test[0];
        using (MySqlApplicationcs db = new())
        {
            int countUser = db.formregistration.FromSqlRaw("Select * from formregistration where name={0} and lastname={1} and firstname={2}", test[0], test[1], test[2]).Count();
            if (countUser == 1)
            {
                Console.WriteLine(test[0]);
                db.Database.ExecuteSqlRaw("UPDATE formregistration SET phone={0},city={1} WHERE name = {2}", person.Phone, person.City, test[0]);
                db.SaveChanges();
                Console.WriteLine("Записываем Обновление сигнала...");
            }
            
        }
    await Task.CompletedTask;
});

app.MapPost("/RouteRegistrationFile", async (context) => {
    var request = context.Request;
    using StreamReader reader = new StreamReader(request.Body);
    string name = await reader.ReadToEndAsync();
    byte[] bytes = Encoding.ASCII.GetBytes(name);
    //Console.WriteLine(name);

    using (MySqlApplicationcs db = new())
    {
        db.Database.ExecuteSqlRaw("UPDATE formregistration SET photo={0} WHERE name = {1}", bytes, Remix.ToString());            
        db.SaveChanges();
        Console.WriteLine("Записываем Обновление сигнала...");
    }
    
    await Task.CompletedTask;
});

app.Map("", async (context) => await context.Response.WriteAsync("Hello World!"));

app.Run();

