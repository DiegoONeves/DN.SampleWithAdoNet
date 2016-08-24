using System.ComponentModel.DataAnnotations;

namespace DN.SampleWithAdoNet.Web.Models
{
    public class UserNewViewModel
    {
        [Required]
        public string Name { get; set; }
        [Display(Name = "E-mail")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}