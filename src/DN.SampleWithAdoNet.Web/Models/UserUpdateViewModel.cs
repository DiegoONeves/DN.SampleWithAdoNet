using System;
using System.ComponentModel.DataAnnotations;

namespace DN.SampleWithAdoNet.Web.Models
{
    public class UserUpdateViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "E-mail")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}