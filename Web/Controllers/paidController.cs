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
    public class paidController : Controller
    {
        // GET: paid
        public readonly DishbillDomainService _dishbillDomainService;
        public paidController(DishbillDomainService dishbillDomainService)
        {
            _dishbillDomainService = dishbillDomainService;
        }

        public ActionResult Index()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                long loginUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                ViewBag.UserRoleId = roleId;

                if(roleId ==3)
                    ViewBag.MyReceivedBill = _dishbillDomainService.GetMyReceivedBill(loginUserId,0,0,0,0);
                else if(roleId ==2)
                    ViewBag.MyReceivedBill = _dishbillDomainService.GetMyReceivedBill(0, loginUserId,0, 0, 0);

                if (roleId == 3 || roleId == 4)
                    ViewBag.MyPaidBill = _dishbillDomainService.GetMyPaidBill(loginUserId,0,0,0);

                return View();

            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }


        [HttpPost]
        public ActionResult PaidAmountToManagerAndFider(BillPaidAPIModel dataObj)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    BillPaidOperationModel isAddSuccess;
                    BillPaidTable mObj = new BillPaidTable();

                    int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                    if (roleId == 4)
                    {
                        mObj.ManagerApproved = false;
                        mObj.ManagerId = LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;
                    }
                    else if (roleId == 3) {
                        mObj.FiderApproved = false;
                        mObj.FiderId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                    }

                    mObj.CollectedBill = dataObj.CollectedBill;
                    mObj.DueAmount = dataObj.DueAmount;
                    mObj.SubmitedId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;

                    isAddSuccess = _dishbillDomainService.PaidAmountToManagerAndFider(mObj);

                    return Json(isAddSuccess, JsonRequestBehavior.AllowGet);
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