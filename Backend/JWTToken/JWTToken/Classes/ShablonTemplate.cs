using Lab13.models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using WebAppLab11.Classes;

namespace WebShablon2.Classes
{
    public abstract class ManipulationDate
    {
        protected static string name = "";
        protected string fullname;
        protected DateTime date;
        protected string pass;

        public ManipulationDate(string fn, DateTime time, string pass)
        {
            fullname = fn;
            date = time;
            this.pass = pass;
        }

        public void Manipulate()
        {
            Сheck();
            AdapdationFormat();
            InsertToDataBase();
            GetDocument();
        }
        public abstract void Сheck();
        public abstract void AdapdationFormat();
        public virtual void InsertToDataBase()
        {
            Console.WriteLine("Сдаем выпускные экзамены");
        }
        public abstract void GetDocument();
    }

    public class FormatName : ManipulationDate
    {
        private string lastname = "";
        private string firstname = "";
        public FormatName(string fn, DateTime time, string pass) : base(fn, time, pass) { }

        public override void Сheck()
        {
            Console.WriteLine("Проверяем...");
            string[] test = fullname.Split(' ');
            if (test.Length == 3)
            {
                Console.WriteLine("Кол-во слов верное");
            }
        }
        public override void AdapdationFormat()
        {
            string[] test = fullname.Split(' ');
            Console.WriteLine("Форматируем...");
            name = test[0];
            lastname = test[1];
            firstname = test[2];
        }
        public override void InsertToDataBase()
        {
            try
            {
                using (MySqlApplicationcs db = new())
                {
                    //MySqlParameter parametr = new MySqlParameter ("@n", name);
                    //MySqlParameter parametr2 = new MySqlParameter ("@n2", lastname );
                    //MySqlParameter parametr3 = new MySqlParameter ("@n3", firstname );
                    int countUser = db.formregistration.FromSqlRaw("Select * from formregistration where name={0} and lastname={1} and firstname={2}", name, lastname, firstname).Count();
                    if (countUser == 0)
                    {
                        Formregistration reg = new Formregistration { name = name, lastname = lastname, firstname = firstname, date = date, password = null };
                        db.formregistration.AddRange(reg);
                        db.SaveChanges();
                    }
                    Console.WriteLine("Записываем...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            
        }
        public override void GetDocument()
        {
            Console.WriteLine("Документируем...");
            Console.WriteLine("Имя: " + name);
            Console.WriteLine("Фамилия: " + lastname);
            Console.WriteLine("Отчество: " + firstname);
        }
        public Formregistration ReturnDate()
        {
            return new Formregistration { firstname = firstname, name = name, lastname = lastname, password = null, date = new DateTime() };
        }
    }
    class FormatDate : ManipulationDate
    {
        private string OldDate;
        private string? NewDate;
        public FormatDate(string fn, DateTime time, string pass) : base(fn, time, pass)
        {
            OldDate = time.ToString();
        }
        public override void Сheck()
        {
            Console.WriteLine("Проверяем...");
        }

        public override void AdapdationFormat()
        {
            if (DateTime.TryParse(OldDate, out date))
            {
                NewDate = date.ToString("yyyy.MM.dd HH:mm:ss");
            }
            Console.WriteLine("Форматируем...");
        }

        public override void InsertToDataBase()
        {
            MySqlApplicationcs db = new();
            if (!string.IsNullOrEmpty(NewDate))
            {
                db.Database.ExecuteSqlRaw("UPDATE formregistration SET date={0} where name = {1}",NewDate,name);
                db.SaveChanges();
            }
            else
            {
                db.Database.ExecuteSqlRaw("UPDATE formregistration SET date={0} where name = {1}", OldDate, name);
                db.SaveChanges();
            }
            Console.WriteLine("Записываем...");
        }

        public override void GetDocument()
        {
            Console.WriteLine("Документируем...");
            if (!string.IsNullOrEmpty(NewDate))
            {
                Console.WriteLine("Дата: " + NewDate);
            }
            else
            {
                Console.WriteLine("Дата: " + OldDate);
            }
        }
    }

    class FormatPassword : ManipulationDate
    {
        bool isInt;
        int? NewPass;
        public FormatPassword(string fn, DateTime time, string pass) : base(fn, time, pass){}
        public override void Сheck()
        {
            Console.WriteLine("Проверяем...");
            isInt = pass.All(char.IsDigit);
        }
        public override void AdapdationFormat()
        {
            Console.WriteLine("Форматируем...");
            if (isInt)
                NewPass = Convert.ToInt32(pass);
            else
            {
                string[] numbers = Regex.Split(pass, @"\D+");
                for (int i = 0; i < numbers.Length; i++)
                    NewPass = Convert.ToInt32(numbers[i]);
            }
        }

        public override void InsertToDataBase()
        {
            MySqlApplicationcs db = new();
            db.Database.ExecuteSqlRaw("UPDATE formregistration SET password={0} where name = {1}", NewPass, name);
            db.SaveChanges();
            Console.WriteLine("Записываем...");
        }
        public override void GetDocument()
        {
            Console.WriteLine("Документируем...");
            Console.WriteLine("Пароль: " + NewPass);
        }
        public int? ReturnPass()
        {
            return NewPass;
        }
    }
}
