using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp.Models
{
    public class Palyazat
    {
        [Key]
        public int id { get; set; }
        public string nev { get; set; } = String.Empty;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime hatarido { get; set; } 
        public string leiras { get; set; } = String.Empty;
        public string nyertes { get; set; }

        [ForeignKey("ReferencedKategoria")]
        public int kategoria_id { get; set; }
        public virtual Kategoria? ReferencedKategoria { get; set; }


    }
}
