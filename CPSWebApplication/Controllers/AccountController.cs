using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using CPSWebApplication.Models.ViewModel;
using CPSWebApplication.Models.EntityManager;

namespace CPSWebApplication.Controllers
{
    public class AccountController:Controller
    {
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserLoginView uLV, string returnUrl) 
        {
            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                UserModel user = new UserModel();

                string password = userManager.getUserPassword(uLV.UHCLEmail);

                if (string.IsNullOrEmpty(password))
                    ModelState.AddModelError("", "The user login and password are incorrect");
                else {
                    if (uLV.UHCLEmailPassword.Equals(password))
                    {
                        String role = userManager.getUserRole(uLV.UHCLEmail);

                        FormsAuthentication.SetAuthCookie(uLV.UHCLEmail, false);
                        int uhclId = userManager.GetUserUHCLID(uLV.UHCLEmail);

                        if (role.Equals("Student"))
                        {
                            return RedirectToAction("Student", "Home", new {id = uhclId});
                        }
                        else if (role.Equals("AcademicAdvisor"))
                        {
                            return RedirectToAction("AcademicAdvisor", "Home", new { id = uhclId });
                        }
                        else if (role.Equals("FacultyAdvisor")) {
                            return RedirectToAction("Faculty", "Home", new { id = uhclId });
                        }
                        else
                        return RedirectToAction("Welcome", "Home");
                    }
                }
            }
            return View(uLV);
        }//

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Account");
        }
    }// end of class
}//end of namespace