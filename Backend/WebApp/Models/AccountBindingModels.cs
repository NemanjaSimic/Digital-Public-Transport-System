using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using WebApp.Models.Enums;

namespace WebApp.Models
{
	// Models used as parameters to AccountController actions.
	public class StanicaBindingModel
	{
		[Required]
		public string Naziv { get; set; }
		[Required]
		public string Adresa { get; set; }
		[Required]
		public double X { get; set; }
		[Required]
		public double Y { get; set; }
	}


	public class NovaLinijaBindingModel
	{
		[Required]
		public string Ime { get; set; }
		[Required]
		public int RedniBroj { get; set; }
		[Required]
		public string VrstaLinije { get; set; }
		public List<string> RadniDanTermini { get; set; }
		public List<string> SubotaTermini { get; set; }
		public List<string> NedeljaTermini { get; set; }
		public List<string> Stanice { get; set; }
	}
	public class NoviCenovnikBindingModel
	{
		[Required]
		public float Vremenska { get; set; }
		[Required]
		public float Dnevna { get; set; }
		[Required]
		public float Mesecna { get; set; }
		[Required]
		public float Godisnja { get; set; }
		[Required]
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
		[Required]
		public string VrstaKarte { get; set; }
		[Required]
		public string VrstaPopusta { get; set; }
		[Required]
		public float Cena { get; set; }
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

        [Display(Name = "IsVerified")]
        public string IsVerified { get; set; }
        [Display(Name = "OldUsername")]
        public string OldUsername { get; set; }

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
