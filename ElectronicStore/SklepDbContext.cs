using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ElectronicStore
{
    /// <summary>
    /// Klasa reprezentująca kontekst bazy danych dla sklepu elektronicznego.
    /// </summary>
    public class SklepDbContext : DbContext
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy.
        /// </summary>
        /// <param name="options">Opcje konfiguracji kontekstu bazy danych.</param>
        public SklepDbContext(DbContextOptions<SklepDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// Konfiguruje model danych dla kontekstu bazy danych.
        /// </summary>
        /// <param name="builder">Konfigurator modelu.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Konfiguracja relacji jeden-do-wielu pomiędzy sklepem a produktami na stanie.
            builder.Entity<Sklep>()
                   .HasMany(s => s.ProduktyNaStanie)
                   .WithOne();

            // Konfiguracja relacji jeden-do-wielu pomiędzy sklepem a pracownikami.
            builder.Entity<Sklep>()
                   .HasMany(s => s.Pracownicy)
                   .WithOne();
        }

        /// <summary>
        /// Zestaw danych reprezentujący sklepy w bazie danych.
        /// </summary>
        public DbSet<Sklep> Sklepy { get; set; }

        /// <summary>
        /// Zestaw danych reprezentujący produkty w bazie danych.
        /// </summary>
        public DbSet<Produkt> Produkty { get; set; }

        /// <summary>
        /// Zestaw danych reprezentujący pracowników w bazie danych.
        /// </summary>
        public DbSet<Pracownik> Pracownicy { get; set; }

        /// <summary>
        /// Zestaw danych reprezentujący laptopy w bazie danych.
        /// </summary>
        public DbSet<Laptop> Laptops { get; set; }

        /// <summary>
        /// Zestaw danych reprezentujący smartfony w bazie danych.
        /// </summary>
        public DbSet<Smartphone> Smartphones { get; set; }
    }
}
