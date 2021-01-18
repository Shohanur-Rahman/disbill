using Services.DomainServices.dishbill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers
{
    public class helpController : BaseController
    {
        // GET: help
        public readonly DishbillDomainService _dishbillDomainService;
        public helpController(DishbillDomainService dishbillDomainService)
        {
            _dishbillDomainService = dishbillDomainService;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}