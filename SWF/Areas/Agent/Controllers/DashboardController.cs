using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SWF.Classes;
using SWF.Models;

namespace SWF.Areas.Agent.Controllers
{
    public class DashboardController : BaseController
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            DashboardModel dashboardModel = new DashboardModel();
            ProjectModel projectModel = new ProjectModel();
            projectModel.UserId = SessionAccesser.UserId;
            dashboardModel.ProjectModelList = projectModel.GetAllProjectsDetails();

            return View(dashboardModel);
        }

        public ActionResult Header()
        {
            UsersModel usersModel = new UsersModel();
            if (Request.IsAuthenticated)
            {
                usersModel.AspNetUserId = Guid.Parse(SessionAccesser.AspNetUserId.ToString());
                usersModel.GetUserByProviderKey();
                return PartialView("_Header", usersModel);
            }
            else
            {
                return PartialView("_Header", usersModel);
            }
        }

        public ActionResult AvailableTranslators()
        {
            List<UsersModel> translatorsList = new UsersModel().GetLoggedInUsers(Global.RolesEnum.Agent);
            return PartialView("_TranslatorsList", translatorsList);
        }

    }
}
