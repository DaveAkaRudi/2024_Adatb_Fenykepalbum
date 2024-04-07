using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp.Models
{
    public class Album
    {
        [Key]
        public int id { get; set; }
        public string cim { get; set; } = String.Empty;

        [ForeignKey("ReferencedFelhasznalo")]
        public int felhasz_id { get; set; }
        public virtual Felhasznalo? ReferencedFelhasznalo { get; set; }

    }
}
