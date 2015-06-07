using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SWF.Models
{
    public class TaskModels
    {

        #region Variables
       
        string moduleName = "TaskModel";
        #endregion

        #region Properties

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Task Name")]
        public string TaskName
        { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Task Date")]
        public string TaskDate
        { get; set; }

       
        [Display(Name = "Assigned To")]
        public string AssignedTo
        { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy
        { get; set; }
        [Display(Name = "time")]
        public string TaskTime
        { get; set; }
        [Display(Name = "is checked")]
        public Boolean CheckTime
        { get; set; }

        [Display(Name = "Hours")]
        public Int32 Hours
        { get; set; }
        [Display(Name = "Minutes")]
        public Int32 Minutes
        { get; set; }


        #endregion
    }
}