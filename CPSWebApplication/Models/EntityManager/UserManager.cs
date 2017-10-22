using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.DB;



namespace CPSWebApplication.Models.EntityManager
{
    public class UserManager
    {

        public bool IsLoginUserExist(string username)
        {
            using(EserviceDBEntities db = new EserviceDBEntities())
            {
                return db.APPUsers.Where(o => o.UHCLEmail.Equals(username)).Any();
            }
        }

        public string getUserRole (string username)
        {
            using (EserviceDBEntities db = new EserviceDBEntities())
            {
                var user = db.APPUsers.Where(o => o.UHCLEmail.ToLower().Equals(username));
                if (user.Any())
                {
                    return user.FirstOrDefault().UserRole;
                }
                  else
                    return String.Empty;
            }
        }//end getuserrole

        public string GetUserFirstName (string username)
        {
            using (EserviceDBEntities db = new EserviceDBEntities())
            {
                var user = db.APPUserProfiles.Where(o => o.UHCLEmail.ToLower().Equals(username));
                if (user.Any())
                {
                    return user.FirstOrDefault().FirstName;
                }
                else
                    return String.Empty;
            }
        }

        public string GetUserFullNamebyId(int id)
        {
            using (EserviceDBEntities db = new EserviceDBEntities())
            {
                var user = db.APPUserProfiles.Where(o => o.UHCLID.Equals(id));
                if (user.Any())
                {
                    return (user.FirstOrDefault().FirstName +" " +user.FirstOrDefault().LastName);
                }
            }
            return null;
        }
        public string GetUserLastName(string username)
        {
            using (EserviceDBEntities db = new EserviceDBEntities())
            {
                var user = db.APPUserProfiles.Where(o => o.UHCLEmail.ToLower().Equals(username));
                if (user.Any())
                {
                    return user.FirstOrDefault().LastName;
                }
                else
                    return String.Empty;
            }
        }

        public int GetUserUHCLID(string username)
        {
            using (EserviceDBEntities db = new EserviceDBEntities())
            {
                var user = db.APPUserProfiles.Where(o => o.UHCLEmail.ToLower().Equals(username));
                if (user.Any())
                {
                    return user.FirstOrDefault().UHCLID;
                }
                else
                    return 0;
            }
        }

        public UserModel GetUserNameByID(int uid)
        {
            using (EserviceDBEntities db = new EserviceDBEntities())
            {
                var user = db.APPUserProfiles.Where(o => o.UHCLID.Equals(uid));
                if (user.Any())
                {
                    return new UserModel { FirstName = user.FirstOrDefault().FirstName, LastName = user.FirstOrDefault().LastName, UHCLID = uid.ToString(), UHCLEmail = user.FirstOrDefault().UHCLEmail };
                   // return (user.FirstOrDefault().FirstName +" " +user.FirstOrDefault().LastName);
                }
                else
                    return null;
            }
        }
        public string getUserPassword(string username)
        {
            using (EserviceDBEntities db = new EserviceDBEntities())
            {
                var user = db.APPUsers.Where(o => o.UHCLEmail.ToLower().Equals(username));
                if (user.Any())
                {
                    return user.FirstOrDefault().UHCLEmailPassword;
                }
                else
                {
                    return String.Empty;
                }

            }   //end of using
        }
    }
}














































































































































































































































































































































































































































































































































































































































































































































































































































































































































