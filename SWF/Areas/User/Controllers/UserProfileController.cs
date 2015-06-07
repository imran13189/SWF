using SWF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.IO;
using System.Web.WebPages;
using System.Text;
using SWF.Classes;
using System.Net.Mail;
using System.Net;

namespace SWF.Areas.User.Controllers
{
    public class UserProfileController : Controller
    {
        SWFEntities DB = new SWFEntities();
        //
        // GET: /Client/ClientProfile/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyAccount()
        {

            Int32 UserId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
            var Record = (from p in DB.tUsers

                          where p.intUserId == UserId
                          select new UsersModel
                          {
                              FirstName = p.strFirstName,
                              LastName = p.strLastName,
                              Email = p.strEmail,
                              BestWayToReachYou = p.strBestWay,
                              Username = p.strUserName,
                              Address = p.strAddress,
                              PhoneNumber = p.strPhoneNumber
                          }).FirstOrDefault();

            ViewData["error"] = TempData["error"];
            return View(Record);
        }
        public ActionResult CustomerProfile()
        {
          
            Int32 UserId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
            var Record = (from p in DB.tUsers

                          where p.intUserId == UserId
                          select new UsersModel
                          {
                              FirstName = p.strFirstName,
                              LastName = p.strLastName,
                              Email=p.strEmail,
                              BestWayToReachYou=p.strBestWay,
                              Username=p.strUserName,
                              Address=p.strAddress,
                              PhoneNumber=p.strPhoneNumber
                          }).FirstOrDefault();
          
            ViewData["error"] = TempData["error"];
            return View(Record);
        }

        public ActionResult AgentProfile(string id)
        {

            var userId = Convert.ToInt32(id);
            var Record = (from p in DB.tUsers

                          where p.intUserId == userId
                          select new UsersModel
                          {
                              FirstName = p.strFirstName,
                              LastName = p.strLastName,
                              Email = p.strEmail,
                              BestWayToReachYou = p.strBestWay,
                              Username = p.strUserName,
                              Address = p.strAddress,
                              PhoneNumber = p.strPhoneNumber
                          }).FirstOrDefault();

            ViewData["error"] = TempData["error"];
            return View(Record);
        }

        public ActionResult UpdateRecord(UsersModel UsersModel)
        {

            var UserId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
            var user = DB.tUsers.Where(w => w.intUserId == UserId).SingleOrDefault();
                
                if (user!=null)
                {
                    user.strPassword = UsersModel.Password;
                    user.strUserName = UsersModel.Username;
                    user.strPhoneNumber = UsersModel.PhoneNumber;
                    user.strFirstName = UsersModel.FirstName;
                    user.strLastName = UsersModel.LastName;
                    DB.SaveChanges();

                }

                else
                {
                    TempData["error"] = "There is some errors";
                    //ViewBag.Message = "Old Password Wrong !";
                    return RedirectToAction("MyAccount", "UserProfile");
                }
            
            return RedirectToAction("Index", "Dashboard");
        }


        public ActionResult ContactUs()
        {

            Int32 UserId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
            var Record = (from p in DB.tUsers

                          where p.intUserId == UserId
                          select new UsersModel
                          {
                              FirstName = p.strFirstName,
                              LastName = p.strLastName,
                              Email = p.strEmail,
                              BestWayToReachYou = p.strBestWay,
                              Username = p.strUserName,
                              Address = p.strAddress,
                              PhoneNumber = p.strPhoneNumber
                          }).FirstOrDefault();

            ViewData["error"] = TempData["error"];
            return View(Record);
        }
        public ActionResult Contact(UsersModel UsersModel)
        {
            sendMail(UsersModel.FirstName, "vippy4573@gmail.com", UsersModel.PhoneNumber, UsersModel.Address, UsersModel.Query);
            ViewBag.Message = "Your Query has been submitted Successfully to the our support services contact you soon.";
            return RedirectToAction("Index", "Dashboard");
        }
        public void sendMail(string Name, string email, string PhoneNumber,string Address,string query)
        {
            using (MailMessage mm = new MailMessage("noreply@gmail.com", email))
            {
                mm.Subject = "Customer Query Email";
                mm.Body = "Name: " + Name + " Phone Number: " + PhoneNumber + " Address: " + Address + "Submitted date time: " + System.DateTime.Now + " Your Query: " + query + "";

                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("testing4573@gmail.com", "Test@123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

            }
        }


    }
}
