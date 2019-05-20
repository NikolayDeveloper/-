namespace MyAppWheels5
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("listOfVehicle")]
    public partial class listOfVehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public listOfVehicle()
        {
            ImageOfCars = new HashSet<ImageOfCar>();
        }

        public int Id { get; set; }

        public int IdTypeOfVehicle { get; set; }

        public int IdMarkka { get; set; }

        public int IdModel { get; set; }

        public int IdCity { get; set; }

        public int YearOfCar { get; set; }

        [StringLength(1000)]
        public string Discribe { get; set; }

        public int Price { get; set; }

        [StringLength(2000)]
        public string OptionOfVehicle { get; set; }

        public int UserId { get; set; }

        [StringLength(50)]
        public string OptionOfTruck { get; set; }

        public DateTime DateAdvert { get; set; }

        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImageOfCar> ImageOfCars { get; set; }

        public virtual Markka Markka { get; set; }

        public virtual Model Model { get; set; }

        public virtual TypeOfVehicle TypeOfVehicle { get; set; }
    }
}
