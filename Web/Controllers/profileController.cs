using Services.Domain;
using Services.DomainServices.dishbill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers
{
    public class profileController : BaseController
    {
        // GET: profile
        public readonly DishbillDomainService _dishbillDomainService;
        public profileController(DishbillDomainService dishbillDomainService)
        {
            _dishbillDomainService = dishbillDomainService;

        }
        public ActionResult Index()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                long mngId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                try
                {
                    UserOperationData userData = new UserOperationData();
                    User data = _dishbillDomainService.GetUserDataByUserId(mngId);
                    userData.User = data;

                    ViewBag.AppUserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;

                    return View("~/Views/profile/Index.cshtml", userData);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }



        [ActionName("change-pssword")]
        public ActionResult ChangePassword()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                long mngId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                try
                {
                    return View("~/Views/profile/ChangePassword.cshtml");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }



        [HttpPost]
        public ActionResult UpdateUserProfile(UserOperationData mObj)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    UserOperationData isAddSuccess = new UserOperationData();
                    mObj.User.Id = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                    string date = DateTime.Now.ToString("MM-yyyy");

                    if (mObj.UserAvatarThumb != null)
                        mObj.User.Avatar = GetUploadImagePath(mObj.UserAvatarThumb, "user/"+ date);

                    if (mObj.CompanyAvatar != null)
                        mObj.User.CompanyLogo = GetUploadImagePath(mObj.CompanyAvatar, "company/" + date);

                    isAddSuccess = _dishbillDomainService.UpdateUserProfile(mObj, false);

                    return RedirectToAction("Index", "profile");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }


        [HttpPost]
        public ActionResult UpdateChangePassword(UserOperationData mObj)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    UserOperationData isAddSuccess = new UserOperationData();
                    mObj.User.Id = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                    isAddSuccess = _dishbillDomainService.UpdateChangePassword(mObj, false);

                    return RedirectToAction("Index", "profile");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }




    }
}