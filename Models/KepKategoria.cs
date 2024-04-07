using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp.Models
{
    public class KepKategoria
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("ReferencedKategoria")]
        public int kategoria_id { get; set; }
        [ForeignKey("ReferencedKep")]
        public int kep_id { get; set; }
        public virtual Kategoria? ReferencedKategoria { get; set; }
        public virtual Kep? ReferencedKep { get; set; }

    }
}
