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
            return View(new TaskModel().GetModel());
        }

        [HttpPost]
        public ActionResult Add(TaskModel model)
        {
            TaskModel taskmodel = new TaskModel();
            if (taskmodel.AddTask(model.task))
            {
                return RedirectToAction("List");
            }
            else
            {
                return View(model);
            }
        }


        public ActionResult Edit(int id)
        {
                return View(new TaskModel().GetTask(id));
        }

        [HttpPost]
        public ActionResult Edit(tblTask model)
        {
            TaskModel taskmodel = new TaskModel();
            if (taskmodel.UpdateTask(model))
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
