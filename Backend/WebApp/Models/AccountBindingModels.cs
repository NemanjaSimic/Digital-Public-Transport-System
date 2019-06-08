using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using WebApp.Models.Enums;

namespace WebApp.Models
{
	// Models used as parameters to AccountController actions.

	public class NoviCenovnikBindingModel
	{
		public float Vremenska { get; set; }
		public float Dnevna { get; set; }
		public float Mesecna { get; set; }
		public float Godisnja { get; set; }
		public DateTime Do { get; set; }
		
	}

	public class AddExternalLoginBindingModel
	{
		[Required]
		[Display(Name = "External access token")]
		public string ExternalAccessToken { get; set; }
	}

	public class StavkaBindingModel
	{
		public string VrstaKarte { get; set; }
		public string VrstaPopusta { get; set; }
		public float Cena { get; set; }
	}
	
    public class StanicaBindingModel
    {
        [Required]
        [Display(Name = "Naziv")]
        public string Naziv { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

	public class RegisterBindingModel
	{
		[Required]
		[Display(Name = "Username")]
		public string Username { get; set; }

		[Required]
		[Display(Name = "Name")]
		public string Name { get; set; }
		[Required]
		[Display(Name = "Surname")]
		public string Surname { get; set; }

		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[Required]
		[Display(Name = "Address")]
		public string Address { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[Display(Name = "DateOfBirth")]
		public DateTime DateOfBirth { get; set; }

		[Required]
		[Display(Name = "UserType")]
		public string UserType { get; set; }

		[Display(Name = "ImgUrl")]
		public string ImgUrl { get; set; }

    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
