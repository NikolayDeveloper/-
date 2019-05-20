namespace MyAppWheels5
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Model")]
    public partial class Model
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Model()
        {
            listOfVehicles = new HashSet<listOfVehicle>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NameModel { get; set; }

        public int? IdMarkka { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<listOfVehicle> listOfVehicles { get; set; }

        public virtual Markka Markka { get; set; }
    }
}
