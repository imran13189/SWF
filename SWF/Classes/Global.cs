using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWF.Classes
{
    public class Global
    {

        #region Enums

        public enum TranslationTypes
        {
            File = 1,
            Text = 2
        }

        public enum ProjectStatus
        {
            Completed = 1,
            InProgress = 2,
            Pending = 3,
            Cancelled = 4
        }

        public enum RolesEnum
        {
            All = 0,
            Admin = 1,
            User = 2,
            Agent = 3,
            PM_Client = 4,
            PM_Manager = 5
        }

        public enum Gender
        {
            Both = 0,
            Male = 1,
            Female = 2
        }

        #endregion


    }
}