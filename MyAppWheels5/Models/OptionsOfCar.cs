namespace MyAppWheels5
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OptionsOfCar")]
    public partial class OptionsOfCar
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NameInInglish { get; set; }

        [Required]
        [StringLength(50)]
        public string NameInRussion { get; set; }

        public bool IsChecked { get; set; }
    }

    public class OptionOfCarModel
    {
        public List<OptionsOfCar> OptionListOfCar { get; set; }
    }
}
