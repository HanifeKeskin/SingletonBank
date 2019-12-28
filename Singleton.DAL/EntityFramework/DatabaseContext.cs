using Singleton.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.DAL.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Musteri> Musteris { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hesap>().MapToStoredProcedures();
        }
        public DbSet<Hesap> Hesaps { get; set; }
        public DbSet<Banka> Bankas { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Kredi> Kredis { get; set; }
        public DbSet<HGS> Hgs { get; set; }
        public DbSet<HgsSatis> HgsSatis { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}
