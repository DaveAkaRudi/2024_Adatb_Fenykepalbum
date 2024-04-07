using System.ComponentModel.DataAnnotations;

namespace PhotoApp.Models
{
    public class Orszag
    {
        [Key]
        public int id { get; set; }

        public string nev { get; set; } = String.Empty;
        [StringLength(3)]
        public string rovidites { get; set; } = String.Empty;
    }
}
