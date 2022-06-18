using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Clinic.Web.Models;
using System.IO;
using Clinic.BLL.VM;

namespace Clinic.Web.Controllers
{
    [Authorize]
    public class AccountController : DefaultController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userobj = _comonBLL.Get_AspNetUsers_List()
                .Where(a => a.UserName == model.Email || a.PhoneNumber == model.Email).FirstOrDefault();

            if (userobj != null && userobj.Email != null)
            {
                model.Email = userobj.UserName;
            }
            else
            {
                userobj = _comonBLL.Get_AspNetUsers_List().Where(a => a.PhoneNumber == model.Email).FirstOrDefault();
                if (userobj != null && userobj.Email != null)
                {
                    model.Email = userobj.UserName;
                }
                else
                {
                    var patient = _patientBLL.Get_Patient_List(null, model.Email, null, null, null,null,null).FirstOrDefault();
                    if (patient != null && !string.IsNullOrEmpty(patient.Patient_UserId))
                    {
                        userobj = _comonBLL.Get_AspNetUsers_List().Where(a => a.Id == patient.Patient_UserId).FirstOrDefault();
                        if (userobj != null && userobj.Email != null)
                        {
                            model.Email = userobj.UserName;
                        }
                    }
                    else
                    {
                        var doctor = _doctorBLL.Get_Doctor_List(null, model.Email, null, null, null).FirstOrDefault();
                        if (doctor != null && !string.IsNullOrEmpty(doctor.Doctor_UserId))
                        {
                            userobj = _comonBLL.Get_AspNetUsers_List().Where(a => a.Id == doctor.Doctor_UserId).FirstOrDefault();
                            if (userobj != null && userobj.Email != null)
                            {
                                model.Email = userobj.UserName;
                            }
                        }
                        else
                        {
                            var employee = _adminBLL.Get_Employee_List(null, model.Email, null, null, null).FirstOrDefault();
                            if (employee != null && !string.IsNullOrEmpty(employee.Admin_UserId))
                            {
                                userobj = _comonBLL.Get_AspNetUsers_List().Where(a => a.Id == employee.Admin_UserId).FirstOrDefault();
                                if (userobj != null && userobj.Email != null)
                                {
                                    model.Email = userobj.UserName;
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "Invalid Username .");
                                return View(model);
                            }
                        }
                    }
                }

            }
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:

                    try
                    {
                        var user = await UserManager.FindAsync(model.Email, model.Password);

                        var roles = UserManager.GetRoles(user.Id);
                        if (roles.Count > 0)
                        {
                            string roleName = roles[0].ToString();

                            if (roleName == "Patient")
                            {
                                return RedirectToLocal(Url.Action("Dashboard", "Patient"));
                            }

                            else if (roleName == "Doctor")
                            {
                                return RedirectToLocal(Url.Action("Dashboard", "Doctor"));
                            }

                            else if (roleName == "Employee")
                            {
                                return RedirectToLocal(Url.Action("Dashboard", "Admin"));
                            }
                            else
                            {
                                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                                return RedirectToLocal(returnUrl);
                            }
                        }
                        else
                        {
                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                            return RedirectToLocal(returnUrl);
                        }
                    }
                    catch (Exception ex)
                    {
                        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        return RedirectToLocal(returnUrl);
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }

        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.BlodGroups = _comonBLL.Get_BlodGroups_Data(null);
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                #region Validate user

                var oldData = _patientBLL.Get_Patient_List(null, null, null, model.Email, null,null,null);
                var oldUserData = _comonBLL.Get_AspNetUsers_List().FirstOrDefault(a => a.Email == model.Email);
                if ((oldData != null && oldData.Count > 0) || oldUserData != null)
                {
                    ModelState.AddModelError("Email", "Email is taken.");
                    ViewBag.BlodGroups = _comonBLL.Get_BlodGroups_Data(null);
                    return View(model);
                }
                else
                {
                    oldData = _patientBLL.Get_Patient_List(null, null, model.Mobile, null, null, null, null);
                    oldUserData = _comonBLL.Get_AspNetUsers_List().FirstOrDefault(a => a.PhoneNumber == model.Mobile);
                    if ((oldData != null && oldData.Count > 0) || oldUserData != null)
                    {
                        ModelState.AddModelError("Mobile", "Mobile is taken.");
                        ViewBag.BlodGroups = _comonBLL.Get_BlodGroups_Data(null);
                        return View(model);
                    }
                    else
                    {
                        oldData = _patientBLL.Get_Patient_List(null, model.NationalID, null, null, null, null, null);
                        if ((oldData != null && oldData.Count > 0))
                        {
                            ModelState.AddModelError("NationalID", "National ID is taken.");
                            ViewBag.BlodGroups = _comonBLL.Get_BlodGroups_Data(null);
                            return View(model);
                        }
                    }
                }

                #endregion


                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.Mobile, LockoutEnabled = false };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    var resultRole = UserManager.AddToRole(user.Id, "Patient"); 

                    #region Image

                    if (model.Image != null)
                    {
                        string FileName = Path.GetFileNameWithoutExtension(model.Image.FileName);
                        string Exten = Path.GetExtension(model.Image.FileName);
                        FileName = FileName + Guid.NewGuid() + Exten;
                        model.ImagePath = FileName;
                        model.Image.SaveAs(Path.Combine(Server.MapPath("~/Uploads/ProfileImage/Patient/"), FileName));
                    }
                    #endregion

                    var newPatient = new PatientVM
                    {
                        BlodGroupId = model.BlodGroupId,
                        PatientAddress = model.Address,
                        PatientDateOfBirth = model.DateOfBirth,
                        PatientEmail = model.Email,
                        PatientGender = model.Gender,
                        PatientHeight = model.Height,
                        PatientName = model.FullName,
                        PatientNationalID = model.NationalID,
                        PatientPhone = model.Mobile,
                        PatientProfileImage = model.ImagePath,
                        PatientWeight = model.Weight,
                        Patient_UserId = user.Id,
                    };

                    int? res = _patientBLL.Save_Patient_Data(newPatient);

                    if(res == null || res.Value == 0)
                    {
                        var deletUserResult=await UserManager.DeleteAsync(user);
                        ModelState.AddModelError("", "Something went wrong please try again");
                        ViewBag.BlodGroups = _comonBLL.Get_BlodGroups_Data(null);
                        return View(model);
                    }

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Dashboard", "Patient");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}