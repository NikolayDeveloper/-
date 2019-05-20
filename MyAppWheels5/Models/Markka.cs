namespace MyAppWheels5
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Markka")]
    public partial class Markka
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Markka()
        {
            listOfVehicles = new HashSet<listOfVehicle>();
            Models = new HashSet<Model>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NameMarkka { get; set; }

        public int? IdTypeOfVehicle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<listOfVehicle> listOfVehicles { get; set; }

        public virtual TypeOfVehicle TypeOfVehicle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Model> Models { get; set; }
    }
}
