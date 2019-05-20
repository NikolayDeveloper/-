namespace MyAppWheels5
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImageOfCar")]
    public partial class ImageOfCar
    {
        public int Id { get; set; }

        public byte[] ImageData { get; set; }

        public int IdlistOfVehicle { get; set; }

        public virtual listOfVehicle listOfVehicle { get; set; }
    }
}
