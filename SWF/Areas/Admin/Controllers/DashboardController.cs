using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SWF.Models;
using SWF.Classes;

namespace SWF.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        //
        // GET: /Admin/Dashboard/
        SWFEntities DB = new SWFEntities();
        public ActionResult Index()
        {
            //DashboardModel dashboardModel = new DashboardModel();
            //ProjectModel projectModel = new ProjectModel();
            //dashboardModel.ProjectModelList = projectModel.GetAllProjectsDetails();

            return View();
        }

        //[HttpPost]
        //public ActionResult Index(DashboardModel dashboardModel)
        //{
        //    ProjectModel projectModel = new ProjectModel();
        //    projectModel.Status = dashboardModel.StatusId;
        //    dashboardModel.ProjectModelList = projectModel.GetAllProjectsDetails();
        //    return View(dashboardModel);
        //}

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
                    var user = DB.tUsers.Where(i => i.intUserId == userId).SingleOrDefault();
                    usersModel.RoleId = Convert.ToInt32(roles[0]);
                    usersModel.FirstName = user.strFirstName;

                }
                return PartialView("_Header", usersModel);
            }
            else
            {
                return PartialView("_Header", usersModel);
            }
        }

        //public ActionResult GetProjects(int status)
        //{
        //    ProjectModel projectModel = new ProjectModel();
        //    projectModel.Status = status;
        //    return PartialView("_ProjectsList", projectModel.GetAllProjectsDetails());
        //}
        //public ActionResult AssignTranslator(long Id = 0, string name = "")
        //{
        //    List<Assigntranslator> translator = new List<Assigntranslator>();
        //    List<SelectListItem> languages = new List<SelectListItem>();
        //    if (name != "")
        //    {
        //        var abxcf = dB.Languages.Where(w => w.Name == name).Select(s => s.Id).FirstOrDefault();
        //        var newrecord = dB.Projects.Where(w => w.ProjectID == Id).Select(s => s.TargetLanguage).FirstOrDefault();
        //        var asdfg = dB.Languages.Where(w => w.Id == newrecord).Select(s => s.Name).FirstOrDefault();
        //        var newlanguages = (from p in dB.Languages select p).Distinct();

        //        foreach (var items in newlanguages)
        //        {
        //            languages.Add(new SelectListItem()
        //            {
        //                Text = items.Name,
        //                Value = items.Id.ToString(),
        //                Selected = (items.Name == asdfg ? true : false)
        //            });
        //        }

        //        ViewBag.language = languages;
        //        translator = (from p in dB.aspnet_Membership
        //                      join m in dB.aspnet_Users on p.UserId equals m.UserId
        //                      join q in dB.vw_aspnet_UsersInRoles on m.UserId equals q.UserId
        //                      join r in dB.aspnet_Roles on q.RoleId equals r.RoleId
        //                      join s in dB.Users on m.UserId equals s.AspNetUserId
        //                      join w in dB.TranslatorDetails on s.ID equals w.TranslatorId
        //                      join t in dB.Languages on w.LanguageId equals t.Id
        //                      join u in dB.Projects on t.Id equals u.TargetLanguage
        //                      where r.RoleName == "Translator" && w.LanguageId == abxcf
        //                      select new Assigntranslator { Translator = s.FirstName, Id = s.ID }).ToList();
        //        ViewBag.records = translator;
        //    }
        //    else
        //    {
                
                
        //            var abxcf = dB.Languages.Where(w => w.Name == name).Select(s => s.Id).FirstOrDefault();
        //            var newrecord = dB.Projects.Where(w => w.ProjectID == Id).Select(s => s.TargetLanguage).FirstOrDefault();
        //            var asdfg = dB.Languages.Where(w => w.Id == newrecord).Select(s => s.Name).FirstOrDefault();
        //            var newlanguages = (from p in dB.Languages select p).Distinct();

        //            foreach (var items in newlanguages)
        //            {
        //                languages.Add(new SelectListItem()
        //                {
        //                    Text = items.Name,
        //                    Value = items.Id.ToString(),
        //                    Selected = (items.Name == asdfg ? true : false)
        //                });
        //            }

        //            ViewBag.language = languages;
        //            translator = (from p in dB.aspnet_Membership
        //                          join m in dB.aspnet_Users on p.UserId equals m.UserId
        //                          join q in dB.vw_aspnet_UsersInRoles on m.UserId equals q.UserId
        //                          join r in dB.aspnet_Roles on q.RoleId equals r.RoleId
        //                          join s in dB.Users on m.UserId equals s.AspNetUserId
        //                          join w in dB.TranslatorDetails on s.ID equals w.TranslatorId
        //                          join t in dB.Languages on w.LanguageId equals t.Id
        //                          join u in dB.Projects on t.Id equals u.TargetLanguage
        //                          where r.RoleName == "Translator" && w.LanguageId == newrecord
        //                          select new Assigntranslator { Translator = s.FirstName, Id = s.ID }).ToList();
        //            ViewBag.records = translator;
                
        //    }
        //    var record = (from p in dB.Projects
        //                  join m in dB.Languages on p.TargetLanguage equals m.Id
        //                  join q in dB.TranslatorDetails on m.Id equals q.LanguageId
        //                  join r in dB.Users on q.TranslatorId equals r.ID
        //                  where p.ProjectID == Id
        //                  select new Assigntranslator { ProjectName = p.ProjectName, ClientName = r.FirstName, ProjectId = p.ProjectID, ClientId = r.ID, PId = q.ID }).FirstOrDefault();

        //    return View(record);
        //}



        public ActionResult AssignUser()
        {
            UsersModel usermodel = new UsersModel();
            //Int32 RoleId = SessionAccesser.GetRole();
            usermodel.lstUsers = (from u in DB.tUsers
                                  where u.intBaseRoleId==2
                                  
                                  select new UsersModel { FirstName = u.strFirstName, UserID = u.intUserId, LastName =u.strLastName }).ToList();
            // ViewBag.agents = agents;


            usermodel.lstAgents = (from u in DB.tUsers
                                    where u.intBaseRoleId==3

                                  select new UsersModel { FirstName = u.strFirstName, UserID = u.intUserId, LastName = u.strLastName }).ToList();
            //   ViewBag.users = users;





            return View(usermodel);
        }


        
        //public ActionResult SaveDocument(Assigntranslator model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var record = dB.TranslatorDetails.Where(w => w.ID == model.PId).FirstOrDefault();

        //        //TranslatorDetail ObjTranslatorDetail = new TranslatorDetail();
        //        record.Comment = model.Comment;
        //        record.LanguageId= model.TargetLanguage;
        //        record.ProjectId = model.ProjectId;
        //        record.ClientId = model.ClientId;
        //        record.DeliveryDate = model.DeliveryDate;
        //        dB.SaveChanges();
        //    }
        //    return RedirectToAction("Index", "Dashboard");
        //}

        //[HttpPost]
        //public string ConvertLogInfoToXml(string jsonOfLog)
        //{
        //    return Convert.ToString(jsonOfLog);
        //}

        [HttpPost]
        public ActionResult SaveUsers(List<string> userids, string agentid)
        {
            try
            {
                if (userids != null)
                {
                    foreach (var item in userids)
                    {
                        tblAssignedUser asignUser = new tblAssignedUser();
                        asignUser.AgentID = Convert.ToInt32(agentid);
                        asignUser.UserID = Convert.ToInt32(item);
                        asignUser.CreatedDate = System.DateTime.Now;
                        DB.tblAssignedUsers.Add(asignUser);
                        DB.SaveChanges();

                    }

                    return Json("User Assigned Successfully.", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Please Select atleast one user to assign.", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
