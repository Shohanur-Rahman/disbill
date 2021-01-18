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
    public class collectorController : BaseController
    {
        // GET: collector
        public readonly DishbillDomainService _dishbillDomainService;
        
        public collectorController(DishbillDomainService dishbillDomainService)
        {
            _dishbillDomainService = dishbillDomainService;
        }

        public ActionResult Index()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0 &&(LoggedInUserInfoFromCookie.AppUserRoleId==2 || LoggedInUserInfoFromCookie.AppUserRoleId == 3))
            {
                try
                {
                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long mngId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    ViewBag.CollectorId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    ViewBag.AreNameList = new SelectList(_dishbillDomainService.GetAreaNameByManagerIdWithDetault(fiderId), "Id", "Name");
                    ViewBag.ManagerNameList = new SelectList(_dishbillDomainService.GetManagerNameListByFiderId(fiderId), "Id", "Name");
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

        [HttpPost]
        public ActionResult SaveCollectorInformation(UserOperationData mObj)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0 && (LoggedInUserInfoFromCookie.AppUserRoleId == 2 || LoggedInUserInfoFromCookie.AppUserRoleId == 3))
            {
                try
                {
                    UserOperationData operationMessage = new UserOperationData();
                    if (mObj.User.Id <= 0)
                    {
                        long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                        long mngId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                        mObj.User.UserFiderId = fiderId;
                        //mObj.User.UserManagerId = mngId;
                        mObj.User.IsActivated = true;
                        mObj.User.IsAdminApproved = true;
                        mObj.User.CreatedId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                        operationMessage = _dishbillDomainService.SaveManagerInformation(mObj.User, 4);
                        if(operationMessage.isOperationSuccess == true)
                            _dishbillDomainService.SaveCollectorAreaMapper(mObj.area_ids, operationMessage.User.Id);
                    }
                    else {
                        operationMessage.User = mObj.User;
                        mObj.User.CreatedId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                        operationMessage = _dishbillDomainService.UpdateUserProfile(operationMessage, true);
                        if (operationMessage.isOperationSuccess == true)
                            _dishbillDomainService.SaveCollectorAreaMapper(mObj.area_ids, mObj.User.Id);

                    }
                   

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



        public ActionResult ManagerGetCollectorListByManagerId()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0 && (LoggedInUserInfoFromCookie.AppUserRoleId == 2 || LoggedInUserInfoFromCookie.AppUserRoleId == 3))
            {
                try
                {
                    int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;

                    long fiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    long managerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;

                    if (roleId == 2)
                        managerId = 0;
                    if (roleId == 3 || roleId == 4)
                        fiderId = 0;

                    IList<Super_Admin_Get_Manager_List> dataList = _dishbillDomainService.ManagerGetCollectorListByManagerId(managerId, fiderId);

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