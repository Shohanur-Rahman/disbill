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
    public class smsController : Controller
    {
        // GET: sms
        public readonly DishbillDomainService _dishbillDomainService;
        public smsController(DishbillDomainService dishbillDomainService)
        {
            _dishbillDomainService = dishbillDomainService;
        }
        public ActionResult Index()
        {
            if ((LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0) && (LoggedInUserInfoFromCookie.AppUserRoleId == 2 || LoggedInUserInfoFromCookie.AppUserRoleId == 3))
            {
                ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                long managerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                long loginUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                long userId = 0;
                int page = 0;
                int limit = 0;
                int api = 0;
                if (roleId == 2)
                    managerId = 0;
                else if (roleId == 3)
                    fiderId = 0;
                else if (roleId == 4)
                    userId = loginUserId;

                ViewBag.GrahokList = _dishbillDomainService.ManagerGetAllGrahok(managerId, fiderId, userId, page, limit, api);

                
                ViewBag.ManagerList = _dishbillDomainService.AdminGetmanagerList(loginUserId);

                ViewBag.CollectorList = _dishbillDomainService.ManagerGetCollectorListByManagerId(managerId, fiderId);
                ViewBag.FreeGrahok = _dishbillDomainService.ManagerGetFreeGrahok(managerId, fiderId, userId, page, limit, api);
                ViewBag.DeActiveGrahok = _dishbillDomainService.ManagerGetAllDeactiveGrahok(managerId, fiderId, userId, page, limit, api);

                ViewBag.ActiveGrahok = _dishbillDomainService.ManagerGetAllActiveGrahok(managerId, fiderId, userId, page, limit, api);

                return View();
            }
            else
            {
                return RedirectToAction("login", "account");
            }
            
        }


        [HttpPost]
        public ActionResult SendSMSToAllUser(SMSOperationModel mObj)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    SMSOperationModel operationMessage = new SMSOperationModel();
                    mObj.LoginUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                    mObj.UserRole = LoggedInUserInfoFromCookie.AppUserRoleId;
                    operationMessage = _dishbillDomainService.SendSMSToAllUser(mObj);

                    return Json(operationMessage, JsonRequestBehavior.AllowGet);
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