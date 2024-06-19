using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text.Json.Nodes;

namespace Lab13.models
{
    public class Person
    {
        public string Name { get; set; }
        public DateTime date { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }

        public byte[] Photo { get; set; }

        public Person() {
            Name = "";
            date = new DateTime();
            //Photo = new JsonValue();
            Password = "password";
            Phone = "";
            City = "";
        }
    }
    public class Photos
    {
        public Blob blob { get; set; }
    }
}
