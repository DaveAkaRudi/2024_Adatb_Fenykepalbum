using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PhotoApp.Models;
using System.Xml.Linq;

namespace PhotoApp.Context
{
    public class EFContext:DbContext
    {
        public EFContext()
        {
            //: base("EFContext");
        }

        public DbSet<Album> albumok { get; set; }
        public DbSet<Felhasznalo> felhasznalok { get; set; }
        public DbSet<Kategoria> kategoriak { get; set; }
        public DbSet<Kep> kepek { get; set; }
        public DbSet<Komment> kommentek { get; set; }
        public DbSet<Palyazat> palyazatok { get; set; }
        public DbSet<Orszag> orszagok { get; set; }

        public DbSet<KepKategoria>? KepKategoria { get; set; }

        private void MisiDBConfig(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(@"User Id=LAJOS;Password=lajos;Data Source=localhost:1521/XEPDB1");
        }

        private void RajmundDBConfig(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(@"User Id=system;Password=admin;Data Source=192.168.0.199:1521/XEPDB1");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             MisiDBConfig(optionsBuilder);
            //RajmundDBConfig(optionsBuilder);
        }

    }
}
