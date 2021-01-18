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
    public class managerController : BaseController
    {
        // GET: manager
        public readonly DishbillDomainService _dishbillDomainService;
        public managerController(DishbillDomainService dishbillDomainService)
        {
            _dishbillDomainService = dishbillDomainService;

        }
        public ActionResult Index()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0 && LoggedInUserInfoFromCookie.AppUserRoleId == 2)
            {
                ViewBag.RoleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                ViewBag.FinderId = LoggedInUserInfoFromCookie.AppUserIdInCookie;
                
                return View();

            }
            else
            {
                return RedirectToAction("login", "account");
            }

        }
        
        public ActionResult AdminGetmanagerList()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0 && LoggedInUserInfoFromCookie.AppUserRoleId == 2)
            {
                long loginUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                try
                {
                    IList<Super_Admin_Get_Manager_List> dataList = _dishbillDomainService.AdminGetmanagerList(loginUserId);

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
        public ActionResult SaveManagerInformation(User mObj)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0 && LoggedInUserInfoFromCookie.AppUserRoleId == 2)
            {
                try
                {
                    UserOperationData operationMessage = new UserOperationData();
                    mObj.CreatedId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                    mObj.FathersName = "";
                    mObj.IsActivated = true;
                    mObj.IsAdminApproved = true;
                    operationMessage = _dishbillDomainService.SaveManagerInformation(mObj, 3);

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
        


        public ActionResult DeDeactiveSelectedManager(int managerId)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    UserOperationData operationMessage = new UserOperationData();
                    operationMessage = _dishbillDomainService.DoDeactiveManager(managerId);

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
        


        public ActionResult DeActiveSelectedManager(int managerId)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    UserOperationData operationMessage = new UserOperationData();
                    operationMessage = _dishbillDomainService.DeActiveSelectedManager(managerId);

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