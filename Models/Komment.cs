using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp.Models
{
    public class Komment
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("ReferencedFelhasznalo")]
        public int felhasz_id { get; set; }
        [ForeignKey("ReferencedKep")]
        public int kep_id { get; set; }

        public string megjegyzes { get; set; } = String.Empty;
        public virtual Felhasznalo? ReferencedFelhasznalo { get; set; }
        public virtual Kep? ReferencedKep { get; set; }

    }
}
