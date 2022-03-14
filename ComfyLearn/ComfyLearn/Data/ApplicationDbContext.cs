using ComfyLearn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComfyLearn.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Kategorija> Kategorija { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Jezik> Jezik { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Clanak> Clanak { get; set; }
        public DbSet<ClanakKomentari> BlogKomentari { get; set; }
        public DbSet<ClanakLike> ClanakLike { get; set; }
        public DbSet<Certifikat> Certifikat { get; set; }
        public DbSet<CourseComments> CourseComments { get; set; }
        public DbSet<FAQ> FAQ { get; set; }
        public DbSet<Korpa> Korpa { get; set; }
        public DbSet<MaterialKurs> MaterialKurs { get; set; }
        public DbSet<Obavijest> Obavijest { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<Podkategorija> Podkategorija { get; set; }
        public DbSet<Akcija> Akcija { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<KupljenaKorpa> KupljenaKorpa { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "df7c9574-3c77-414b-ae26-80ed8c65b119",
                    ConcurrencyStamp = "0c1bed3e-18df-4d0e-afba-3c404028a62e",
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = "1ffaffdc-be81-4a6e-92a6-775d396ff869",
                    ConcurrencyStamp = "f39541c9-8e57-4c5b-babf-f28b5f824ceb",
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                }
            );

            builder.Entity<Jezik>().HasData(new List<Jezik>()
            {
                new Jezik
                {
                    Id=1,
                    Naziv="Bosanski"
                },
                new Jezik
                {
                    Id=2,
                    Naziv="English"
                }
            });
            builder.Entity<Kategorija>().HasData(new List<Kategorija>()
            {
                new Kategorija
                {
                    Id=1,
                    Naziv="IT & Software"
                },
                new Kategorija
                {
                    Id=2,
                    Naziv="Design"
                }
            });
            builder.Entity<Podkategorija>().HasData(new List<Podkategorija>()
            {
                new Podkategorija
                {
                    Id=1,
                    KategorijaId=1,
                    Naziv="Network & Security"
                },
                new Podkategorija
                {
                    Id=2,
                    KategorijaId=1,
                    Naziv="Hardware"
                }
            });

        }
        public DbSet<ComfyLearn.Models.TipObavijesti> TipObavijesti { get; set; }
    }
}
