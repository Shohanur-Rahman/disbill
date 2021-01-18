using Services.Domain.dishbill;
using Services.DomainServices.dishbill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.AppCode;

namespace Web.Controllers
{
    public class areaController : BaseController
    {
        // GET: area
        public readonly DishbillDomainService _dishbillDomainService;
        public areaController(DishbillDomainService dishbillDomainService)
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


        public ActionResult GetAreaDetailsByAreaId(int id)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                long mngId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                try
                {
                    AreaList dataList = _dishbillDomainService.GetAreaDetailsByAreaId(id);

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


        public ActionResult GetAreaNameList()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                long mngId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                try
                {
                    IList<GetAreaNameListByManagerId> dataList  = _dishbillDomainService.GetAreanameByManagerId(mngId);

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
        public ActionResult SaveAreaName(AreaList mObj)
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0)
            {
                try
                {
                    bool mode = false;
                    bool isAddSuccess = false;
                    mObj.ManagerId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;

                    if (mObj.Id > 0)
                        mode = true;

                    isAddSuccess = _dishbillDomainService.SaveAreaName(mObj, mode);

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