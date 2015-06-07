using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWF;

namespace SWF.Areas.User.Controllers
{
    public class TaskController : Controller
    {
        //
        // GET: /User/Task/

        public ActionResult Index()
        {

             
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(tblTask model)
        {
            TaskModel taskmodel = new TaskModel();
            if (taskmodel.AddTask(model))
            {
                return RedirectToAction("List");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult List()
        {
            TaskModel taskmodel = new TaskModel();
            return  View(taskmodel.GetTasks());
        }

        public ActionResult Delete(int id)
        {
            TaskModel taskModel = new TaskModel();
            taskModel.Delete(id);
            return RedirectToAction("List");
        }
    }
}
