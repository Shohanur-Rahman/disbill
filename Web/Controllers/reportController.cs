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
    public class reportController : BaseController
    {
        // GET: report
        public readonly DishbillDomainService _dishbillDomainService;
        public reportController(DishbillDomainService dishbillDomainService)
        {
            _dishbillDomainService = dishbillDomainService;
        }



        public ActionResult Index()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    long userId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                    int roleId= LoggedInUserInfoFromCookie.AppUserRoleId;
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;

                    ViewBag.MonthlyBillHistory = _dishbillDomainService.GetBillHistoryDataByRoleAndId(roleId, userId);
                    ViewBag.DailyBillHistory = _dishbillDomainService.GetDailyBillHistoryDataByRoleAndId(roleId, userId);
                    ViewBag.CollectorList = new SelectList(_dishbillDomainService.GetCollectorNamesAndIdListByRoleAndUserId(userId, roleId), "Id", "Name");

                    ViewBag.RoleId = roleId;
                    
                    ViewBag.EndDate = "";
                    ViewBag.StartDate = "";

                    string date = DateTime.Now.ToString("dd");
                    string month = DateTime.Now.ToString("MM");
                    string year = DateTime.Now.ToString("yyyy");

                    DateTime startDate= Convert.ToDateTime(month + "/" +"1/" + year);
                    DateTime endDate= Convert.ToDateTime(month + "/" + date + "/" + year);
                    long collectorId = 0;
                    long managerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;

                    if (string.IsNullOrEmpty(Request.QueryString["start"]) == false)
                        startDate = Convert.ToDateTime(Request.QueryString["start"]);

                    if (string.IsNullOrEmpty(Request.QueryString["end"]) == false)
                        endDate = Convert.ToDateTime(Request.QueryString["end"]);

                    if (string.IsNullOrEmpty(Request.QueryString["cltId"]) == false)
                        collectorId = Convert.ToInt64(Request.QueryString["cltId"]);

                    if (string.IsNullOrEmpty(Request.QueryString["mngId"]) == false)
                        managerId = Convert.ToInt64(Request.QueryString["mngId"]);
                    
                    if (string.IsNullOrEmpty(Request.QueryString["mngName"]) == false)
                        ViewBag.ManagerName = Request.QueryString["mngName"];

                    if (string.IsNullOrEmpty(Request.QueryString["cltName"]) == false)
                        ViewBag.CollectorName = "->"+ Request.QueryString["cltName"];

                    if (roleId > 2)
                        fiderId = 0;

                    if (string.IsNullOrEmpty(Request.QueryString["start"]) == false && string.IsNullOrEmpty(Request.QueryString["end"]) == false)
                    {
                        ViewBag.SearchResultHistory = _dishbillDomainService.GetDailyBillHistoryDataByRoleAndIdAndDate(fiderId, managerId, collectorId, startDate, endDate);
                        ViewBag.StartDate= Convert.ToDateTime(Request.QueryString["start"]).ToString("MM/dd/yyyy");
                        ViewBag.EndDate = Convert.ToDateTime(Request.QueryString["end"]).ToString("MM/dd/yyyy");
                    }

                    ViewBag.DataHistory = _dishbillDomainService.GetBillCollectionHistory(fiderId, managerId, collectorId, startDate, endDate, 0, 0, 0);
                    ViewBag.CollectorId = collectorId;
                    ViewBag.ManagerId = managerId;

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



        public ActionResult GetCollectorListByManagerId(long userId)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                long mngId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                try
                {
                    IList<UserNameAndIdList> dataList = _dishbillDomainService.GetCollectorNamesAndIdListByRoleAndUserId(userId, 3);

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

    }
}