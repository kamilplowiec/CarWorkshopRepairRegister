namespace CarWorkshopRepairRegister
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Baza : DbContext
    {
        public Baza()
            : base("name=Baza")
        {
        }

        public virtual DbSet<Osoba> Osoba { get; set; }
        public virtual DbSet<Pojazd> Pojazd { get; set; }
        public virtual DbSet<Pracownik> Pracownik { get; set; }
        public virtual DbSet<RejestrNapraw> RejestrNapraw { get; set; }
        public virtual DbSet<RejestrNaprawUslugi> RejestrNaprawUslugi { get; set; }
        public virtual DbSet<Usluga> Usluga { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
