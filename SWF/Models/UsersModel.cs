using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using SWF.Classes;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using System.Net.Mail;
using System.Net;

namespace SWF.Models
{
    public class UsersModel
    {
        SWFEntities db= new SWFEntities();
        #region Variables
        string moduleName = "UsersModel";
        #endregion

        #region Properties

        public long UserID
        { get; set; }

        public long RoleId
        { get; set; }

       public List<UsersModel> lstUsers = new List<UsersModel>();
       public List<UsersModel> lstAgents = new List<UsersModel>();


        [Required(ErrorMessage = "Required")]
        [Display(Name = "First Name")]
        public string FirstName
        { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Last Name")]
        public string LastName
        { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Username
        { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email
        { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        //[RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})", ErrorMessage = "Password doesn't match the criteria")]
        public string Password
        { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        //[RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})", ErrorMessage = "Password doesn't match the criteria")]
        public string NewPassword
        { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword
        { get; set; }

        [Display(Name = "Best Way To Reach You")]
        public string BestWayToReachYou
        { get; set; }
        [Display(Name = "Referal Code")]
        public string ReferalCode
        { get; set; }

       
        public DateTime? CreatedDate
        { get; set; }

        public bool blActive
        { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber
        { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Address")]
        public string Address
        { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Query")]
        public string Query
        { get; set; }
        #region specific to information display

      
        
        #endregion

       
        #endregion

        #region Constructor

        public UsersModel()
        {
           // LanguageModel = new LanguageModel();
        }

        #endregion
        #region Methods
        //Register
        public long AddUser()
        {
            try
            {
                using (SWFEntities context = new SWFEntities())
                {
                    tUser objUser = new tUser();
                    objUser.strFirstName = FirstName;
                    objUser.strLastName = LastName;
                    objUser.dtCreatedOn =DateTime.Now;
                    objUser.strEmail = Email;
                 //   objUser.CustomerContactId = CustomerContactId;
                    objUser.blActive = true;
                    objUser.strBestWay = BestWayToReachYou;
                    objUser.strRefCode = ReferalCode;
                    objUser.intBaseRoleId = 2;
                    objUser.strAddress = Address;
                    objUser.strPhoneNumber = PhoneNumber;
                    objUser.strBestWay=BestWayToReachYou;
                   // objUser.Gender = Convert.ToByte(Global.Gender.Male);
                    objUser.strUserName = Guid.NewGuid().ToString().Substring(0, 6);
                    objUser.strPassword = Guid.NewGuid().ToString().Substring(0, 6);
                    context.tUsers.Add(objUser);
                    context.SaveChanges();
                    //Send Email code here
                    sendMail(objUser.strUserName, objUser.strEmail, objUser.strPassword);
                    return objUser.intUserId;
                }
            }
            catch (Exception ex)
            {
                new ExceptionLogger(moduleName, "AddUser", ex);
                throw;
            }
        }
        public void sendMail(string userName,string email,string password )
        {
            using (MailMessage mm = new MailMessage("noReply@gmail.com", email))
            {
                mm.Subject = "Registration Email";
                mm.Body = "Your UserName: "+userName+" Your Password: "+password+"";

                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("testing4573@gmail.com", "Test@123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

            }
        }

        //public static void Logout()
        //{
        //    if (SessionAccesser.LogoutCounter <= 0)
        //    {
        //        LoggedInUserModel.RemoveLoggedInUser(SessionAccesser.UserId);
        //    }

        //    SessionAccesser.ClearAll();

        //    FormsAuthentication.SignOut();
        //    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
        //    cookie.Expires = DateTime.Now.AddDays(-1);
        //}
         #endregion

        //#region Methods

        //public long AddUser()
        //{
        //    try
        //    {
        //        using (SWFEntities context = new SWFEntities())
        //        {
        //            User objUser = new User();
        //            objUser.FirstName = FirstName;
        //            objUser.LastName = LastName;
        //            objUser.AspNetUserId = AspNetUserId;
        //            objUser.CreatedDate = CreatedDate;
        //            objUser.ContactId = ContactId;
        //            objUser.CustomerContactId = CustomerContactId;
        //            objUser.IsActive = true;
        //            objUser.BestWayToReachYou = BestWayToReachYou;
        //            objUser.ReferalCode = ReferalCode;
        //            objUser.Gender = Convert.ToByte(Global.Gender.Male);

        //            context.Users.Add(objUser);
        //            context.SaveChanges();

        //            return objUser.ID;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        new ExceptionLogger(moduleName, "AddUser", ex);
        //        throw;
        //    }
        //}

        //public void GetUser()
        //{
        //    try
        //    {
        //        using (SWFEntities context = new SWFEntities())
        //        {
        //            User objUser = context.Users.FirstOrDefault(q => q.ID == ID);

        //            if (objUser != null)
        //            {
        //                ID = objUser.ID;
        //                FirstName = objUser.FirstName;
        //                LastName = objUser.LastName;
        //                CreatedDate = objUser.CreatedDate;
        //                IsActive = objUser.IsActive ?? false;
        //                AspNetUserId = objUser.AspNetUserId;
        //                ContactId = objUser.ContactId;
        //                CustomerContactId = objUser.CustomerContactId;
        //                Gender = objUser.Gender;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        new ExceptionLogger(moduleName, "GetUser", ex);
        //    }
        //}

        //public void GetUserByProviderKey()
        //{
        //    try
        //    {
        //        using (SWFEntities context = new SWFEntities())
        //        {
        //            User objUser = context.Users.FirstOrDefault(q => q.AspNetUserId == AspNetUserId);

        //            if (objUser != null)
        //            {
        //                FirstName = objUser.FirstName;
        //                LastName = objUser.LastName;
        //                IsActive = objUser.IsActive ?? false;
        //                CreatedDate = objUser.CreatedDate;
        //                AspNetUserId = objUser.AspNetUserId;
        //                Gender = objUser.Gender;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        new ExceptionLogger(moduleName, "GetUserByProviderKey", ex);
        //    }
        //}

        //public void DeleteUser()
        //{
        //    try
        //    {
        //        using (SWFEntities context = new SWFEntities())
        //        {
        //            User objUser = context.Users.FirstOrDefault(q => ((AspNetUserId != Guid.Empty) ? q.AspNetUserId == AspNetUserId : q.ID == ID));

        //            if (objUser != null)
        //            {
        //                context.Users.Remove(objUser);
        //                context.SaveChanges();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        new ExceptionLogger(moduleName, "DeleteUser", ex);
        //    }
        //}

        //public List<UsersModel> GetUsers(Global.RolesEnum argRole = Classes.Global.RolesEnum.All)
        //{
        //    List<UsersModel> result = new List<UsersModel>();

        //    try
        //    {
        //        string roleName = Enum.GetName(typeof(Global.RolesEnum), argRole).Replace('_', ' ');

        //        using (SWFEntities context = new SWFEntities())
        //        {
        //            //result = context.Users.Join(context.vw_aspnet_UsersInRoles, user => user.AspNetUserId, userInRole => userInRole.UserId, (user, userInRole) => new { Users = user, UsersInRoles = userInRole })
        //            //            .Join(context.vw_aspnet_Roles, usersRoles => usersRoles.UsersInRoles.RoleId, role => role.RoleId, (userRoles, role) => new { UsersRoles = userRoles, Role = role })
        //            //            .Join(context.Projects)
        //            //            .Where(q => ((!roleName.Equals("All")) ? q.Role.RoleName.Equals(roleName) : roleName.Equals("All")))
        //            //            .Select(res => new UsersModel()
        //            //            {
        //            //                ID = res.UsersRoles.Users.ID,
        //            //                FirstName = res.UsersRoles.Users.FirstName,
        //            //                LastName = res.UsersRoles.Users.LastName,
        //            //                AspNetUserId = res.UsersRoles.Users.AspNetUserId,
        //            //                IsActive = res.UsersRoles.Users.IsActive ?? false,
        //            //                CreatedDate = res.UsersRoles.Users.CreatedDate,
        //            //                Username = res.UsersRoles.Users.aspnet_Users.UserName,
        //            //                StatusText = ((res.UsersRoles.Users.IsActive == true) ? "Active" : "Inactive"),
        //            //                Gender = res.UsersRoles.Users.Gender
        //            //            }).ToList();
        //            result = (from p in context.Users
        //                      join q in context.vw_aspnet_UsersInRoles on p.AspNetUserId equals q.UserId
        //                      join r in context.vw_aspnet_Roles on q.RoleId equals r.RoleId
        //                      join s in context.TranslatorDetails on p.ID equals s.TranslatorId
        //                      join t in context.Languages on s.LanguageId equals t.Id
        //                      join u in context.Projects on t.Id equals u.TargetLanguage
        //                      where r.RoleName == roleName
        //                      select new UsersModel
        //                      {
        //                          ID = p.ID,
        //                          FirstName = p.FirstName,
        //                          LastName = p.LastName,
        //                          AspNetUserId = p.AspNetUserId,
        //                          IsActive = p.IsActive ?? false,
        //                          CreatedDate = p.CreatedDate,
        //                          //StatusText = (p.IsActive==true)? "Active" : "Inactive"),
        //                          Targetlanguage = u.TargetLanguage,
        //                          Sourcelanguage = u.SourceLanguage
        //                      }).ToList();


        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        new ExceptionLogger(moduleName, "GetUsers", ex);
        //        return result;
        //    }
        //}

        //public List<UsersModel> GetLoggedInUsers(Global.RolesEnum argRole = Classes.Global.RolesEnum.All)
        //{
        //    List<UsersModel> result = new List<UsersModel>();

        //    try
        //    {
        //        string roleName = Enum.GetName(typeof(Global.RolesEnum), argRole);

        //        using (SWFEntities context = new SWFEntities())
        //        {
        //            if (argRole == Global.RolesEnum.Agent)
        //            {
        //                result = (from users in context.Users
        //                          join userInRoles in context.vw_aspnet_UsersInRoles on users.AspNetUserId equals userInRoles.UserId
        //                          join roles in context.vw_aspnet_Roles.Where(q => q.RoleName.Equals(roleName)) on userInRoles.RoleId equals roles.RoleId
        //                          join loggedInUser in context.LoggedInUsers on users.ID equals loggedInUser.UserID
        //                          select users).ToList()
        //                          .Select(res => new UsersModel()
        //                          {
        //                              ID = res.ID,
        //                              FirstName = res.FirstName,
        //                              LastName = res.LastName,
        //                              AspNetUserId = res.AspNetUserId,
        //                              IsActive = res.IsActive ?? false,
        //                              CreatedDate = res.CreatedDate,
        //                              Username = res.aspnet_Users.UserName,
        //                              StatusText = ((res.IsActive == true) ? "Active" : "Inactive"),
        //                              Gender = res.Gender,
        //                              LanguageModel = new LanguageModel()
        //                              {
        //                                  Name = LanguageModel.GetLanguageNamesByTranslator(res.ID)
        //                              }
        //                          }).ToList();
        //            }
        //            else
        //            {
        //                result = context.Users.Join(context.vw_aspnet_UsersInRoles, user => user.AspNetUserId, userInRole => userInRole.UserId, (user, userInRole) => new { Users = user, UsersInRoles = userInRole })
        //                            .Join(context.vw_aspnet_Roles, usersRoles => usersRoles.UsersInRoles.RoleId, role => role.RoleId, (userRoles, role) => new { UsersRoles = userRoles, Role = role })
        //                            .Where(q => ((!roleName.Equals("All")) ? q.Role.RoleName.Equals(roleName) : roleName.Equals("All")))
        //                            .Join(context.LoggedInUsers, rolesUsersInRoles => rolesUsersInRoles.UsersRoles.Users.ID, logInUsers => logInUsers.UserID, (rolesUsersInRoles, logInUsers) => new { RolesUsersInRoles = rolesUsersInRoles, LogInUsers = logInUsers })
        //                            .Select(res => new UsersModel()
        //                            {
        //                                ID = res.RolesUsersInRoles.UsersRoles.Users.ID,
        //                                FirstName = res.RolesUsersInRoles.UsersRoles.Users.FirstName,
        //                                LastName = res.RolesUsersInRoles.UsersRoles.Users.LastName,
        //                                AspNetUserId = res.RolesUsersInRoles.UsersRoles.Users.AspNetUserId,
        //                                IsActive = res.RolesUsersInRoles.UsersRoles.Users.IsActive ?? false,
        //                                CreatedDate = res.RolesUsersInRoles.UsersRoles.Users.CreatedDate,
        //                                Username = res.RolesUsersInRoles.UsersRoles.Users.aspnet_Users.UserName,
        //                                StatusText = ((res.RolesUsersInRoles.UsersRoles.Users.IsActive == true) ? "Active" : "Inactive"),
        //                                Gender = res.RolesUsersInRoles.UsersRoles.Users.Gender
        //                            }).ToList();
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        new ExceptionLogger(moduleName, "GetLoggedInUsers", ex);
        //        return result;
        //    }
        //}

        //public static List<SelectListItem> GetUsersDropDown(Global.RolesEnum argRole = Global.RolesEnum.All)
        //{
        //    UsersModel objUserModel = new UsersModel();
        //    List<UsersModel> usersList = objUserModel.GetUsers(argRole);

        //    return usersList.Select(usr => new SelectListItem()
        //    {
        //        Value = usr.ID.ToString(),
        //        Text = (usr.FirstName + " " + usr.LastName)
        //    }).ToList();
        //}

        //#endregion
    }
}