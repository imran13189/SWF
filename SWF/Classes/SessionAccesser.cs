using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SWF.Classes
{
    public class SessionAccesser
    {
        #region Static Variables

        public static int LogoutCounter
        { get; set; }

        #endregion

        #region Session Variables

        public static Guid AspNetUserId
        {
         
            get { return Guid.Parse(GetValue("AspNetUserId").ToString()); }
            set { SaveValue("AspNetUserId", value.ToString()); }
        
        }

        public static long UserId
        {
            get { return Convert.ToInt32(GetValue("UserId")); }
            set { SaveValue("UserId", value.ToString()); }
        }

        public static long ToUserId
        {
            get { return Convert.ToInt32(GetValue("ToUserId")); }
            set { SaveValue("ToUserId", value.ToString()); }
        }

        public static Guid RoleId
        {
            get { return Guid.Parse(GetValue("RoleId").ToString()); }
            set { SaveValue("RoleId", value.ToString()); }
        }

        public static string RoleName
        {
            get { return GetValue("RoleName").ToString(); }
            set { SaveValue("RoleName", value.ToString()); }
        }

        #endregion

        #region Constructor

        public SessionAccesser()
        {
            LogoutCounter = 0;
        }

        #endregion

        #region Methods

        private static void SaveValue(string key, string value, bool encrypted = false)
        {
            HttpContext.Current.Session[key] = value;

            HttpCookie appCookie = GetAppCookie();
            if (encrypted)
            {
                appCookie[key] = HttpContext.Current.Server.UrlEncode(value);
            }
            else
            {
                appCookie[key] = value;
            }
            HttpContext.Current.Response.Cookies.Add(appCookie);
        }

        public static HttpCookie GetAppCookie()
        {
            HttpCookie appCookie = null;
            if (HttpContext.Current.Request.Cookies["SWFLoginInfo"] != null)
            {
                appCookie = HttpContext.Current.Request.Cookies["SWFLoginInfo"];
            }
            else
            {
                appCookie = new HttpCookie("SWFLoginInfo");
            }
            appCookie.Expires = DateTime.Now.AddHours(1);

            return appCookie;
        }

        private static object GetValue(string key, bool encrypted = false, bool logout = true)
        {
            try
            {
                object returnValue = null;

                if (HttpContext.Current.Session[key] != null)
                {
                    returnValue = HttpContext.Current.Session[key];
                }

                if (string.IsNullOrEmpty(returnValue + "".Trim()))
                {
                    HttpCookie appCookie = GetAppCookie();
                    if (appCookie[key] != null)
                    {
                        if (encrypted)
                        {
                            returnValue = HttpContext.Current.Server.UrlDecode(appCookie[key].ToString());
                        }
                        else
                        {
                            returnValue = appCookie[key].ToString();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(returnValue + "".Trim()))
                {
                    HttpContext.Current.Session[key] = returnValue;
                }
                else if (logout)
                {
                    string redirectUrl = "~/Login/Logout";
                    LogoutCounter = 1;
                    HttpContext.Current.Response.Redirect(redirectUrl);
                }

                return returnValue;
            }
            catch
            {
                return null;
            }
        }

        public static void ClearAll()
        {
            HttpCookie appCookie = GetAppCookie();

            ClearKey("AspNetUserId", appCookie);
            ClearKey("UserId", appCookie);
            ClearKey("ToUserId", appCookie);
            ClearKey("RoleId", appCookie);
            ClearKey("RoleName", appCookie);          
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();

            appCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(appCookie);
        }

        public static void ClearKey(string key, HttpCookie appCookie)
        {
            HttpContext.Current.Session[key] = null;
            appCookie[key] = null;
        }
        public static Int32 GetRole()
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
                //var userId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);

                Int32 RoleId = Convert.ToInt32(roles[0]);
                return RoleId;
            }
            else
            {
                return 4;
            }


        }
        #endregion
    }
}
