using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SWF.Models;
using SWF.Classes;


namespace SWF.Controllers
{
    public class HomeController : Controller
    {
        SWFEntities db = new SWFEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";


            if (System.Web.HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsIdentity id =
                    (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;

                // Get the stored user-data, in this case, our roles
                string userData = ticket.UserData;
                string[] roles = userData.Split(',');
                System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
                var userId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
                var user = db.tUsers.Where(i => i.intUserId == userId).SingleOrDefault();
               var RoleId = Convert.ToInt32(roles[0]);

               if ((Request.IsAuthenticated) && (!string.IsNullOrEmpty(RoleId.ToString())) && (RoleId==1))
                {
                    return RedirectToAction("Index", "Dashboard", routeValues: new { area = "Admin" });
                }

               if ((Request.IsAuthenticated) && (!string.IsNullOrEmpty(RoleId.ToString())) && (RoleId == 2))
                {
                    return RedirectToAction("Index", "Dashboard", routeValues: new { area = "User" });
                }

               if ((Request.IsAuthenticated) && (!string.IsNullOrEmpty(RoleId.ToString())) && (RoleId == 3))
                {
                    return RedirectToAction("Index", "Dashboard", routeValues: new { area = "Agent" });
                }
            }
          

            return View();
        }
        public ActionResult Register()
        {
           // ViewData["error"] = TempData["Message"];
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
          //  ViewBag.ErrorMessage = TempData["ErrorMessage"];
          //  ViewData["userName"] = TempData["UserName"];
            return View();
        }

        [HttpPost]
        public ActionResult Register(UsersModel UsersModel)
        {
            try
            {
                UsersModel.AddUser();
                TempData["SuccessMessage"] = "Thank you for your interest on us, we will contact you as soon as possible.";

                return RedirectToAction("Register", "Home");
            }
            catch (Exception)
            {

                TempData["SuccessMessage"]="Error";
                return RedirectToAction("Register", "Home");
            }
           

        }
        public ActionResult Login()
        {
            ViewBag.error = TempData["Error"];
            return View();

        }


        [HttpPost]
        public ActionResult Login(UsersModel UsersModel)
        {
            //if (ModelState.IsValid)
            //{
               // Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                UsersModel.Username = UsersModel.Username;
                UsersModel.Password = UsersModel.Password;
                //  Session["User_ID"] = SessionAccesser.UserId;
                var user = db.tUsers.Where(i => i.strUserName == UsersModel.Username && i.strPassword == UsersModel.Password).Count();

                if (user>0)
                {
                    var userdetail = db.tUsers.Where(i => i.strUserName == UsersModel.Username).SingleOrDefault();

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
               1, // Ticket version
               userdetail.intUserId.ToString(), // Username associated with ticket
               DateTime.Now, // Date/time issued
               DateTime.Now.AddMinutes(15), // Date/time to expire
               true, // "true" for a persistent user cookie
               userdetail.intBaseRoleId.ToString(), // User-data, in this case the roles
               FormsAuthentication.FormsCookiePath);// Path cookie valid for

                    // Encrypt the cookie using the machine key for secure transport
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(
                       FormsAuthentication.FormsCookieName, // Name of auth cookie
                       hash); // Hashed ticket

                    // Set the cookie's expiration time to the tickets expiration time
                    if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
                   // HttpContext.Current.Response.Cookies.Add(cookie);
                   
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                    if ((userdetail.intBaseRoleId.Equals(2)))
                    {
                        return RedirectToAction("Index", "Dashboard",routeValues: new { area = "User" });
                    }
                    else if ((userdetail.intBaseRoleId.Equals(1)))
                    {
                        return RedirectToAction("Index", "Dashboard", routeValues: new { area = "Admin" });
                    }
                    else if ((userdetail.intBaseRoleId.Equals(3)))
                    {
                        return RedirectToAction("Index", "Dashboard", routeValues: new { area = "Agent" });
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "User");
                    }
                }
                else
                {
                    TempData["Error"] = "UserName Or Password Invalid";
                    return RedirectToAction("Login", "Home");
                }

            //}
        }
        public ActionResult  Logout()
        {
           
            FormsAuthentication.SignOut();
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index","Home",routeValues: new { area = "" });
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Header()
        {
           UsersModel usersModel = new UsersModel();
            if (Request.IsAuthenticated)
            {

                if (System.Web.HttpContext.Current.User.Identity is FormsIdentity)
                {
                    FormsIdentity id =
                        (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;

                    // Get the stored user-data, in this case, our roles
                    string userData = ticket.UserData;
                    string[] roles = userData.Split(',');
                   System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
                   var userId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
                   var user = db.tUsers.Where(i => i.intUserId == userId).SingleOrDefault();
                   usersModel.RoleId = Convert.ToInt32(roles[0]);
                   usersModel.FirstName = user.strFirstName;
                }
                return PartialView("_Header",usersModel);
            }
            else
            {
                return PartialView("_Header", usersModel);
            }
        }
    }
}
