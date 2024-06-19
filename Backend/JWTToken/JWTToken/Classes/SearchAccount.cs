using Lab13.models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using WebAppLab11.Classes;
using WebShablon2.Classes;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;

namespace JWTToken.Classes
{
    public class SearchAccount
    {
        protected string fullname;
        protected DateTime date;
        protected string pass;

        public SearchAccount(string fullname,DateTime time,string pass) {
            this.fullname = fullname;
            this.date = time;
            this.pass = pass;
        }

        public Person? SelectToDataBaseUser()
        {
            MySqlApplicationcs db = new();
            Person ps = new Person();
            FormatName manipulationName = new FormatName(fullname, date, pass);
            manipulationName.Сheck();
            manipulationName.AdapdationFormat();
            Formregistration autoriz = manipulationName.ReturnData();

            FormatPassword manipulationPass = new FormatPassword(fullname, date, pass);
            manipulationPass.Сheck();
            manipulationPass.AdapdationFormat();
            object? password = manipulationPass.ReturnPass();

            //autoriz.password = password.ToString();
            //MySqlParameter parametr = new MySqlParameter { ParameterName = "@n", Value = autoriz.name };
            //MySqlParameter parametr2 = new MySqlParameter { ParameterName = "@n2", Value = autoriz.lastname };
            //MySqlParameter parametr3 = new MySqlParameter { ParameterName = "@n3", Value = autoriz.firstname };
            //MySqlParameter parametr4 = new MySqlParameter { ParameterName = "@pass", Value = autoriz.password };
            int countUser = db.formregistration.FromSqlRaw("Select * from formregistration where name={0} and lastname={1} and firstname={2} and password = {3}", autoriz.name, autoriz.lastname, autoriz.firstname, password.ToString()).Count();
            if (countUser == 1)
            {
                Console.WriteLine("Кол-во выводов: "+ countUser);
                var dates = db.formregistration.FromSqlRaw("Select * from formregistration where name={0} and lastname={1} and firstname={2} and password = {3}", autoriz.name, autoriz.lastname, autoriz.firstname, password.ToString()).ToList();
                ps.Name = fullname;
                ps.date = dates[0].date;
                ps.Password = pass;
                Console.WriteLine("Записываем...");
                return ps;
            }
            else { return null; }
        }
    }
}
