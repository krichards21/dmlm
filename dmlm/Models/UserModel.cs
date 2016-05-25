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
            public string AccessToken { get; set; }
            public string AspUserID { get; set; }
        }

        public dmlm.User LoginUser(string email)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                var user = new dmlm.User();
                    var userEntity = db.Users.Where(u => u.emailAddress.ToLower() == email.ToLower()).FirstOrDefault();
                    if (userEntity == null)
                        throw new KeyNotFoundException();
                    user.loginDate = userEntity.lastUpdateDate ?? DateTime.UtcNow.AddDays(-100);
                    return user;
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