using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }


        [Required]
        [StringLength(14,ErrorMessage = "The identity No must be 14 characters long",MinimumLength =14)]
        [Display(Name = "National ID")]
        public string NationalID { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(14, ErrorMessage = "The identity No must be 14 characters long", MinimumLength = 14)]
        [MinLength(10,ErrorMessage ="Invalid Phone Number")]
        [Display(Name = "Phone Number")]
        public string Mobile { get; set; }



        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        public byte Gender { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Weight")]
        public double? Weight { get; set; }

        [Display(Name = "Height")]
        public double? Height { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
