using System.ComponentModel.DataAnnotations;

namespace PhotoApp.Models
{
    public class Kategoria
    {
        [Key]
        public int id { get; set; }
        public string nev { get; set; } = String.Empty;

    }
}
