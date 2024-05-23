using MySqlConnector;
using System.Data;

namespace WebShablon2.Classes
{
    public class MyDBApplicationcs
    {
        protected string data; //Настройка подключения к бд
        protected MySqlConnection conn; //Подключение к бд
        public static MyDBApplicationcs? MySqlSource; //Подключение к бд

        public MyDBApplicationcs()
        {
            data = "";
            string generatePort = "Server=" + "localhost" + ";Database=" + "dbkurenkov" + ";port=" + "3306" + ";User=" + "root" + ";password=" + "SaraParker206";
            conn = new MySqlConnection(generatePort);
            Console.WriteLine("Подключение к Базе Данных...");
        }

        public static MyDBApplicationcs? Connection()
        {
            if (MySqlSource == null)
                MySqlSource = new MyDBApplicationcs();
            return MySqlSource;
        }

        public void InsertToName(string n,string l, string f)
        {
            DBOpen();
            data = "Insert Into formregistration (name,lastname,firstname) values (@n,@l,@f)";
            MySqlCommand command = new MySqlCommand(data, conn);
            command.Parameters.Add("@n", MySqlDbType.VarChar).Value = n;
            command.Parameters.Add("@l", MySqlDbType.VarChar).Value = l;
            command.Parameters.Add("@f", MySqlDbType.VarChar).Value = f;
            command.ExecuteNonQuery();
            DBExit();
        }

        public void InsertToDate(string n, string time)
        {
            DBOpen();
            data = "UPDATE formregistration SET date=@d where name = @n";
            MySqlCommand command = new MySqlCommand(data, conn);
            command.Parameters.Add("@n", MySqlDbType.DateTime).Value = n;
            command.Parameters.Add("@d", MySqlDbType.DateTime).Value = time;
            command.ExecuteNonQuery();
            DBExit();
        }

        public void InsertToPassword(string n, int? p)
        {
            DBOpen();
            data = "UPDATE formregistration SET password=@p where name = @n";
            MySqlCommand command = new MySqlCommand(data, conn);
            command.Parameters.Add("@n", MySqlDbType.Int64).Value = n;
            command.Parameters.Add("@p", MySqlDbType.Int64).Value = p;
            command.ExecuteNonQuery();
            DBExit();
        }

        public int SelectToUser(string n,string n2,string n3)
        {
            int count = 0;
            DBOpen();
            data = "Select Count(*) from formregistration where name=@n and lastname=@n2 and firstname=@n3;";
            MySqlCommand command = new MySqlCommand(data, conn);
            command.Parameters.Add("@n", MySqlDbType.VarChar).Value = n;
            command.Parameters.Add("@n2", MySqlDbType.VarChar).Value = n2;
            command.Parameters.Add("@n3", MySqlDbType.VarChar).Value = n3;
            count = Convert.ToInt32(command.ExecuteScalar());
            //command.ExecuteNonQuery();
            DBExit();
            return count;
        }
        public int SelectToUser(string n, string n2, string n3,int? password)
        {
            int count = 0;
            if (password != null)
            {
                DBOpen();
                data = "Select Count(*) from formregistration where name=@n and lastname=@n2 and firstname=@n3 and password = @pass;";
                MySqlCommand command = new MySqlCommand(data, conn);
                command.Parameters.Add("@n", MySqlDbType.VarChar).Value = n;
                command.Parameters.Add("@n2", MySqlDbType.VarChar).Value = n2;
                command.Parameters.Add("@n3", MySqlDbType.VarChar).Value = n3;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
                count = Convert.ToInt32(command.ExecuteScalar());
                //command.ExecuteNonQuery();
                DBExit();
            }
            return count;
        }

        public DateTime SelectUserProfileDate(string n, string n2, string n3, int? password)
        {
            DateTime dt = DateTime.Now;
            if (password != null)
            {
                DBOpen();
                data = "Select date from formregistration where name=@n and lastname=@n2 and firstname=@n3 and password = @pass;";
                MySqlCommand command = new MySqlCommand(data, conn);
                command.Parameters.Add("@n", MySqlDbType.VarChar).Value = n;
                command.Parameters.Add("@n2", MySqlDbType.VarChar).Value = n2;
                command.Parameters.Add("@n3", MySqlDbType.VarChar).Value = n3;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
                DBExit();
                dt = (DateTime)table.Rows[0][0];
                
            }
            return dt;
        }
        //Методы управлением потоком
        private void DBOpen()
        {
            conn.Open();
        }
        private void DBExit()
        {
            conn.Close();
        }
    }
}
