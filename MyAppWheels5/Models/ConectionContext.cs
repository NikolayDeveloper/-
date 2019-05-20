namespace MyAppWheels5
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ConectionContext : DbContext
    {
        public ConectionContext()
            : base("name=database_P_conection")
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ImageOfCar> ImageOfCars { get; set; }
        public virtual DbSet<listOfVehicle> listOfVehicles { get; set; }
        public virtual DbSet<Markka> Markkas { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<OptionsOfCar> OptionsOfCars { get; set; }
        public virtual DbSet<RemarkedOptionsOfCar> RemarkedOptionsOfCars { get; set; }
        public virtual DbSet<TypeOfVehicle> TypeOfVehicles { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<GetListOfVehicle> GetListOfVehicles { get; set; }
        public virtual DbSet<OptionsOfTruck> OptionsOfTrucks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<listOfVehicle>()
                .HasMany(e => e.ImageOfCars)
                .WithRequired(e => e.listOfVehicle)
                .HasForeignKey(e => e.IdlistOfVehicle);

            modelBuilder.Entity<City>()
                .HasMany(e => e.listOfVehicles)
                .WithRequired(e => e.City)
                .HasForeignKey(e => e.IdCity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Markka>()
                .HasMany(e => e.listOfVehicles)
                .WithRequired(e => e.Markka)
                .HasForeignKey(e => e.IdMarkka);

            modelBuilder.Entity<Markka>()
                .HasMany(e => e.Models)
                .WithOptional(e => e.Markka)
                .HasForeignKey(e => e.IdMarkka);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.listOfVehicles)
                .WithRequired(e => e.Model)
                .HasForeignKey(e => e.IdModel);

            modelBuilder.Entity<TypeOfVehicle>()
                .HasMany(e => e.listOfVehicles)
                .WithRequired(e => e.TypeOfVehicle)
                .HasForeignKey(e => e.IdTypeOfVehicle)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeOfVehicle>()
                .HasMany(e => e.Markkas)
                .WithOptional(e => e.TypeOfVehicle)
                .HasForeignKey(e => e.IdTypeOfVehicle);

        }
    }
}
