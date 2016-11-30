using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using GTI619_Lab5.Models;
using System.Linq;
using System;
using GTI619_Lab5.Entities;

namespace GTI619_Lab5.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
            : this(new MyUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
            _context = new ApplicationDbContext();


            var passConf = _context.AuthentificationConfigs.First();

            ((MyUserManager)userManager).PasswordValidator = new MyUserManager.MyPasswordValidator(passConf.MaxLenght, 
                passConf.MinLenght, 
                passConf.IsSpecialCase, 
                passConf.IsNumber, 
                passConf.IsUpperCase, 
                passConf.IsLowerCase);
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        private ApplicationDbContext _context;

        //
        // GET: /Account/Login
        [RequireHttps]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [RequireHttps]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userByUsername = await UserManager.FindByNameAsync(model.UserName);
                var loginConfig = _context.LoginConfigs.First();

                if (userByUsername == null)
                {
                    ModelState.AddModelError("", "Invalid username.");
                }
                else
                {
                    var lastFailedAttempt = _context.UserLoginLogs.Where(w => w.userId == userByUsername.Id && w.success == false).OrderByDescending(w => w.loginTime).FirstOrDefault();
                    var lastSucessfulAttempt = _context.UserLoginLogs.Where(w => w.userId == userByUsername.Id && w.success == true).OrderByDescending(w => w.loginTime).FirstOrDefault();
                    
                    var failedAttemptsSinceLastSuccess = 0;

                    if (lastFailedAttempt != null)
                    {
                        if (lastSucessfulAttempt == null)
                        {
                            failedAttemptsSinceLastSuccess = _context.UserLoginLogs.Where(w => w.userId == userByUsername.Id).Count();
                        }
                        else
                        {
                            failedAttemptsSinceLastSuccess = _context.UserLoginLogs.Where(w => w.userId == userByUsername.Id && w.loginTime > lastSucessfulAttempt.loginTime).Count();
                        }
                    }

                    if (failedAttemptsSinceLastSuccess > 0 &&
                        failedAttemptsSinceLastSuccess % loginConfig.NbAttemptsBeforeBlocking == 0) // user is blocked
                    {
                        var nbTimesUserHasBeenBlocked = (int)(failedAttemptsSinceLastSuccess / loginConfig.NbAttemptsBeforeBlocking) - 1;

                        if (nbTimesUserHasBeenBlocked >= loginConfig.MaxBlocksBeforeAdmin)
                        {
                            ModelState.AddModelError("", "You have been permanently blocked. Contact Admin");
                            return View(model);
                        }

                        DateTime timeOfLastfailedAttempts = lastFailedAttempt.loginTime;

                        var currentBlockingDelay = TimeSpan.FromMinutes(
                            Int32.Parse(loginConfig.DelayBetweenBlocks.Split(',')[nbTimesUserHasBeenBlocked]));

                        var timeSinceLastfailedAttempts = DateTime.Now.Subtract(timeOfLastfailedAttempts);

                        if (currentBlockingDelay > timeSinceLastfailedAttempts) // verify block is finished
                        {
                            ModelState.AddModelError("", "You have been blocked. Wait " 
                                + Math.Ceiling(currentBlockingDelay.Subtract(timeSinceLastfailedAttempts).TotalMinutes) 
                                + " minutes." );
                            return View(model);
                        }
                    }


                    var user = await UserManager.FindAsync(model.UserName, model.Password);

                    if (user != null)
                    {
                        await SignInAsync(user, model.RememberMe);

                        // Loging successful login
                        _context.UserLoginLogs.Add(
                            new UserLoginLog(
                                user.Id,
                                DateTime.Now,
                                true
                            ));

                        _context.SaveChanges();

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {

                        if (userByUsername != null)
                        {
                            // Loging failed login
                            _context.UserLoginLogs.Add(
                                new UserLoginLog(
                                    userByUsername.Id,
                                    DateTime.Now,
                                    false
                                ));

                            _context.SaveChanges();

                            if (lastFailedAttempt != null)
                            {
                                if (lastSucessfulAttempt == null)
                                {
                                    failedAttemptsSinceLastSuccess = _context.UserLoginLogs.Where(w => w.userId == userByUsername.Id).Count();
                                }
                                else
                                {
                                    failedAttemptsSinceLastSuccess = _context.UserLoginLogs.Where(w => w.userId == userByUsername.Id && w.loginTime > lastSucessfulAttempt.loginTime).Count();
                                }
                            }

                            if (failedAttemptsSinceLastSuccess >= loginConfig.NbAttemptsBeforeBlocking)
                            {
                                var nbTimesUserHasBeenBlocked = (int)(failedAttemptsSinceLastSuccess / loginConfig.NbAttemptsBeforeBlocking) - 1;

                                if (nbTimesUserHasBeenBlocked >= loginConfig.MaxBlocksBeforeAdmin)
                                {
                                    ModelState.AddModelError("", "You have been permanently blocked. Contact Admin");
                                    return View(model);
                                }

                                var currentBlockingDelay = TimeSpan.FromMinutes(
                                    Int32.Parse(loginConfig.DelayBetweenBlocks.Split(',')[nbTimesUserHasBeenBlocked]));

                                DateTime timeOfLastfailedAttempts = lastFailedAttempt.loginTime;

                                var timeSinceLastfailedAttempts = DateTime.Now.Subtract(timeOfLastfailedAttempts);

                                if (currentBlockingDelay > timeSinceLastfailedAttempts) // verify block is finished
                                {
                                    ModelState.AddModelError("", "You have been blocked. Wait "
                                        + Math.Ceiling(currentBlockingDelay.Subtract(timeSinceLastfailedAttempts).TotalMinutes)
                                        + " minutes.");
                                    return View(model);
                                }
                            }
                        }

                        Thread.Sleep(loginConfig.DelayBetweenFailedAuthentication);

                        ModelState.AddModelError("", "Invalid username or password.");
                    }

                }
            }

            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            var registerViewModel = new RegisterViewModel();
            registerViewModel.Roles = _context.Roles.Where(w => w.Name != "Administrateur")
                                                        .Select(s => new RoleModel()
                                                        {
                                                            Value = s.Id,
                                                            Text = s.Name
                                                        })
                                                        .ToList();
            return View(registerViewModel);
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
                var user = new ApplicationUser() { UserName = model.UserName };
                var role = _context.Roles.Where(w=>w.Id == model.Role).FirstOrDefault();
                if(role==null || role.Name=="Administrateur"){throw new Exception();}
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _context.PasswordStores.Add(
                                new PasswordStore(
                                    user.Id,
                                    user.PasswordHash,
                                    DateTime.Now
                                ));

                    _context.SaveChanges();

                    UserManager.AddToRole(user.Id, role.Name);
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            model.Roles = _context.Roles.Where(w => w.Name != "Administrateur")
                                                        .Select(s => new RoleModel()
                                                        {
                                                            Value = s.Id,
                                                            Text = s.Name
                                                        })
                                                        .ToList();

            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Votre mot de passe a été modifié."
                : message == ManageMessageId.SetPasswordSuccess ? "Votre mot de passe a été défini."
                : message == ManageMessageId.RemoveLoginSuccess ? "La connexion externe a été supprimée."
                : message == ManageMessageId.Error ? "Une erreur s'est produite."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    
                    var numberOfPasswordsChecked = 3;   // configurable 
                    var userId = User.Identity.GetUserId();
                    bool passwordAlreadyExists = false;

                    var oldPasswordList = _context.PasswordStores
                    .Where(w => w.userId == userId)
                    .OrderBy(x => x.creationDate)
                    .Take(numberOfPasswordsChecked).Any();

                    if (oldPasswordList)
                    {
                        var oldPasswords = _context.PasswordStores
                            .Where(w => w.userId == userId)
                            .OrderBy(x => x.creationDate)
                            .Take(numberOfPasswordsChecked);

                        foreach (var oldPass in oldPasswords)
                        {
                            var resultCheck = new PasswordHasher().VerifyHashedPassword(oldPass.passwordHash, model.NewPassword);
                            if (resultCheck == PasswordVerificationResult.Success)
                            {
                                passwordAlreadyExists = true;
                            }

                        }
                    }

                    if (passwordAlreadyExists)
                    {
                        ModelState.AddModelError("", "Password already registered");
                        return View(model);
                    }
                    else
                    {
                        IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                        if (result.Succeeded)
                        {
                            _context.PasswordStores.Add(
                                    new PasswordStore(
                                        User.Identity.GetUserId(),
                                        UserManager.FindById(User.Identity.GetUserId()).PasswordHash,
                                        DateTime.Now
                                    ));

                            _context.SaveChanges();

                            return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                        }
                        else
                        {
                            AddErrors(result);
                        }
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        _context.PasswordStores.Add(
                                new PasswordStore(
                                    User.Identity.GetUserId(),
                                    UserManager.FindById(User.Identity.GetUserId()).PasswordHash,
                                    DateTime.Now
                                ));

                        _context.SaveChanges();

                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Demandez une redirection vers le fournisseur de connexions externe
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
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
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
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
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Obtenez des informations sur l’utilisateur auprès du fournisseur de connexions externe
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
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
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Applications auxiliaires
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
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
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}