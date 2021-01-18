using Services.Domain.Admin;
using Services.DomainServices.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers.Admin
{
    public class LiveChatMessagesController : BaseController
    {
        // GET: LiveChatMessages
        private readonly LiveChatMessagesDomainService _liveChatMessagesDomainService;
        public LiveChatMessagesController(LiveChatMessagesDomainService liveChatMessagesDomainService)
        {

            _liveChatMessagesDomainService = liveChatMessagesDomainService;
        }


        public ActionResult Index()
        {
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null && LoggedInUserInfoFromCookie.AppUserIdInCookie.Value > 0 && LoggedInUserInfoFromCookie.AppUserRoleId < 4)
            {
                try
                {
                    long userId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
                    ViewBag.AllLiveMessage = _liveChatMessagesDomainService.GetAllChatMessageFromAdmin(userId);
                    return View("~/Views/LiveChatMessages/LiveChatMessageList.cshtml");
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
        [ValidateAntiForgeryToken]
        public ActionResult SaveLiveChatMessages(LiveChatMessages mObj)
        {
            try
            {
                bool isAddSuccess = false;
                long adminUserId = 0;
                long receiverId = 0;
                int roleId = LoggedInUserInfoFromCookie.AppUserRoleId;
                if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null)
                    adminUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;

                if (roleId == 3)
                    receiverId = LoggedInUserInfoFromCookie.UserFiderIdInCookie.Value;
                else if (roleId == 4)
                    receiverId= LoggedInUserInfoFromCookie.UserManagerIdInCookie.Value;


                if (mObj != null)
                {
                    mObj.IP_Address = GetIPAddress();
                    mObj.UserId = adminUserId;
                    mObj.ReceiverId = receiverId;
                    isAddSuccess = _liveChatMessagesDomainService.SaveLiveChatMessages(mObj, adminUserId, false);
                }

                return Json(isAddSuccess, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReplyLiveChatMessages(LiveChatMessages mObj)
        {
            try
            {
                bool isAddSuccess = false;
                long adminUserId = 0;
                if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null)
                    adminUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;

                if (mObj != null)
                {
                    mObj.ReaderId = adminUserId;
                    mObj.IsRead = true;
                    isAddSuccess = _liveChatMessagesDomainService.SaveLiveChatMessages(mObj, adminUserId, true);
                }

                return Json(isAddSuccess, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public ActionResult GetLiveChatMessagesByIpAddress(long receiverId)
        {
            string ipAddress = GetIPAddress();
            IList<Get_LiveChatMessagesByIpAddress> rolesList = _liveChatMessagesDomainService.GetLiveChatMessagesByIpAddress(ipAddress, receiverId);

            return Json(rolesList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLiveChatMessagesByIpAddressForAdmin(string ipAddress, long receiverId)
        {
            if(string.IsNullOrEmpty(ipAddress) == true)
                ipAddress= GetIPAddress();

            IList<Get_LiveChatMessagesByIpAddress> rolesList = _liveChatMessagesDomainService.GetLiveChatMessagesByIpAddress(ipAddress, receiverId);

            return Json(rolesList, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult ReplyMessage()
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"])==false && string.IsNullOrEmpty(Request.QueryString["rid"])==false)
            {
                ViewBag.ConversastionId = Request.QueryString["id"];
                ViewBag.ReciverId = Request.QueryString["rid"];
                ViewBag.YourId = Request.QueryString["rid"];
            }
            return View("~/Views/LiveChatMessages/ReplyMessage.cshtml");
        }

    }
}