namespace MyAppWheels5
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GetListOfVehicle")]
    public partial class GetListOfVehicle
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdTypeOfVehicle { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdMarkka { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdModel { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCity { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int YearOfCar { get; set; }

        [StringLength(1000)]
        public string Discribe { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Price { get; set; }

        [StringLength(2000)]
        public string OptionOfVehicle { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [StringLength(50)]
        public string OptionOfTruck { get; set; }

        [Key]
        [Column(Order = 8)]
        public DateTime DateAdvert { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(50)]
        public string NameVehicle { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(50)]
        public string NameMarkka { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(50)]
        public string NameModel { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(50)]
        public string NameCity { get; set; }
    }
}
