using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SWF
{

    public class CRMTypeMetadata
    {
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string SpecificTime { get; set; }

         [Required]
        public string SpecialRequest { get; set; }
    }

    [MetadataType(typeof(CRMTypeMetadata))]
    public partial class tblTask
    {
    }
    public class TaskModel
    {
        public tblTask task { get; set; }
        public IList<tblTask> taskList{get;set;}


        public TaskModel GetModel()
        {
            try
            {
                using (SWFEntities db = new SWFEntities())
                {
                    int userID = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
                    IEnumerable<tblTask> list = from m in db.tblTasks where m.IsCompleted == true && m.CreatedBy == userID select m;
                    return new TaskModel()
                    {
                        task = new tblTask() {TaskTime=DateTime.Now },
                        taskList = list == null ? null : list.OrderByDescending(x => x.CreatedOn).Take(5).ToList()
                    };

                }
            }
            catch {

                return new TaskModel()
                {

                    task = new tblTask()
                };
            }
        
        }
        public bool AddTask(tblTask model)
        {
            try
            {
                using (SWFEntities db = new SWFEntities())
                {
                    int userID = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
                    model.CreatedBy = 1;
                    model.CreatedOn = DateTime.Now;
                    model.CreatedBy = userID;
                    if(!model.IsSpecificTime)
                    {
                        model.SpecificTime = "N/A";
                    }
                    model.IsCompleted = false;
                    db.tblTasks.Add(model);
                    db.SaveChanges();
                }

                return true;
            }
            catch { return false; }
        }

        public tblTask GetTask(int taskID)
        {
            try
            {
                using (SWFEntities db = new SWFEntities())
                {
                    return db.tblTasks.FirstOrDefault(x => x.TaskId == taskID);
                }
            }
            catch { return null; }
        }

        public bool UpdateTask(tblTask task)
        {
            try
            {
                using (SWFEntities db = new SWFEntities())
                {
                    tblTask tbltask= db.tblTasks.FirstOrDefault(x => x.TaskId == task.TaskId);
                    tbltask.TaskName = task.TaskName;
                    tbltask.TaskTime = task.TaskTime;
                    tbltask.IsSpecificTime = task.IsSpecificTime;
                    tbltask.SpecificTime = task.SpecificTime;
                    if (!task.IsSpecificTime)
                    {
                        tbltask.SpecificTime = "N/A";
                    }
                    tbltask.SpecialRequest = task.SpecialRequest;
                    db.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }
        public bool Delete(int id)
        {
            try
            {
                using (SWFEntities db = new SWFEntities())
                {
                    tblTask task = db.tblTasks.FirstOrDefault(x => x.TaskId == id);
                    db.tblTasks.Remove(task);
                    db.SaveChanges();
                }

                return true;
            }
            catch { return false; }
        }

        public IEnumerable<tblTask> GetTasks()
        {
                SWFEntities db = new SWFEntities();
            
                return db.tblTasks;
            
        }
    }
}