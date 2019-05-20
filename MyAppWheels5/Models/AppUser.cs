namespace MyAppWheels5
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppUser")]
    public partial class AppUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "Please, write down email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Uncurrect email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please,write down password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Please, write down firstname ")]
        [DisplayName("Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please, write down email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Uncurrect email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please,write down password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Please,write down confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match")] 
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
