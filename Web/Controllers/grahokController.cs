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
    public class grahokController : BaseController
    {
        // GET: grahok
        public readonly DishbillDomainService _dishbillDomainService;
        public grahokController(DishbillDomainService dishbillDomainService)
        {
            _dishbillDomainService = dishbillDomainService;
        }

        public ActionResult Index()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long mngId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    ViewBag.AreNameList = new SelectList(_dishbillDomainService.GetAreaNameByManagerIdWithDetault(LoggedInUserInfoFromCookie.AppUserIdInCookie.Value), "Id", "Name");
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

        [ActionName("free-grahok")]
        public ActionResult FreeGrahok()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long mngId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;

                    ViewBag.AreNameList = new SelectList(_dishbillDomainService.GetAreaNameByManagerIdWithDetault(LoggedInUserInfoFromCookie.AppUserIdInCookie.Value), "Id", "Name");
                    return View("~/Views/grahok/FreeGrahok.cshtml");
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


        [ActionName("all-grahok")]
        public ActionResult AllGrahok()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long mngId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                   
                    ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    ViewBag.AreNameList = new SelectList(_dishbillDomainService.GetAreaNameByManagerIdWithDetault(LoggedInUserInfoFromCookie.AppUserIdInCookie.Value), "Id", "Name");
                    return View("~/Views/grahok/AllGrahok.cshtml");
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



        [ActionName("bondho-grahok")]
        public ActionResult BondhoGrahok()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long mngId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    ViewBag.AreNameList = new SelectList(_dishbillDomainService.GetAreaNameByManagerIdWithDetault(LoggedInUserInfoFromCookie.AppUserIdInCookie.Value), "Id", "Name");
                    return View("~/Views/grahok/BondhoGrahok.cshtml");
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


        [ActionName("active-grahok")]
        public ActionResult ActiveGrahok()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long mngId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    ViewBag.AreNameList = new SelectList(_dishbillDomainService.GetAreaNameByManagerIdWithDetault(LoggedInUserInfoFromCookie.AppUserIdInCookie.Value), "Id", "Name");
                    return View("~/Views/grahok/ActiveGrahok.cshtml");
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
        public ActionResult SaveGrahokInformation(GrahokOperationModel mObj)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    GrahokOperationModel operationMessage = new GrahokOperationModel();

                    mObj.GrahokTable.CreatedId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                    mObj.GrahokTable.ManagerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    mObj.GrahokTable.FiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    operationMessage = _dishbillDomainService.SaveGrahokInformation(mObj, 2);


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
        public ActionResult SaveFreeGrahokInformation(GrahokOperationModel mObj)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    GrahokOperationModel operationMessage = new GrahokOperationModel();

                    mObj.GrahokTable.CreatedId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                    mObj.GrahokTable.ManagerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    mObj.GrahokTable.FiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    operationMessage = _dishbillDomainService.SaveGrahokInformation(mObj, 1);


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
        public ActionResult SendSMSToUser(SMSOperationModel mObj)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    SMSOperationModel operationMessage = new SMSOperationModel();
                    operationMessage = _dishbillDomainService.SendSMSToUser(mObj);

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


        public ActionResult ManagerGetDueAmountGrahokList()
        {
            if ((LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0))
            {
                try
                {
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long managerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    int page = 0;
                    int limit = 0;
                    int api = 0;
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
                    IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.ManagerGetDueAmountGrahokList(managerId, fiderId, userId, page, limit, api);

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

        
        public ActionResult ManagerGetFreeGrahok()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                long mngId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                try
                {
                    int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;

                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long managerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    int page = 0;
                    int limit = 0;
                    int api = 0;
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


                    IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.ManagerGetFreeGrahok(managerId, fiderId, userId, page, limit, api);

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

        
        public ActionResult ManagerGetAllGrahok()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long managerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    long userId = 0;
                    int page = 0;
                    int limit = 0;
                    int api = 0;

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

                    IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.ManagerGetAllGrahok(managerId, fiderId, userId, page, limit, api);

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


        
        public ActionResult ManagerGetAllDeactiveGrahok()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long managerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    int page = 0;
                    int limit = 0;
                    int api = 0;

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

                    IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.ManagerGetAllDeactiveGrahok(managerId, fiderId, userId, page, limit, api);

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

        
        public ActionResult ManagerGetAllActiveGrahok()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;

                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long managerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    int page = 0;
                    int limit = 0;
                    int api = 0;

                    long userId = 0;
                    if (roleId == 2)
                        managerId = 0;
                    else if (roleId == 3)
                        fiderId = 0;
                    else if (roleId == 4) {
                        managerId = 0;
                        fiderId = 0;
                        userId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                    }
                     
                    IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.ManagerGetAllActiveGrahok(managerId, fiderId, userId, page, limit, api);

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


        public ActionResult GetGrahokTableReturnData(long grahokId)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    GrahokTableReturnColumns data = _dishbillDomainService.GetGrahokTableReturnData(grahokId);

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



        public ActionResult GetGrahokDetailsByGrahokId(long grahokId)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    GRAHOK_TABLE data = _dishbillDomainService.GetGrahokDetailsByGrahokId(grahokId);

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

        
        public ActionResult DeDeactiveSelectedGrahok(long grahokId)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    GrahokOperationModel operationMessage = new GrahokOperationModel();
                    operationMessage = _dishbillDomainService.DeDeactiveSelectedGrahok(grahokId);

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



        public ActionResult DeleteSelectedGrahok(long grahokId)
        {
            if ((LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0) && LoggedInUserInfoFromCookie.AppUserRoleId == 2)
            {
                try
                {
                    int qrySuccess = _dishbillDomainService.DeleteSelectedGrahok(grahokId);

                    return Json(qrySuccess, JsonRequestBehavior.AllowGet);
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



        public ActionResult DeActiveSelectedGrahok(long grahokId)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    GrahokOperationModel operationMessage = new GrahokOperationModel();
                    operationMessage = _dishbillDomainService.DeActiveSelectedGrahok(grahokId);

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