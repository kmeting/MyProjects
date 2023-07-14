using AracKiralama.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralama.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Arac> Araclar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Marka> Markalar { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<Satis> Satislar { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(LocalDB)\MSSQLLocalDB; database=AracKiralama;integrated security=True;MultipleActiveResultSets=True;");

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marka>().Property(m => m.Adi).IsRequired().HasColumnType("varchar(50)");
            modelBuilder.Entity<Rol>().Property(m => m.Adi).IsRequired().HasColumnType("varchar(50)");
            modelBuilder.Entity<Rol>().HasData(new Rol { 
            Id=1,
            Adi="Admin"
            });

            //modelBuilder.Entity<Kullanici>()

            modelBuilder.Entity<Rol>().HasData(new Rol()
            {
                Id = 2,
                Adi = "Admin"
            });

            modelBuilder.Entity<Kullanici>().HasData(new Kullanici
            {
                Id=1,
                Adi = "Admin",
                AktifMi = true,
                EklenmeTarihi = DateTime.Now,
                Email = "admin@otokiralama.tc",
                KullaniciAdi = "admin",
                Sifre = "123456",
                //Rol = new Rol { Id = 1, Adi = "Admin" },
                RolId = 1,
                Soyadi="admin",
                Telefon="0850",
                
            }); 

            
            base.OnModelCreating(modelBuilder);
        }
    }
}
