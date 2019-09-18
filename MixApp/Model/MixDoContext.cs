using MixApp.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixApp.Model
{
    public class MixDoContext : DbContext
    {
        // yapıcı method
        public MixDoContext() : base("DefCon") // Ana tabloda DefCon olarak adlandırılan alandan çek.
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MixDoContext, Configuration>("DefCon")); // Update işlemi sırasında; sil tekrar oluştur yapmadan, farklılığı bulup sadece o farklılığı güncelliyor.
        }
        public DbSet<Cariler> Carilers { get; set; } // Cariler class'ını db üzerinde tabloya dönüştürüyor.
        public DbSet<UrunGruplari> UrunGruplaris { get; set; }
        public DbSet<Renkler> Renklers{ get; set; }
        public DbSet<Bedenler> Bedenlers{ get; set; }
        public DbSet<Sezonlar> Sezonlars{ get; set; }
        public DbSet<Markalar> Markalars{ get; set; }
        public DbSet<Urunler> Urunlers{ get; set; }

    }
}
