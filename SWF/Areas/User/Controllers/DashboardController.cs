using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SWF.Classes;
using SWF.Models;

namespace SWF.Areas.User.Controllers
{
    public class DashboardController : BaseController
    {
        SWFEntities DB = new SWFEntities();
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                Int32 intUserId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
                var AgentId = DB.tblAssignedUsers.Where(i => i.UserID == intUserId).Take(1).SingleOrDefault().AgentID;
                ViewBag.AgentId = AgentId;
            }
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


    }
}
