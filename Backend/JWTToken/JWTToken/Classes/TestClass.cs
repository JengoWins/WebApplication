using Lab13.models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppLab11.Classes
{
    public class Formregistration
    {
        public int id { get; set; }
        public required string name { get; set; }
        public required string lastname { get; set; }
        public required string firstname { get; set; }
        public required DateTime date { get; set; }

        public byte[] photo { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public required string? password { get; set; }
    }
    public class Ships
    {
      public int id {  get; set; }
      public string name { get; set; }
      public int Health { get; set; }
      public int Speed { get; set; }
      public string flexibility { get; set; }
      public int TeamCrew { get; set; }
      public int HeavyWeapon { get; set; }
      public int MediumWeapon { get; set; }
      public int LightWeapon { get; set; }
      public int price { get; set; }
      public string imgWay { get; set; }
        //public List<Basket> Baskets { get; set; }
    }

    public class Weapons
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int Speed { get; set; }
        public int distance {get; set; }
        public int EffectiveRange { get; set; }
        public int damage { get; set; }
        public int price { get; set; }
    }


    public class Crew
    {
        public int id { get; set; }
        public string name { get; set; }
        public int Shooting { get; set; }
        public int leadership { get; set; }
        public int navigation { get; set; }
        public int Mechanics { get; set; }
        public int Rigging { get; set; }
        public int Tracking { get; set; }
        public int Battle { get; set; }
        public int price { get; set; }

    }

    public class Basket
    {
        public int id { get; set; }
        [ForeignKey("id_Ship")]
        public int id_Ship { get; set; }
        [ForeignKey("id_User")]
        public int id_User { get; set; }
    }

    public class BasketAdd
    {
        public string nameShip { get; set; }
        public string nameUser { get; set; }
    }

    public class BasketInGame
    {
        public int PriceTotal { get; set; }
        public string nameUser { get; set; }
    }

    public class ModelToken
    {
        public string access_token { get; set; }
        public string username { get; set; }
        public DateTime date { get; set; }
    }

}