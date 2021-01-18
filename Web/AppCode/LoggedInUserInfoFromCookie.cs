using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Security;
//using Microsoft.AspNetCore.Authorization;
using System.Web.Mvc;
using Services.Domain;
using Services.DomainServices;
using System.Security.Claims;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Http;

using System.Web;

namespace Web.AppCode
{
    public class LoggedInUserInfoFromCookie 
    {

        public static string LoggedInUserLogo;
        public static string LoggedInUserDisplayName;

        public static string UserIdRoleEncryptedInCookie
        {
            set
            {
                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                authCookie.Expires = DateTime.Now.AddMinutes(7200);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }
        
        public static long? AppUserIdInCookie
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserId.ToString()] != null)
                {
                    string userId = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserId.ToString()].Value;
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(userId);
                    return Convert.ToInt64(authTicket.Name);
                }
                else {
                    return Convert.ToInt64(0);
                }
                
            }
            set
            {
                var authTicket = new FormsAuthenticationTicket(value.ToString(), true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserId.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }


        public static long? UserFiderIdInCookie
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.FiderId.ToString()] != null)
                {
                    string userId = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.FiderId.ToString()].Value;
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(userId);
                    return Convert.ToInt64(authTicket.Name);
                }
                else
                {
                    return Convert.ToInt64(0);
                }

            }
            set
            {
                var authTicket = new FormsAuthenticationTicket(value.ToString(), true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.FiderId.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }


        public static long? UserManagerIdInCookie
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.ManagerId.ToString()] != null)
                {
                    string userId = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.ManagerId.ToString()].Value;
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(userId);
                    return Convert.ToInt64(authTicket.Name);
                }
                else
                {
                    return Convert.ToInt64(0);
                }

            }
            set
            {
                var authTicket = new FormsAuthenticationTicket(value.ToString(), true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.ManagerId.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }


        public static int AppUserRoleId
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.RoleId.ToString()] != null)
                {
                    string userId = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.RoleId.ToString()].Value;
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(userId);
                    return Convert.ToInt32(authTicket.Name);
                }
                else
                {
                    return Convert.ToInt32(0);
                }

            }
            set
            {
                var authTicket = new FormsAuthenticationTicket(value.ToString(), true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.RoleId.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }

        public static string UserLogoInCookie
        {
        

            get
            {

                string loggedInUserLogo = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserOrCompanyLogo.ToString()].Value;
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(loggedInUserLogo);
                return authTicket.Name;
            }
            set
            {
                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserOrCompanyLogo.ToString(), encryptedTicket);
               // HttpCookie cookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserOrCompanyLogo.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }
        
        //User First Name
        public static string UserFirstNameInCookie
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserFirstName.ToString()] !=null) {
                    string firstName = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserFirstName.ToString()].Value;
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(firstName);
                    return authTicket.Name;
                }
                return "";

            }
            set
            {

                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserFirstName.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }


        public static string UserAvatarUrlInCookie
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserAvatarURL.ToString()] != null)
                {
                    string firstName = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserAvatarURL.ToString()].Value;
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(firstName);
                    return authTicket.Name;
                }
                return "";

            }
            set
            {

                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserAvatarURL.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }

        public static string UserEmailAddressInCookie
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserEmailAddress.ToString()] != null)
                {
                    string firstName = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserEmailAddress.ToString()].Value;
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(firstName);
                    return authTicket.Name;
                }
                return "";

            }
            set
            {

                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserEmailAddress.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }
        

    }//end class
}
