using Services.AppServices;
using Services.Domain;
using Services.DomainServices.Account;
using Services.DomainServices.dishbill;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers
{
    public class homeController : BaseController
    {
        public readonly DishbillDomainService _dishbillDomainService;
        public readonly AccountDomainService _accountDomainService;
        public homeController(AccountDomainService accountDomainService, DishbillDomainService dishbillDomainService)
        {
            _accountDomainService = accountDomainService;
            _dishbillDomainService = dishbillDomainService;

        }
        public ActionResult Index()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                ViewBag.LoggedInUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                long userId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                ViewBag.Notific = _dishbillDomainService.GetTotalNotifications(userId, ViewBag.UserRoleId);
                return View("~/Views/home/Index.cshtml");

            }
            else {
                return RedirectToAction("application", "home");
            }
        }


        public PartialViewResult _LoadLiveChatWidget()
        {
            long receiverId = 0;
            int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;
            
            if (roleId == 3)
                receiverId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
            else if (roleId == 4)
                receiverId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;

            ViewBag.ReciverId = receiverId;

            ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;

            return PartialView("~/Views/Partial/LiveChatMessage.cshtml");
        }

        public ActionResult application()
        {
            return View("~/Views/home/application.cshtml");
        }

    }
}