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
        public string TaskName { get; set; }
        
       
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