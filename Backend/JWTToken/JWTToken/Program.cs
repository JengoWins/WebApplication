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


var builder = WebApplication.CreateBuilder();
builder.Logging.ClearProviders();   // ������� ��� ����������
builder.Logging.AddConsole();   // ��������� ��������� ��� ������������ �� �������
builder.Services.AddAuthorization();
builder.Services.AddCors(); // ��������� ������� CORS
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
// ����������� CORS
app.UseCors(builder => builder.AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader());

app.UseAuthorization();
app.UseAuthentication();

app.MapGet("/ListShip", () =>
{
    MySqlApplicationcs db = new();
    var data = db.Ships.ToList();
    return Results.Json(data);
});

app.MapGet("/ListBasket", () =>
{
    MySqlApplicationcs db = new();
    var basket = db.Basket.ToList();
    app.Logger.LogInformation("������...");
    List<Ships> ship = new List<Ships>();
    for (int i = 0; i < basket.Count; i++)
    {
        ship.Add(db.Ships.FirstOrDefault(p => p.id == basket[i].id_Ship));
    }
    return Results.Json(ship);
});

app.MapPost("/AddShipBasket", async (string? returnUrl, HttpContext context) =>
{
    app.Logger.LogInformation("������...");
    MySqlApplicationcs db = new();
    var form = await context.Request.ReadFromJsonAsync<Ships>();
    Ships? ship = db.Ships.FirstOrDefault(p => p.name == form!.name);
    app.Logger.LogInformation("��� �������: " + form!.name);
    Basket bass = new Basket { id_Ship=ship!.id };
    var data = db.Basket.Add(bass);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapDelete("/DeleteShipBasket", async (string? returnUrl, HttpContext context) =>
{
    app.Logger.LogInformation("������...");
    MySqlApplicationcs db = new();
    var form = await context.Request.ReadFromJsonAsync<Ships>();
    Ships? ship = db.Ships.FirstOrDefault(p => p.name == form!.name);
    
    Basket bass = db.Basket.FirstOrDefault(p => p.id_Ship == ship.id);

    app.Logger.LogInformation("��� �������: " + form!.name + "ID �������: "+ bass.id_Ship); 
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
    app.Logger.LogInformation("������...");
    var form = await context.Request.ReadFromJsonAsync<Person>();
    app.Logger.LogInformation("Name: " + form.Name + " Pass: " + form.Password);
    SearchAccount account = new SearchAccount(form.Name,DateTime.Now,form.Password);
    Person? person = account.SelectToDataBaseUser();
    if (person is null)
        return Results.BadRequest();

    app.Logger.LogInformation("Name: " + person.Name + " Pass: " + person.Password +" Date: " + person.date);
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Name) };
    // ������� ������ ClaimsIdentity
    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    var jwt = new JwtSecurityToken(
    issuer: AuthOptions.ISSUER,
    audience: AuthOptions.AUDIENCE,
    claims: claims,
    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),  // �������� ������ �������� ����� 2 ������
    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    app.Logger.LogInformation(encodedJwt);

    //context.Response.Cookies.Append("X-Access-Token", encodedJwt);
    return Results.Ok(new ModelToken { access_token = encodedJwt, username = person.Name, date = person.date });
});

app.MapPost("/RouteRegistration", async (context) => {

    //var response = context.Response;
    var request = context.Request;
    var message = "������������ ������";   // ���������� ��������� �� ���������
    var person = await request.ReadFromJsonAsync<Person>();
    try
    {
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
    }
    catch
    {
        Console.WriteLine("������ ��������� ������");
    }
    await Task.CompletedTask;
});

app.Map("", async (context) => await context.Response.WriteAsync("Hello World!"));

app.Run();

