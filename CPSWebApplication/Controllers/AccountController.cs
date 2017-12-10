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
            FormsAuthentication.SignOut();
            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["Role"] = null;
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
                        String fullName = userManager.GetUserFullNamebyId(uhclId);

                        Session["UserID"] = uhclId.ToString();
                        Session["UserName"] = fullName.ToString();
                        Session["Role"] = role.ToString();

                        if (role.Equals("Student"))
                        { 
                            return RedirectToAction("Student", "Home", new {id = uhclId});
                        }
                        else if (role.Equals("AcademicAdvisor"))
                        {
                            return RedirectToAction("AcademicAdvisor", "Home", new { id = uhclId });
                        }
                        else if (role.Equals("FacultyAdvisor"))
                        {
                            return RedirectToAction("Faculty", "Home", new { id = uhclId });
                        }
                        else if (role.Equals("Secretary"))
                        {
                            return RedirectToAction("Secretary", "Home", new { id = uhclId });
                        }

                        else
                            return RedirectToAction("Welcome", "Home");
                    }
                }
            }
            return View(uLV);
        }//

        public ActionResult forgotPassword()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult forgotPassword(UserLoginView user)
        {
            UserManager mgr = new UserManager();
            mgr.updateUserDetails(user.UHCLEmail, user.confirmPassword);
            TempData["Message"] = "Password updated successfully.";
            return RedirectToAction("LogIn", "Account");
        }

        public ActionResult HomePage(int id)
        {
            UserManager userManager = new UserManager();
            UserModel user = new UserModel();
            String role = userManager.getUserRoleById(id);

            if (role.Equals("Student"))
            {
                return RedirectToAction("Student", "Home", new { id = id });
            }
            else if (role.Equals("AcademicAdvisor"))
            {
                return RedirectToAction("AcademicAdvisor", "Home", new { id = id });
            }
            else if (role.Equals("FacultyAdvisor"))
            {
                return RedirectToAction("Faculty", "Home", new { id = id });
            }
            else if (role.Equals("Secretary"))
            {
                return RedirectToAction("Secretary", "Home", new { id = id });
            }

            else
                return RedirectToAction("Login", "Account");

        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["Role"] = null;

            return RedirectToAction("LogIn", "Account");
        }
    }// end of class
}//end of namespace