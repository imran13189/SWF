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

namespace SWF.Areas.Agent.Controllers
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
            var User_Id = Session["User_ID"];
            Guid abc = (Guid)User_Id;
            var Record = (from p in DB.aspnet_Users
                          join q in DB.Users on p.UserId equals q.AspNetUserId
                          join r in DB.aspnet_Membership on p.UserId equals r.UserId
                          where p.UserId == abc
                          select new ProfileModel
                          {
                              FirstName = q.FirstName,
                              LastName = q.LastName,
                              Email = r.Email

                          }).FirstOrDefault();
            ViewData["error"] = TempData["error"];
            return View(Record);
        }
        public ActionResult UpdateRecord(ProfileModel profilemodel)
        {
            if (!ModelState.IsValid)
            {
                var User_Id = Session["User_ID"];
                Guid newuserid = (Guid)User_Id;
                var password = DB.PasswordDetails.Where(w => w.UserId == newuserid).OrderByDescending(m=>m.Id).FirstOrDefault();
                string newpass = profilemodel.Password;
                if (password.PasswordName == newpass)
                {
                    List<PasswordDetail> countvalue = new List<PasswordDetail>();
                    countvalue = DB.PasswordDetails.Where(w => w.UserId == newuserid).ToList();

                    if (countvalue.Count > 4)
                    {
                        var deletedrecord = DB.PasswordDetails.Where(w => w.UserId == newuserid).FirstOrDefault();
                        DB.PasswordDetails.Remove(deletedrecord);
                        DB.SaveChanges();
                        var getPass = DB.PasswordDetails.Where(w => w.UserId == newuserid && w.PasswordName == profilemodel.NewPassword).Any();
                        var get_Pass = DB.PasswordDetails.Where(w => w.UserId == newuserid && w.PasswordName == newpass).FirstOrDefault();
                        var record_membership = DB.aspnet_Membership.Where(w => w.UserId == newuserid).FirstOrDefault();

                        if (getPass != true)
                        {
                            MembershipUser mu = Membership.GetUser(newuserid);
                            mu.ChangePassword(mu.ResetPassword(), profilemodel.NewPassword);
                            PasswordDetail ObjPasswordDetail = new PasswordDetail();
                            ObjPasswordDetail.PasswordName = profilemodel.NewPassword;
                            ObjPasswordDetail.UserId = newuserid;
                            DB.PasswordDetails.Add(ObjPasswordDetail);
                            DB.SaveChanges();


                            var record = DB.Users.Where(w => w.AspNetUserId == newuserid).FirstOrDefault();
                            var new_record = DB.aspnet_Membership.Where(w => w.UserId == newuserid).FirstOrDefault();
                            if (record != null)
                            {
                                record.FirstName = profilemodel.FirstName;
                                record.LastName = profilemodel.LastName;
                                new_record.Email = profilemodel.Email;
                                DB.SaveChanges();
                            }
                        }
                        else
                        {
                            TempData["error"] = "Please Choose Another Password ! Don't belong to last 4 Password";
                            return RedirectToAction("MyAccount", "ClientProfile");
                        }
                    }
                    else if (countvalue.Count <= 4)
                    {
                        var getPass = DB.PasswordDetails.Where(w => w.UserId == newuserid && w.PasswordName == profilemodel.NewPassword).Any();
                        var get_Pass = DB.PasswordDetails.Where(w => w.UserId == newuserid && w.PasswordName == newpass).FirstOrDefault();
                        var record_membership = DB.aspnet_Membership.Where(w => w.UserId == newuserid).FirstOrDefault();

                        if (getPass != true)
                        {
                            MembershipUser mu = Membership.GetUser(newuserid);
                            mu.ChangePassword(mu.ResetPassword(), profilemodel.NewPassword);
                            PasswordDetail ObjPasswordDetail = new PasswordDetail();
                            ObjPasswordDetail.PasswordName = profilemodel.NewPassword;
                            ObjPasswordDetail.UserId = newuserid;
                            DB.PasswordDetails.Add(ObjPasswordDetail);
                            DB.SaveChanges();

                            
                            var record = DB.Users.Where(w => w.AspNetUserId == newuserid).FirstOrDefault();
                            var new_record = DB.aspnet_Membership.Where(w => w.UserId == newuserid).FirstOrDefault();
                            if (record != null)
                            {
                                record.FirstName = profilemodel.FirstName;
                                record.LastName = profilemodel.LastName;
                                new_record.Email = profilemodel.Email;
                                DB.SaveChanges();
                            }
                        }
                        else
                        {

                            TempData["error"] = "Please Choose Another Password ! Don't belong to last 4 Password";
                            return RedirectToAction("MyAccount", "ClientProfile");
                        }


                    }
                }

                else
                {
                    TempData["error"] = "Old Password Wrong !";
                    //ViewBag.Message = "Old Password Wrong !";
                    return RedirectToAction("MyAccount", "ClientProfile");
                }
            }
            return RedirectToAction("Index", "Dashboard");
        }



    }
}
