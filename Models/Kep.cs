using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp.Models
{
    public class Kep
    {
        [Key]
        public int id { get; set; }

        public string cim { get; set; } = String.Empty;
        [Display(Name = "orszag id")]
        [ForeignKey("ReferencedOrszag")]
        public int orszag_id { get; set; } 
        public double ertekeles { get; set; } = 0;

        [ForeignKey("ReferencedAlbum")]
        public int album_id { get; set; }
        [ForeignKey("ReferencedFelhasznalo")]
        public int felhasz_id { get; set; }


        public string fenykeputvonal { get; set; } = String.Empty;

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public virtual Felhasznalo? ReferencedFelhasznalo { get; set; }
        public virtual Album? ReferencedAlbum { get; set; }
        public virtual Orszag? ReferencedOrszag { get; set; }

    }
}
