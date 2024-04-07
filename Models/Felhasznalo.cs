using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace PhotoApp.Models
{

    public class Felhasznalo
    {
        public enum Role
        {
            User, 
            Admin,
        }
        [Key]
        public int id { get; set; }

        [MinLength(6)]
        [MaxLength(32)]
        public string nev { get; set; } = String.Empty;

        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])", ErrorMessage = "Wrong format!")]
        public string email { get; set; } = String.Empty;

        [MinLength(6)]
        [MaxLength(32)]
        public string jelszo { get; set; } = String.Empty;

        [Display(Name = "szuletes datuma")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime szuletes_datuma { get; set; }

        public Role role { get; set; }= Role.User;

        [NotMapped]
        public bool rememberMe { get; set; }
    }
}
