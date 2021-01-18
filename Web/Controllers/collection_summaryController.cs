using Services.DomainServices.dishbill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers
{
    public class collection_summaryController : Controller
    {
        // GET: collection_summary
        public readonly DishbillDomainService _dishbillDomainService;
        public collection_summaryController(DishbillDomainService dishbillDomainService)
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

                if (roleId == 2)
                    managerId = 0;
                if (roleId == 3 || roleId == 4)
                    fiderId = 0;

                ViewBag.Summary = _dishbillDomainService.GetCollectionSummary(managerId, fiderId);

                return View();
            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }
    }
}