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
    public class fiderController : BaseController
    {
        // GET: fider
        public readonly DishbillDomainService _dishbillDomainService;
        public fiderController(DishbillDomainService dishbillDomainService)
        {
            _dishbillDomainService = dishbillDomainService;

        }
        public ActionResult Index()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0 && (LoggedInUserInfoFromCookie.AppUserRoleId == 1 || LoggedInUserInfoFromCookie.AppUserRoleId == 2))
            {
                try
                {
                    ViewBag.UserRoleId = LoggedInUserInfoFromCookie.AppUserRoleId;
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
        public ActionResult SaveFiderInformation(User mObj)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0 && (LoggedInUserInfoFromCookie.AppUserRoleId==1 || LoggedInUserInfoFromCookie.AppUserRoleId == 2))
            {
                try
                {
                    UserOperationData operationMessage = new UserOperationData();
                    mObj.CreatedId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                    if (LoggedInUserInfoFromCookie.AppUserRoleId == 2)
                    {
                        mObj.IsAdminApproved = false;
                        mObj.IsActivated = false;
                    }
                        
                    operationMessage = _dishbillDomainService.SaveManagerInformation(mObj, 2);

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

        //[HttpPost]
        //public ActionResult RegisterNewFider(User mObj)
        //{
        //    try
        //    {
        //        UserOperationData operationMessage = new UserOperationData();
        //        mObj.CreatedId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
        //        mObj.IsAdminApproved = true;
        //        mObj.IsActivated = true;
        //        mObj.FathersName = "";
        //        operationMessage = _dishbillDomainService.SaveManagerInformation(mObj, 2);
        //        if (operationMessage.isOperationSuccess == true)
        //        {
        //            MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, operationMessage.OperationMessage);

        //        }
        //        else
        //        {
        //            MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, operationMessage.OperationMessage);
        //        }

        //        return RedirectToAction("registerresult", "account");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        
        public ActionResult SuperAdminGetFiderList()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                long mngId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                try
                {
                    int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    IList<Super_Admin_Get_Manager_List> dataList = _dishbillDomainService.SuperAdminGetFiderList(mngId, roleId);

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