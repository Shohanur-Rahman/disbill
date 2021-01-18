using Services.Domain.dishbill;
using Services.DomainServices.dishbill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers
{
    public class notificationController : BaseController
    {
        // GET: notification
        public readonly DishbillDomainService _dishbillDomainService;
        public notificationController(DishbillDomainService dishbillDomainService)
        {
            _dishbillDomainService = dishbillDomainService;
        }
        public ActionResult Index()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0 && (LoggedInUserInfoFromCookie.AppUserRoleId == 3 || LoggedInUserInfoFromCookie.AppUserRoleId == 2))
            {
                try
                {
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long mngId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    int roleId= LoggedInUserInfoFromCookie.AppUserRoleId;
                    long userId = 0;
                    if (roleId == 2)
                        userId = fiderId;
                    else if (roleId == 3)
                        userId = mngId;

                    ViewBag.Notification= _dishbillDomainService.GetNotificationListByUserId(userId, roleId);

                      return View();
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


        public ActionResult UpdateNotificationAsSeen(int notificationId)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    int status = _dishbillDomainService.UpdateNotificationAsSeen(notificationId);

                    return Json(status, JsonRequestBehavior.AllowGet);
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