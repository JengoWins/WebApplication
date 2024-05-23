using System.ComponentModel.DataAnnotations.Schema;

namespace Lab13.models
{
    public class Person
    {
        public string Name { get; set; }
        public DateTime date { get; set; }
        public string Password { get; set; }
       
        public Person() {
            Name = "";
            date = new DateTime();
            Password = "password";
        }
    }
}
