using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Web;
using System.Web.Security;

namespace dmlm.Models
{
    public class UserModel
    {
        public bool CheckAccessToken(string accessToken, string id, DbContext ctx)
        {
            var manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var store = new UserStore<ApplicationUser>(ctx);
            //var manager = new UserManager<ApplicationUser>(store);
            var user = manager.Users.Where(u => u.Id == id).FirstOrDefault();
            //var tokens = manager.UserTokenProvider.ValidateAsync("MobileAuth", accessToken, manager, user);
            return manager.VerifyUserToken(id, "MobileAuth", accessToken);
        }
        public class LoginModel
        {
            public int ID { get; set; }
            public string AccessToken { get; set; }
            public string AspUserID { get; set; }

            public bool HasError { get; set; }
            public List<string> Error { get; set; }
        }

        public LoginModel LoginUser(LoginViewModel model)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                var manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = manager.Find(model.Email, model.Password);
                    var userEntity = db.Users.Where(u => u.emailAddress.ToLower() == model.Email.ToLower()).FirstOrDefault();
                    if (userEntity == null)
                        throw new KeyNotFoundException();
                    userEntity.loginDate = userEntity.lastUpdateDate ?? DateTime.UtcNow.AddDays(-100);
                //userEntity.aspUserID = user.Id;
                //ModifyUser(userEntity);
                var token = manager.GenerateUserToken("MobileAuth", user.Id);
                return new LoginModel() { ID = userEntity.Id, AccessToken = token, AspUserID = user.Id };
            }
        }

        public LoginModel RegisterUser(RegisterUser model)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new dmlm.Models.ApplicationDbContext()));
                var result = userManager.Create(user, model.Password);
                var registerModel = new LoginModel();
                if (result.Succeeded)
                {
                    HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>().SignIn(user, true, false);
                    //signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    db.Users.Add(new User() {  aspUserID = user.Id, firstName = model.Firstname, lastName = model.Lastname, cellPhoneNumber = model.Phone,
                    createDate = DateTime.UtcNow, isActive = true, loginDate = DateTime.UtcNow, emailAddress = model.Email});
                    db.SaveChanges();
                    registerModel.AspUserID = user.Id;
                    registerModel.HasError = false;
                    registerModel.AccessToken = user.SecurityStamp;

                }
                else
                {
                    registerModel.AspUserID = string.Empty;
                    registerModel.HasError = true;
                    registerModel.Error = result.Errors.ToList();
                    registerModel.AccessToken = string.Empty;
                }
                return registerModel;
            }
        }

        public bool ModifyUser(dmlm.User user)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }
    }
}