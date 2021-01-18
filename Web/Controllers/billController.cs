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
    public class billController : BaseController
    {
        // GET: bill

        public readonly DishbillDomainService _dishbillDomainService;
        public billController(DishbillDomainService dishbillDomainService)
        {
            _dishbillDomainService = dishbillDomainService;
        }
        public ActionResult Index()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }

        [ActionName("bill-baki")]
        public ActionResult DueGrahok()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long mngId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    ViewBag.ThisMonth = DateTime.Now.ToString("MM");
                    ViewBag.MonthNameList = new SelectList(_dishbillDomainService.GetAllMonthsName(), "Id", "Name");
                    ViewBag.AreNameList = new SelectList(_dishbillDomainService.GetAreaNameByManagerIdWithDetault(LoggedInUserInfoFromCookie.AppUserIdInCookie.Value), "Id", "Name");
                    return View("~/Views/bill/DueGrahok.cshtml");
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


        [ActionName("bill-collection")]
        public ActionResult BillCollection()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long mngId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    ViewBag.ThisMonth = DateTime.Now.ToString("MM");
                    ViewBag.AreNameList = new SelectList(_dishbillDomainService.GetAreaNameByManagerIdWithDetault(LoggedInUserInfoFromCookie.AppUserIdInCookie.Value), "Id", "Name");
                    ViewBag.MonthNameList = new SelectList(_dishbillDomainService.GetAllMonthsName(), "Id", "Name");
                    return View("~/Views/bill/BillCollection.cshtml");
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


        [ActionName("bill-paid")]
        public ActionResult BillPaid()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long mngId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    ViewBag.AreNameList = new SelectList(_dishbillDomainService.GetAreaNameByManagerIdWithDetault(LoggedInUserInfoFromCookie.AppUserIdInCookie.Value), "Id", "Name");
                    return View("~/Views/bill/BillPaid.cshtml");
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


        public ActionResult GetAllBillPaidGrahokThisMonth()
        {
            if ((LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0))
            {
                try
                {
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long managerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;

                    long userId = 0;
                    if (roleId == 2)
                        managerId = 0;
                    else if (roleId == 3)
                        fiderId = 0;
                    else if (roleId == 4)
                    {
                        managerId = 0;
                        fiderId = 0;
                        userId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                    }
                    IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.GetAllBillPaidGrahokThisMonth(managerId, fiderId, userId, 0, 0, 0);

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


        public ActionResult GetAllGrahokForPayBill()
        {
            if ((LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0))
            {
                try
                {
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long managerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;

                    long userId = 0;
                    if (roleId == 2)
                        managerId = 0;
                    else if (roleId == 3)
                        fiderId = 0;
                    else if (roleId == 4)
                    {
                        managerId = 0;
                        fiderId = 0;
                        userId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                    }
                    IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.GetAllGrahokForPayBill(managerId, fiderId, userId, 0, 0, 0);

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



        [HttpPost]
        public ActionResult SaveBillCollectInformationData(BillOperationModel mObj)
        {
            if ((LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0))
            {
                try
                {
                    int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    BillOperationModel operationMessage = new BillOperationModel();

                    mObj.BillTable.FiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    mObj.BillTable.ManagerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    mObj.BillTable.CollectorRoleId = roleId;

                    if (mObj.BillTable.AdvanceAmount == null)
                        mObj.BillTable.AdvanceAmount = 0;

                    if (roleId == 4)
                        mObj.BillTable.CollectorId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;

                    mObj.BillTable.CollectedBy = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;

                    mObj.UserName = LoggedInUserInfoFromCookie.UserFirstNameInCookie;

                    operationMessage = _dishbillDomainService.SaveBillCollecInformation(mObj, true);


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




        [HttpPost]
        public ActionResult SaveBillCollectInformation(BillCollectOperationModel mObj)
        {
            if ((LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0))
            {
                try
                {
                    int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    BillCollectOperationModel operationMessage = new BillCollectOperationModel();

                    mObj.BillTable.FiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    mObj.BillTable.ManagerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    mObj.BillTable.CollectorRoleId = roleId;

                    if (mObj.BillTable.AdvanceAmount == null)
                        mObj.BillTable.AdvanceAmount = 0;

                    if (roleId==4)
                        mObj.BillTable.CollectorId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;

                    mObj.BillTable.CollectedBy = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                     
                    mObj.UserName = LoggedInUserInfoFromCookie.UserFirstNameInCookie;

                    operationMessage = _dishbillDomainService.SaveBillCollectInformation(mObj, true);


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


        public ActionResult GetGrahokBillHistory(long grahokId)
        {
            if ((LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0))
            {
                try
                {
                    IList<Bill_History_For_Grahok> dataList = _dishbillDomainService.Get_Bill_History_For_Grahok(grahokId);

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


        public ActionResult mobiles()
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["apiKey"]) == false && Request.QueryString["apiKey"] == "ed20XCode1")
                {
                    int page = 0;
                    int limit = 0;
                    if (string.IsNullOrEmpty(Request.QueryString["page"]) == false)
                        page = Convert.ToInt32(Request.QueryString["page"]);

                    if (string.IsNullOrEmpty(Request.QueryString["limit"]) == false)
                        limit = Convert.ToInt32(Request.QueryString["limit"]);

                    IList<allSMSMobile> dataList = _dishbillDomainService.GetAllSMSMobileNumbers(page, limit);
                    return Json(dataList, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json("Invalid access", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult listed_sms()
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["apiKey"]) == false && Request.QueryString["apiKey"] == "ed20XCode1")
                {
                    IList<Messages> dataList = _dishbillDomainService.GetAllMessageInformation();
                    return Json(dataList, JsonRequestBehavior.AllowGet);
                }
                else {

                    return Json("Invalid access", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult update_sms()
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["apiKey"]) == false && Request.QueryString["apiKey"] == "ed20XCode1")
                {
                    long id = 0;
                    int status = 0;
                    int local = 0;
                    if (string.IsNullOrEmpty(Request.QueryString["id"]) == false)
                        id = long.Parse(Request.QueryString["id"]);

                    if (string.IsNullOrEmpty(Request.QueryString["status"]) == false)
                        status = int.Parse(Request.QueryString["status"]);

                    if (string.IsNullOrEmpty(Request.QueryString["local"]) == false)
                        local = int.Parse(Request.QueryString["local"]);

                    Messages msg = new Messages();
                    msg.id = id;
                    msg.local = local;
                    msg.status = status;

                    SMSUpdateStatus data= _dishbillDomainService.UpdateSMS(msg);

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json("Invalid access", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}