using System;

namespace AgileWorksProjekt.Server.Models
{
    public class Poordumine
    {
        public String Id { get; set; } = "0";
        public string Kirjeldus { get; set; } = "";
        public DateTime SisestamiseAeg { get; set; }
        public DateTime LahendamiseTähtaeg { get; set; }
    }
}