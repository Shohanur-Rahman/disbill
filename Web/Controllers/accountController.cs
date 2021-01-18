using Services.ApplicationEntity.Account;
using Services.AppServices;
using Services.DataServices.Account;
using Services.Domain;
using Services.Domain.Models.User.EditorModel;
using Services.DomainServices;
using Services.DomainServices.Account;
using Services.EmailService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Web.AppCode;

namespace Web.Controllers
{
    public class accountController : BaseController
    {
        public readonly AccountDataService _accountDataService;
        // GET: account
        public accountController(AccountDataService accountDomainService)
        {
            _accountDataService = accountDomainService;

        }

        //public ActionResult register()
        //{
        //    return View();
        //}

        public ActionResult Index(LoginModel model)
        {
            return View();
        }

        public ActionResult login(LoginModel model)
        {
            //string encryptPassword = SimpleCryptService.Factory().Encrypt("123");
            string serverMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
            string successMessage = MessageDisplayHelper.InformationMessageSetOrDisplay(this, false, string.Empty);
            ViewBag.SuccessMessage = successMessage;
            ViewBag.ServerMessage = serverMessage;
            return View();
        }

        public ActionResult registerresult()
        {
            string successMessage = MessageDisplayHelper.InformationMessageSetOrDisplay(this, false, string.Empty);
            ViewBag.SuccessMessage = successMessage;
            string errorMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
            ViewBag.ErrorMessage = errorMessage;

            return View();
        }

        [ActionName("forgot-password")]
        public ActionResult ForGotPassword()
        {
            string serverMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
            ViewBag.ServerMessage = serverMessage;
            return View("~/Views/account/ForGotPassword.cshtml");
        }

        public ActionResult Logout()
        {

            if (User.Identity.IsAuthenticated)
            {
                long? userId = LoggedInUserInfoFromCookie.AppUserIdInCookie;
                //  _userDomainService.UpdateCurrentUserLoggedInInfo(userId);

                var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    authCookie.Expires = DateTime.Today.AddDays(-1);
                }

                Response.Cookies.Add(authCookie);

                FormsAuthentication.SignOut();

            }

            List<string> cookieKeyNameList = new List<string>();
            foreach (var key in HttpContext.Request.Cookies.Keys)
            {
                cookieKeyNameList.Add(key.ToString());
            }

            if (cookieKeyNameList != null && cookieKeyNameList.Count > 0)
            {
                foreach (string cookieKey in cookieKeyNameList)
                {
                    HttpCookie cookie = new HttpCookie(cookieKey);
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }
            }
            return RedirectToAction("login", "account");
        }

        #region Login Related Method

        
        [HttpPost]
        public ActionResult DoLogin(LoginModel loginModel)
        {

            loginSuccessData loginResult = null;

            //string encryptPassword = SimpleCryptService.Factory().Encrypt("WmxGGJwz");
            
            return DataActionViewService(
                () =>
                {
                    loginResult = _accountDataService.Login(loginModel.MobileNumber, loginModel.Password);

                },
                () =>
                {
                    //if role is user or stuff

                    if (loginResult != null && loginResult.User != null && loginResult.IsLoginError == false)
                    {
                        LoggedInUserInfoFromCookie.AppUserIdInCookie = loginResult.User.Id;
                        LoggedInUserInfoFromCookie.UserEmailAddressInCookie = loginResult.User.MobileNumber;
                        LoggedInUserInfoFromCookie.UserAvatarUrlInCookie = loginResult.User.Avatar;
                        LoggedInUserInfoFromCookie.UserFirstNameInCookie = loginResult.User.Name;
                        LoggedInUserInfoFromCookie.AppUserRoleId = loginResult.User.RoleId;
                        LoggedInUserInfoFromCookie.UserFiderIdInCookie = 0;
                        LoggedInUserInfoFromCookie.UserManagerIdInCookie = 0;

                        if (loginResult.User.RoleId == 2)
                        {
                            LoggedInUserInfoFromCookie.UserFiderIdInCookie = loginResult.User.Id;
                            LoggedInUserInfoFromCookie.UserManagerIdInCookie = 0;
                        }
                        else if (loginResult.User.RoleId == 3)
                        {
                            LoggedInUserInfoFromCookie.UserFiderIdInCookie = loginResult.User.CreatedId;
                            LoggedInUserInfoFromCookie.UserManagerIdInCookie = loginResult.User.Id;
                        }
                        else if (loginResult.User.RoleId == 4)
                        {
                            LoggedInUserInfoFromCookie.UserFiderIdInCookie = loginResult.User.UserFiderId;
                            LoggedInUserInfoFromCookie.UserManagerIdInCookie = loginResult.User.UserManagerId;
                        }

                        return RedirectToAction("Index", "home");
                    }

                    // return RedirectToAction("Index", "home");

                    return Json(loginResult, JsonRequestBehavior.AllowGet);

                },
                () => View(nameof(Index)));
        }

        #endregion
        
        
        [HttpPost]
        public ActionResult UpdatePassword(UserOperationModel mObj)
        {
            try
            {
                UserOperationModel operationMessage = new UserOperationModel();
                operationMessage = _accountDataService.UpdatePassword(mObj);

                if (operationMessage.isAddOperation == true)
                {
                    MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, operationMessage.message);
                    return RedirectToAction("login", "account");
                }
                else
                {
                    MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, operationMessage.message);

                }
                return RedirectToAction("forgot-password", "account");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}