using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using WebApp.Models;
using WebApp.Models.Enums;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;
using WebApp.Providers;
using WebApp.Results;

namespace WebApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        public IUnitOfWork UnitOfWork { get; set; }

        public AccountController()
        {
        }

        public AccountController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
		
        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // POST api/Account/Logout     
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetUserType/{username}")]
        public string GetUserType(string username)
        {
            ApplicationUser user = UserManager.FindByName(username);
            if(user == null)
            {
                return "";
            }
            else
            {
                return user.UserType;
            }
        }

        [HttpPost]
        [Authorize(Roles = "AppUser")]
        [Route("PostKartaRegistrovani")]
        public IHttpActionResult PostKartaRegistrovani(KartaBindingModel karta)
        {
            ApplicationUser user = UserManager.FindByName(karta.Korisnik);
            if (user == null)
            {
                return BadRequest("Korisnik ne postoji.");
            }

            if (!user.UserType.Equals(karta.TipPopusta) && karta.TipPopusta != "Regular")
            {
                return BadRequest("Korisniku nije dozvoljeno da uzme kartu datog tipa");
            }

            if (!karta.TipPopusta.Equals("Regular") && !user.IsVerified.ToString().Equals("Prihvacen"))
            {
                return BadRequest("Korisniku nije validiran od strane kontrolora.");
            }

            List<StavkaCenovnika> stavke = UnitOfWork.Cenovnici.GetAktuelanCenovnik();
            StavkaCenovnika stavka = stavke.FirstOrDefault(item => item.TipKarte.VrstaKarte.ToString().Equals(karta.TipKarte) && item.TipPopusta.VrstaPopusta.ToString().Equals(karta.TipPopusta));
            Karta novaKarta = new Karta()
            {
                DatumIzdavanja = DateTime.Now,
                Validna = true,
                Izbrisano = false,
                Korisnik = karta.Korisnik,
                StavkaCenovnika = stavka
               
            };

            try
            {
                UnitOfWork.Karte.Add(novaKarta);
                UnitOfWork.Complete();
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest("Desila se greska prilikom kupovine karte.");
            }
           
        }

        [HttpGet]
        [Route("GetAllUsersForValidation")]
        [Authorize(Roles = "Controller")]
        public List<RegisterBindingModel> GetAllUsersForValidation()
        {
            List<RegisterBindingModel> retVal = UserManager.Users.Where(x=> x.ImgUrl != null && !x.UserType.Equals("Regular") && !x.Izbrisano).
                Select(y => new RegisterBindingModel()
                {
                    Name = y.Name,
                    Surname = y.Surname,
                    Username = y.UserName,
                    UserType = y.UserType,
                    Address = y.Address,
                    DateOfBirth = y.DateOfBirth,
                    Email = y.Email,
                    Password = y.PasswordHash,
                    ImgUrl = y.ImgUrl,
                    IsVerified = y.IsVerified.ToString()
                }).ToList();
            return retVal;
        }


        [HttpPut]
        [Route("ValidateUser")]
        [Authorize(Roles = "Controller")]
        public IHttpActionResult ValidateUser(ValidateUserBindingModel userToValidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = UserManager.FindByName(userToValidate.Username);

            if (user == null)
            {
                return BadRequest("Korisnik sa datim username-om ne postoji.");
            }

            
            user.IsVerified = userToValidate.Status;

            try
            {
                UserManager.Update(user);
                UnitOfWork.Complete();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }


            MailMessage mail = new MailMessage("gulegjsp@gmail.com", user.Email);
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential("gulegjsp@gmail.com", "Gulice123!");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            mail.Subject = "Zahtev za popust - GJSP Novi Sad";
            mail.Body = $"Vas zahtev za {user.UserType.ToString()} popust je {user.IsVerified.ToString()}. {Environment.NewLine} Reason: {Environment.NewLine} {userToValidate.Reason} {Environment.NewLine} {Environment.NewLine} Svako dobro, {Environment.NewLine} GJSP Novi Sad";
            try
            {
                client.Send(mail);
            }
            catch (Exception e)
            {

            }
            return Ok();


        }

        [AllowAnonymous]
        [Route("GetUser")]
        public RegisterBindingModel GetUser(string username)
        {
            RegisterBindingModel retVal = null;
            ApplicationUser user = UserManager.FindByName(username);
            if(user != null)
            {
                retVal = new RegisterBindingModel()
                {
                    Username = user.UserName,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    UserType = user.UserType.ToString(),
                    IsVerified = user.IsVerified.ToString(),
                    ImgUrl = user.ImgUrl
                };
            }
      

            return retVal;
        }
        [HttpPut]
        [Authorize(Roles = "AppUser")]
        [Route("EditUser")]
        public IHttpActionResult EditUser(RegisterBindingModel user)
        {
            string username = user.OldUsername;
            ApplicationUser userCheck = UserManager.FindByName(user.OldUsername);
            if(!ApplicationUser.VerifyHashedPassword(userCheck.PasswordHash, user.Password))
            {
                return BadRequest("Pogresna lozinka.Neuspesna izmena profila.");
            }

            if(!user.Username.Equals(username))
            {
                ApplicationUser temp = UserManager.FindByName(user.Username);
                if (temp != null)
                {
                    return BadRequest("Username vec postoji!");
                }
                else
                {
                    ApplicationUser stariUser = UserManager.FindByName(username);
                    if(stariUser != null)
                    {
                        if(stariUser.UserType != user.UserType)
                        {
                            user.IsVerified = "ProcesiraSe";
                            user.ImgUrl = "";
                        }
                    }
                    temp = new ApplicationUser()
                    {
                        Id = user.Username,
                        UserName = user.Username,
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        Address = user.Address,
                        DateOfBirth = user.DateOfBirth,
                        UserType = user.UserType,
                        IsVerified = (StatusZahteva)Enum.Parse(typeof(StatusZahteva), user.IsVerified),
                        ImgUrl = user.ImgUrl,
                        PasswordHash = stariUser.PasswordHash
                    };

					IdentityResult result = UserManager.Delete(stariUser);

                    IdentityResult result2 = UserManager.Create(temp);
                    IdentityResult roleResult = UserManager.AddToRole(temp.UserName, "AppUser");
                    UnitOfWork.Complete();
                    if (!result.Succeeded || !result2.Succeeded || !roleResult.Succeeded)
                    {
                        return GetErrorResult(result);
                    }
                    return Ok();
                }
            }
            else
            {
                ApplicationUser stariUser = UserManager.FindByName(user.Username);
                if (stariUser != null)
                {
                    //IdentityResult result = await UserManager.DeleteAsync(temp);

                    if (stariUser.UserType != user.UserType)
                    {
                        user.IsVerified = "ProcesiraSe";
                        user.ImgUrl = "";
                    }

					stariUser.Id = user.Username;
					stariUser.UserName = user.Username;
					stariUser.Name = user.Name;
					stariUser.Surname = user.Surname;
					stariUser.Email = user.Email;
					stariUser.Address = user.Address;
					stariUser.DateOfBirth = user.DateOfBirth;
					stariUser.UserType = user.UserType;
					stariUser.IsVerified = (StatusZahteva)Enum.Parse(typeof(StatusZahteva), user.IsVerified);
					stariUser.ImgUrl = user.ImgUrl;
					stariUser.PasswordHash = stariUser.PasswordHash;

					IdentityResult result = UserManager.Update(stariUser);
                    //IdentityResult roleResult = await UserManager.AddToRoleAsync(temp.UserName, "AppUser");
                    //UnitOfWork.Complete();

                    if (!result.Succeeded )
                    {
                        return GetErrorResult(result);
                    }
                    return Ok();
                }
                return BadRequest("Greska prilikom izmene profila.");
            }

            
        }

        [HttpPost]
        [Authorize(Roles = "AppUser")]
        [Route("DeactivateMyProfil")]
        public IHttpActionResult DeactivateMyProfil(UserPassModel userPass)
        {
            try
            {
                string username = userPass.Username;
                string password = userPass.Password;
                var user = UserManager.FindByName(username);
                if (user == null)
                {
                    return BadRequest("Korisnik sa datim username-om ne postoji.");
                }
                string sifra = ApplicationUser.HashPassword(password);
               
                if (!ApplicationUser.VerifyHashedPassword(user.PasswordHash, password))
                {
                    return BadRequest("Pogresna lozinka! Neuspesna deaktivacija profila.");
                }

                // user.Izbrisano = true;
                // UserManager.Update(user);

                UserManager.Delete(user);
                return Ok();
            }
            catch
            {
                return BadRequest("Neuspesna deaktivacija profila.");
            }

        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result = null;
            try
            {
                result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                    model.NewPassword);
            }
            catch(Exception e)
            {
                return BadRequest("Greska prilikom izmene lozinke");
                throw e;
            }
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [HttpPost]
        [Route("UploadImage/{username}")]
        [AllowAnonymous]
        public IHttpActionResult UploadImage(string username)
        {
            var httpRequest = HttpContext.Current.Request;

            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {

                        ApplicationUser user = UserManager.FindByName(username);

                        if(user == null)
                        {
                            return BadRequest("Greska prilikom uploada slike");
                        }

                            //IdentityResult result = UserManager.Delete(user);

                            //if (!result.Succeeded)
                            //{
                            //    return BadRequest("Greska prilikom uploada slike");
                            //}

                            var postedFile = httpRequest.Files[file];
                            string fileName = username + "_" + postedFile.FileName;
                            var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + fileName);


                            user.ImgUrl = fileName;
                            user.IsVerified = StatusZahteva.ProcesiraSe;

                            postedFile.SaveAs(filePath);
                            IdentityResult result = UserManager.Update(user);
                            //UnitOfWork.Complete();
                            if (!result.Succeeded)
                            {
                                return BadRequest("Greska prilikom uploada slike");
                            }
                        }

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("DownloadImage/{username}")]
        public IHttpActionResult DownloadPicture(string username)
        {

            ApplicationUser user = UserManager.FindByName(username);

            if (user == null)
            {
                return BadRequest("User doesn't exists.");
            }

            if (user.ImgUrl == null)
            {
                return BadRequest("Image doesn't exists.");
            }


            var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + user.ImgUrl);

            FileInfo fileInfo = new FileInfo(filePath);
            string type = fileInfo.Extension.Split('.')[1];
            byte[] data = new byte[fileInfo.Length];

            HttpResponseMessage response = new HttpResponseMessage();
            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ByteArrayContent(data);
                response.Content.Headers.ContentLength = data.Length;

            }

            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/png");

            return Ok(data);


        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                
                 ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			var user = new ApplicationUser()
			{
				UserName = model.Username,
				Name = model.Name,
				Surname = model.Surname,
				Email = model.Email,
				PasswordHash = ApplicationUser.HashPassword(model.Password),
				Address = model.Address,
				DateOfBirth = model.DateOfBirth ,
				UserType = model.UserType,
				ImgUrl = model.ImgUrl,
				IsVerified = StatusZahteva.ProcesiraSe,
				Id = model.Username,
				EmailConfirmed = false
			};

			IdentityResult result = await UserManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
			}
			else
			{
				ApplicationUser currentUser = UserManager.FindByName(user.UserName);

				IdentityResult roleResult = await UserManager.AddToRoleAsync(currentUser.Id, "AppUser");
                //UnitOfWork.Complete();
				if (!roleResult.Succeeded)
				{
					return GetErrorResult(roleResult);
				}
			}

            return Ok();
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result); 
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
