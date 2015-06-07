using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWF.Areas.Agent.Controllers
{
    [Authorize(Roles="Agent")]
    public class BaseController : Controller
    {
        //
        // GET: /Client/Base/

        //public ActionResult Index()
        //{
        //    return View();
        //}

    }
}
