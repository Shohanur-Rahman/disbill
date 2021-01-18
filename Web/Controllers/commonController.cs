using Services.Domain;
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
    public class commonController : BaseController
    {
        // GET: common
        public readonly DishbillDomainService _dishbillDomainService;
        public commonController(DishbillDomainService dishbillDomainService)
        {
            _dishbillDomainService = dishbillDomainService;

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserListByMobileNumber(string mobileNumber)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                long mngId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                try
                {
                    IList<User> dataList = _dishbillDomainService.GetUserListByMobileNumber(mobileNumber);

                    return Json(dataList, JsonRequestBehavior.AllowGet);
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

       
        public ActionResult GetUserDetailsByUserId(long userId)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    User data = _dishbillDomainService.GetUserDataByUserId(userId);

                    return Json(data, JsonRequestBehavior.AllowGet);
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



        public ActionResult GetCollectorAreaList(long userId)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    IList<GrahokAreaMapper> data = _dishbillDomainService.GetCollectorAreaList(userId);

                    return Json(data, JsonRequestBehavior.AllowGet);
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