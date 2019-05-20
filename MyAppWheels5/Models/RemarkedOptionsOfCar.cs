namespace MyAppWheels5
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RemarkedOptionsOfCar")]
    public partial class RemarkedOptionsOfCar
    {
        public int Id { get; set; }

        public bool? GUR { get; set; }

        public bool? ABSS { get; set; }

        public bool? SRS { get; set; }

        public bool? WinterMode { get; set; }

        public bool? TurboNaduv { get; set; }

        public bool? TurboTimer { get; set; }

        public bool? Signalization { get; set; }

        public bool? AutoZapusk { get; set; }

        public bool? Immobilazer { get; set; }

        public bool? FullElectroPaket { get; set; }

        public bool? CenerLatch { get; set; }

        public bool? Konditioner { get; set; }

        public bool? ClimetControl { get; set; }

        public bool? KruizControl { get; set; }

        public bool? NavigationSystem { get; set; }

        public bool? ParkTronik { get; set; }

        public int IdListOfVehicle { get; set; }

       
    }
}
