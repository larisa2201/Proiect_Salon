using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SalonModel
{
    public partial class SalonEntitiesModel : DbContext
    {
        public SalonEntitiesModel()
            : base("name=SalonEntitiesModel")
        {
        }

        public virtual DbSet<Angajati> Angajatis { get; set; }
        public virtual DbSet<Clienti> Clientis { get; set; }
        public virtual DbSet<Facturi> Facturis { get; set; }
        public virtual DbSet<Programari> Programaris { get; set; }
        public virtual DbSet<Servicii> Serviciis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Angajati>()
                .Property(e => e.Nume)
                .IsFixedLength();

            modelBuilder.Entity<Angajati>()
                .Property(e => e.Prenume)
                .IsFixedLength();

            modelBuilder.Entity<Angajati>()
                .Property(e => e.Telefon)
                .IsFixedLength();

            modelBuilder.Entity<Angajati>()
                .Property(e => e.E_mail)
                .IsFixedLength();

            modelBuilder.Entity<Clienti>()
                .Property(e => e.Nume)
                .IsFixedLength();

            modelBuilder.Entity<Clienti>()
                .Property(e => e.Prenume)
                .IsFixedLength();

            modelBuilder.Entity<Clienti>()
                .Property(e => e.Telefon)
                .IsFixedLength();

            modelBuilder.Entity<Clienti>()
                .Property(e => e.E_mail)
                .IsFixedLength();

            modelBuilder.Entity<Clienti>()
                .Property(e => e.Parola)
                .IsFixedLength();

            modelBuilder.Entity<Facturi>()
                .Property(e => e.Total)
                .HasPrecision(18, 0);

           

            modelBuilder.Entity<Programari>()
                .Property(e => e.Id_client)
                .IsFixedLength();

            modelBuilder.Entity<Programari>()
                .Property(e => e.Id_angajat)
                .IsFixedLength();

            modelBuilder.Entity<Programari>()
                .Property(e => e.Id_serviciu)
                .IsFixedLength();

            modelBuilder.Entity<Servicii>()
                .Property(e => e.Denumire)
                .IsFixedLength();

            modelBuilder.Entity<Servicii>()
                .Property(e => e.Pret)
                .HasPrecision(18, 0);
        }
    }
}
