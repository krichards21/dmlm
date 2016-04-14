using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dmlm.Models
{
    public class UserModel
    {
        public class User
        {
            public int UserID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime LastUpdateDate { get; set; }
        }

        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public User GetUserByEmailandPwd(LoginModel loginModel)
        {
            using (dmlmEntities db = new dmlmEntities())
            {
                var user = new User();
                // need password encryption 
                var userEntity = db.Users.Where(u => u.emailAddress.ToLower() == loginModel.Email.ToLower() && u.password.ToLower() == loginModel.Password.ToLower()).FirstOrDefault();
                if (userEntity == null)
                    throw new KeyNotFoundException();
                user.FirstName = userEntity.firstName;
                user.LastName = userEntity.lastName;
                user.UserID = userEntity.Id;
                user.LastUpdateDate = userEntity.lastUpdateDate ?? DateTime.UtcNow.AddDays(-100);
                return user;
            }
        }
    }
}