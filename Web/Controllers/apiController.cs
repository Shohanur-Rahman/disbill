using Core.AppService;
using Services.AppServices;
using Services.DataServices.Account;
using Services.Domain;
using Services.Domain.Admin;
using Services.Domain.APIModels;
using Services.Domain.dishbill;
using Services.Domain.Models.User.EditorModel;
using Services.DomainServices.Admin;
using Services.DomainServices.API;
using Services.DomainServices.dishbill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers
{
    public class apiController : BaseController
    {
        // GET: api
        public readonly DishbillDomainService _dishbillDomainService;
        public readonly AccountDataService _accountDataService;
        public readonly APIDomainServices _APIDomainServices;
        private readonly LiveChatMessagesDomainService _liveChatMessagesDomainService;
        public apiController(DishbillDomainService dishbillDomainService, AccountDataService accountDomainService, APIDomainServices APIDomain, LiveChatMessagesDomainService liveChatMessagesDomainService)
        {
            _dishbillDomainService = dishbillDomainService;
            _accountDataService = accountDomainService;
            _APIDomainServices = APIDomain;
            _liveChatMessagesDomainService = liveChatMessagesDomainService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public string get_password()
        {
            try
            {
                string has = Request.QueryString["has"].ToString();
                
                return SimpleCryptService.Factory().Decrypt(has);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User get_user(string key)
        {
            try
            {
                User data = _APIDomainServices.GetUserByTokenKey(key);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult user_login(LoginModel loginModel)
        {
            APIReturnObject data = new APIReturnObject();

            try
            {
                loginSuccessData loginResult = null;
                loginResult = _accountDataService.Login(loginModel.MobileNumber, loginModel.Password);

                if (loginResult != null && loginResult.IsLoginError == false)
                {
                    data.data = loginResult;
                    data.status = APIStatusCodes.success;
                    data.error = null;
                    data.message = loginResult.LoginMessage;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    data.data = null;
                    data.status = APIStatusCodes.warning;
                    data.error = loginResult.LoginMessage;
                    data.message = loginResult.LoginMessage;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                data.status = APIStatusCodes.error;
                data.message = "Coding Error: " + ex.InnerException;
                data.error = ex.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }





        [HttpPost]
        public ActionResult fider_registretion(FiderRegisterModal modalObj)
        {
            APIReturnObject data = new APIReturnObject();
            UserOperationData operationMessage = new UserOperationData();

            try
            {
                User mObj = new User();
                mObj.Name = modalObj.Name;
                mObj.MobileNumber = modalObj.MobileNumber;
                mObj.Email = modalObj.Email;
                mObj.House = modalObj.House;
                mObj.Road = modalObj.Road;
                mObj.PostOffice = modalObj.PostOffice;
                mObj.Thana = modalObj.Thana;
                mObj.Zila = modalObj.Zila;
                mObj.NIDNo = modalObj.NIDNo;
                mObj.ReferenceName = modalObj.ReferenceName;
                mObj.ReferenceMobile = modalObj.ReferenceMobile;
                mObj.CompanySignature = modalObj.CompanySignature;
                mObj.UserManagerId = modalObj.UserManagerId;
                mObj.UserFiderId = modalObj.UserFiderId;
                mObj.IsAdminApproved = modalObj.IsAdminApproved;
                mObj.IsActivated = modalObj.IsActivated;


                operationMessage = _dishbillDomainService.SaveManagerInformation(mObj, 2);
                if (operationMessage.isOperationSuccess == true)
                {
                    data.data = operationMessage;
                    data.status = APIStatusCodes.success;
                    data.error = null;
                    data.message = operationMessage.OperationMessage;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    data.data = null;
                    data.status = APIStatusCodes.warning;
                    data.error = operationMessage.OperationMessage;
                    data.message = operationMessage.OperationMessage;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                data.status = APIStatusCodes.error;
                data.message = "Coding Error: " + ex.InnerException;
                data.error = ex.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult area_names(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        long loginUserId = 0;
                        if (dataObj.managerId != null)
                            loginUserId = dataObj.managerId.Value;
                        else
                            loginUserId = user.Id;

                        IList<GetAreaNameListByManagerId> dataList = _dishbillDomainService.GetAreanameByManagerId(loginUserId);
                        data.data = dataList;
                        if (dataList != null && dataList.Count > 0)
                            data.total_row = dataList.Count;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult get_user_details_by_id(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        long loginUserId = 0;
                        if (dataObj.fiderId != null)
                            loginUserId = dataObj.fiderId.Value;
                        else
                            loginUserId = user.Id;

                        User dataList = _dishbillDomainService.GetUserDataByUserId(dataObj.user_id);

                        data.data = user;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult update_user_profile(FiderRegisterModal modalObj)
        {
            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(modalObj.acces_token) == false)
            {
                UserOperationData operationMessage = new UserOperationData();

                User user = _APIDomainServices.GetUserByTokenKey(modalObj.acces_token);

                if (user != null && user.Id > 0)
                {
                    try
                    {
                        UserOperationData mObj = new UserOperationData();
                        User userData = new User();
                        userData.Id = user.Id;
                        userData.Name = modalObj.Name;
                        userData.Email = modalObj.Email;
                        userData.House = modalObj.House;
                        userData.Road = modalObj.Road;
                        userData.PostOffice = modalObj.PostOffice;
                        userData.Thana = modalObj.Thana;
                        userData.Zila = modalObj.Zila;
                        userData.NIDNo = modalObj.NIDNo;
                        userData.ReferenceName = modalObj.ReferenceName;
                        userData.ReferenceMobile = modalObj.ReferenceMobile;
                        userData.CompanySignature = modalObj.CompanySignature;
                        userData.Avatar = modalObj.Avatar;
                        userData.CompanyLogo = modalObj.CompanyLogo;

                        mObj.User = userData;
                        operationMessage = _dishbillDomainService.UpdateUserProfile(mObj, false);
                        if (operationMessage.isOperationSuccess == true)
                        {
                            data.data = operationMessage;
                            data.status = "success";
                            data.error = null;
                            data.message = operationMessage.OperationMessage;
                            return Json(data, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            data.data = null;
                            data.status = "warning";
                            data.error = operationMessage.OperationMessage;
                            data.message = operationMessage.OperationMessage;
                            return Json(data, JsonRequestBehavior.AllowGet);
                        }


                    }
                    catch (Exception ex)
                    {
                        data.status = APIStatusCodes.error;
                        data.message = "Coding Error: " + ex.InnerException;
                        data.error = ex.Message;
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }
                data.status = APIStatusCodes.invalid;
                data.message = APIStatusCodes.token_not_found;
                data.error = APIStatusCodes.token_error;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult save_manager_collector(FiderRegisterModal modalObj)
        {
            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(modalObj.acces_token) == false)
            {
                UserOperationData operationMessage = new UserOperationData();

                User user = _APIDomainServices.GetUserByTokenKey(modalObj.acces_token);

                if (user != null && user.Id > 0)
                {
                    int roleId = user.RoleId;
                    try
                    {
                        User mObj = new User();
                        mObj.Id = modalObj.Id;
                        mObj.Name = modalObj.Name;
                        mObj.MobileNumber = modalObj.MobileNumber;
                        mObj.Email = modalObj.Email;
                        mObj.House = modalObj.House;
                        mObj.Road = modalObj.Road;
                        mObj.PostOffice = modalObj.PostOffice;
                        mObj.Thana = modalObj.Thana;
                        mObj.Zila = modalObj.Zila;
                        mObj.NIDNo = modalObj.NIDNo;
                        mObj.ReferenceName = modalObj.ReferenceName;
                        mObj.ReferenceMobile = modalObj.ReferenceMobile;
                        mObj.CompanySignature = modalObj.CompanySignature;
                        mObj.UserManagerId = modalObj.UserManagerId;
                        mObj.UserFiderId = modalObj.UserFiderId;
                        mObj.IsAdminApproved = true;
                        mObj.IsActivated = true;
                        mObj.CreatedId = user.Id;
                        mObj.RoleId = modalObj.RoleId;
                        mObj.AreaId = modalObj.AreaId;

                        if (roleId == 3)
                        {
                            mObj.UserFiderId = user.UserFiderId;
                            mObj.UserManagerId = user.Id;
                        }
                        else if (roleId == 2)
                        {
                            mObj.UserFiderId = user.Id;
                        }

                        operationMessage = _dishbillDomainService.SaveManagerInformation(mObj, modalObj.RoleId);
                        if (operationMessage.isOperationSuccess == true && modalObj.RoleId == 4)
                        {
                            _dishbillDomainService.SaveCollectorAreaMapper(modalObj.area_ids, operationMessage.User.Id);
                        }

                        if (operationMessage.isOperationSuccess == true)
                        {
                            data.data = operationMessage;
                            data.status = "success";
                            data.error = null;
                            data.message = operationMessage.OperationMessage;
                            return Json(data, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            data.data = null;
                            data.status = "warning";
                            data.error = operationMessage.OperationMessage;
                            data.message = operationMessage.OperationMessage;
                            return Json(data, JsonRequestBehavior.AllowGet);
                        }


                    }
                    catch (Exception ex)
                    {
                        data.status = APIStatusCodes.error;
                        data.message = "Coding Error: " + ex.InnerException;
                        data.error = ex.Message;
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }
                data.status = APIStatusCodes.invalid;
                data.message = APIStatusCodes.token_not_found;
                data.error = APIStatusCodes.token_error;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public ActionResult forget_password(FiderRegisterModal modalObj)
        {
            APIReturnObject data = new APIReturnObject();
            try
            {
                UserOperationModel operationMessage = new UserOperationModel();
                UserOperationModel mObj = new UserOperationModel();
                mObj.User = new User();
                mObj.User.MobileNumber = modalObj.MobileNumber;

                operationMessage = _accountDataService.UpdatePassword(mObj);

                data.data = operationMessage;
                data.status = APIStatusCodes.success;
                data.error = null;
                data.message = operationMessage.message;
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex) {
                data.status = APIStatusCodes.error;
                data.message = "Coding Error: " + ex.InnerException;
                data.error = ex.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult get_managers(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        long loginUserId = 0;
                        if (dataObj.fiderId != null)
                            loginUserId = dataObj.fiderId.Value;
                        else
                            loginUserId = user.Id;

                        IList<Super_Admin_Get_Manager_List> dataList = _dishbillDomainService.AdminGetmanagerList(loginUserId);
                        data.data = dataList;
                        if (dataList != null && dataList.Count > 0)
                            data.total_row = dataList.Count;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult do_deactive_manager(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        int managerId = 0;
                        if (dataObj.managerId != null)
                            managerId = int.Parse(dataObj.managerId.Value.ToString());

                        UserOperationData operationMessage = new UserOperationData();
                        operationMessage = _dishbillDomainService.DoDeactiveManager(managerId);
                        data.data = operationMessage;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult do_active_manager(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        int managerId = 0;
                        if (dataObj.managerId != null)
                            managerId = int.Parse(dataObj.managerId.Value.ToString());

                        UserOperationData operationMessage = new UserOperationData();
                        operationMessage = _dishbillDomainService.DeActiveSelectedManager(managerId);
                        data.data = operationMessage;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }





        [HttpPost]
        public ActionResult save_area(AreaApiModal dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    bool mode = false;

                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        if (dataObj.Id > 0)
                            mode = true;
                        AreaList mObj = new AreaList();
                        bool isAddSuccess = false;
                        mObj.ManagerId = user.Id;
                        mObj.Name = dataObj.Name;
                        mObj.Id = dataObj.Id;

                        isAddSuccess = _dishbillDomainService.SaveAreaName(mObj, mode);
                        data.data = isAddSuccess;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult get_all_paybill_grahok(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        long mngId = 0;
                        long fiderId = 0;
                        long cltId = 0;
                        if (user.RoleId == 2)
                            fiderId = user.Id;
                        else if (user.RoleId == 3)
                            mngId = user.Id;
                        else if (user.RoleId == 4)
                            cltId = user.Id;

                        IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.GetAllGrahokForPayBill(mngId, fiderId, cltId, dataObj.page, dataObj.limit, 1);

                        TotalGrahokRow totalData = _dishbillDomainService.GetTotalGrahokForPayBill(mngId, fiderId, cltId);

                        data.data = dataList;
                        if (totalData != null && totalData.total_row > 0)
                            data.total_row = totalData.total_row;

                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult get_searched_grahok(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        long mngId = 0;
                        long fiderId = 0;
                        long cltId = 0;
                        if (user.RoleId == 2)
                            fiderId = user.Id;
                        else if (user.RoleId == 3)
                            mngId = user.Id;
                        else if (user.RoleId == 4)
                            cltId = user.Id;

                        IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.GetGrahokListBySearchParam(dataObj.search_text, mngId, fiderId, cltId, dataObj.page, dataObj.limit);

                        TotalGrahokRow totalData = _dishbillDomainService.GetTotalNumberOfGrahokBySearchParam(dataObj.search_text, mngId, fiderId, cltId);

                        data.data = dataList;
                        if (totalData != null && totalData.total_row > 0)
                            data.total_row = totalData.total_row;

                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult get_this_month_paid_grahok(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        long mngId = 0;
                        long fiderId = 0;
                        long cltId = 0;
                        if (user.RoleId == 2)
                            fiderId = user.Id;
                        else if (user.RoleId == 3)
                            mngId = user.Id;
                        else if (user.RoleId == 4)
                            cltId = user.Id;

                        IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.GetAllBillPaidGrahokThisMonth(mngId, fiderId, cltId, dataObj.page, dataObj.limit, 1);

                        TotalGrahokRow totalData = _dishbillDomainService.GetTotalGrahokForPayBill(mngId, fiderId, cltId);

                        data.data = dataList;
                        if (totalData != null && totalData.total_row > 0)
                            data.total_row = totalData.total_row;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult save_bill_data(APIBillCollectionModal dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        int roleId = user.RoleId;
                        BillOperationModel operationMessage = new BillOperationModel();
                        BillOperationModel mObj = new BillOperationModel();

                        mObj.BillTable = new BillHistoryTable();

                        if (roleId == 4)
                        {
                            mObj.BillTable.CollectorId = user.Id;
                            mObj.BillTable.FiderId = user.UserFiderId;
                            mObj.BillTable.ManagerId = user.UserManagerId;
                        }
                        else if (roleId == 3)
                        {
                            mObj.BillTable.FiderId = user.UserFiderId;
                            mObj.BillTable.ManagerId = user.Id;
                        }
                        else if (roleId == 2)
                        {
                            mObj.BillTable.FiderId = user.Id;
                        }

                        mObj.BillTable.CollectedBy = user.Id;
                        mObj.BillTable.GrahokId = dataObj.GrahokId;
                        mObj.BillTable.BillAmount = dataObj.BillAmount;
                        mObj.BillTable.DueAmount = dataObj.DueAmount;
                        mObj.BillTable.AdvanceAmount = dataObj.AdvanceAmount;
                        mObj.BillTable.BillMonth = dataObj.BillMonth;

                        operationMessage = _dishbillDomainService.SaveBillCollecInformation(mObj, true);
                        if (operationMessage.isOperationSuccess == true)
                        {
                            data.status = APIStatusCodes.success;
                            data.error = null;
                            data.message = "বিল সফল ভাবে প্রদান করা হয়েছে।";
                        }

                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult bill_history(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        long grahokId = dataObj.grahokId;

                        IList<Bill_History_For_Grahok> dataList = _dishbillDomainService.Get_Bill_History_For_Grahok(grahokId);
                        data.data = dataList;
                        if (dataList != null && dataList.Count > 0)
                            data.total_row = dataList.Count;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }







        public ActionResult get_collector(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        int roleId = user.RoleId;
                        long managerId = 0;
                        long fiderId = 0;
                        if (roleId == 3)
                        {
                            managerId = user.Id;
                        }
                        else if (roleId == 2)
                        {
                            fiderId = user.Id;
                        }

                        IList<Super_Admin_Get_Manager_List> dataList = _dishbillDomainService.ManagerGetCollectorListByManagerId(managerId, fiderId);
                        data.data = dataList;


                        if (dataList != null && dataList.Count > 0)
                            data.total_row = dataList.Count;

                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult get_area_details_by_id(APIGetMethodParams dataObj)
        {
            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {

                        AreaList dataList = _dishbillDomainService.GetAreaDetailsByAreaId(dataObj.area_id);
                        data.data = dataList;

                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult get_user_details(APIGetMethodParams dataObj)
        {
            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {

                        data.data = user;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult get_fiderList(APIGetMethodParams dataObj)
        {
            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        int roleId = user.RoleId;
                        long managerId = 0;
                        long fiderId = 0;
                        if (roleId == 3)
                        {
                            managerId = user.Id;
                        }
                        else if (roleId == 2)
                        {
                            fiderId = user.Id;
                        }
                        IList<Super_Admin_Get_Manager_List> dataList = _dishbillDomainService.SuperAdminGetFiderList(managerId, roleId);
                        data.data = dataList;
                        if (dataList != null && dataList.Count > 0)
                            data.total_row = dataList.Count;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult delete_grahok(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if ((user != null && user.Id > 0) && (user.RoleId == 2))
                    {
                        int qrySuccess = _dishbillDomainService.DeleteSelectedGrahok(dataObj.grahokId);
                        data.data = qrySuccess;
                        data.status = APIStatusCodes.success;
                        data.error = null;

                        data.message = "গ্রাহক সফলভাবে ডিলিট করা হয়েছে।";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult save_grahok(GraholAPIModal dataObj)
        {
            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                GrahokOperationModel operationMessage = new GrahokOperationModel();

                User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);

                if (user != null && user.Id > 0)
                {
                    int roleId = user.RoleId;
                    try
                    {
                        GrahokOperationModel mObj = new GrahokOperationModel();
                        //GRAHOK_TABLE grahoktable = new GRAHOK_TABLE();
                        mObj.GrahokTable = new GRAHOK_TABLE();
                        mObj.GrahokTable.Id = dataObj.Id;
                        if (string.IsNullOrEmpty(dataObj.Name) == false)
                            mObj.GrahokTable.Name = dataObj.Name;
                        if (dataObj.LineRate != null)
                            mObj.GrahokTable.LineRate = dataObj.LineRate;
                        if (dataObj.DueAmount != null)
                            mObj.GrahokTable.DueAmount = dataObj.DueAmount;
                        if (dataObj.AdvanceAmount != null)
                            mObj.GrahokTable.AdvanceAmount = dataObj.AdvanceAmount;
                        //if (dataObj.FiderId != null)
                        //    mObj.GrahokTable.FiderId = dataObj.FiderId;
                        //if (dataObj.ManagerId != null)
                        //    mObj.GrahokTable.ManagerId = dataObj.ManagerId;


                        if (roleId == 3)
                        {
                            mObj.GrahokTable.FiderId = user.UserFiderId;
                            mObj.GrahokTable.ManagerId = user.Id;
                        }
                        else if (roleId == 2)
                        {
                            mObj.GrahokTable.FiderId = user.Id;
                        }


                        if (dataObj.HouseOwnerName != null)
                            mObj.GrahokTable.HouseOwnerName = dataObj.HouseOwnerName;
                        if (dataObj.GrahokId != null)
                            mObj.GrahokTable.GrahokId = dataObj.GrahokId;
                        if (dataObj.NumberOfTV != null)
                            mObj.GrahokTable.NumberOfTV = dataObj.NumberOfTV;
                        if (dataObj.GrahokRoleId != null)
                            mObj.GrahokTable.GrahokRoleId = dataObj.GrahokRoleId;

                        mObj.GrahokTable.IsActivated = dataObj.IsActivated;
                        if (dataObj.CompanyName != null)
                            mObj.GrahokTable.CompanyName = dataObj.CompanyName;
                        if (dataObj.AreaId != null)
                            mObj.GrahokTable.AreaId = dataObj.AreaId;
                        if (dataObj.MobileNumber != null)
                            mObj.GrahokTable.MobileNumber = dataObj.MobileNumber;
                        if (dataObj.Email != null)
                            mObj.GrahokTable.Email = dataObj.Email;
                        if (dataObj.House != null)
                            mObj.GrahokTable.House = dataObj.House;
                        if (dataObj.Road != null)
                            mObj.GrahokTable.Road = dataObj.Road;
                        if (dataObj.PostOffice != null)
                            mObj.GrahokTable.PostOffice = dataObj.PostOffice;
                        if (dataObj.Thana != null)
                            mObj.GrahokTable.Thana = dataObj.Thana;
                        if (dataObj.Zila != null)
                            mObj.GrahokTable.Zila = dataObj.Zila;
                        if (dataObj.NIDNo != null)
                            mObj.GrahokTable.NIDNo = dataObj.NIDNo;
                        if (dataObj.ReferenceName != null)
                            mObj.GrahokTable.ReferenceName = dataObj.ReferenceName;
                        if (dataObj.ReferenceMobile != null)
                            mObj.GrahokTable.ReferenceMobile = dataObj.ReferenceMobile;

                        mObj.GrahokTable.CreatedId = user.Id;


                        operationMessage = _dishbillDomainService.SaveGrahokInformation(mObj, dataObj.GrahokRoleId.Value);

                        if (operationMessage.isOperationSuccess == true)
                        {
                            data.data = operationMessage;
                            data.status = "success";
                            data.error = null;
                            data.message = operationMessage.OperationMessage;
                            return Json(data, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            data.data = null;
                            data.status = "warning";
                            data.error = operationMessage.OperationMessage;
                            data.message = operationMessage.OperationMessage;
                            return Json(data, JsonRequestBehavior.AllowGet);
                        }


                    }
                    catch (Exception ex)
                    {
                        data.status = APIStatusCodes.error;
                        data.message = "Coding Error: " + ex.InnerException;
                        data.error = ex.Message;
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }
                data.status = APIStatusCodes.invalid;
                data.message = APIStatusCodes.token_not_found;
                data.error = APIStatusCodes.token_error;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult get_due_grahok(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        int roleId = user.RoleId;
                        long managerId = 0;
                        long fiderId = 0;
                        long cltId = 0;
                        if (roleId == 3)
                        {
                            managerId = user.Id;
                        }
                        else if (roleId == 2)
                        {
                            fiderId = user.Id;
                        }
                        else if (roleId == 2)
                        {
                            cltId = user.Id;
                        }
                        IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.ManagerGetDueAmountGrahokList(managerId, fiderId, cltId, dataObj.page, dataObj.limit, 1);

                        TotalGrahokRow totalRow = _dishbillDomainService.ManagerGetTotalDueAmountGrahokList(managerId, fiderId, cltId);

                        data.data = dataList;
                        if (totalRow != null && totalRow.total_row > 0)
                            data.total_row = totalRow.total_row;

                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult get_free_grahok(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        int roleId = user.RoleId;
                        long managerId = 0;
                        long fiderId = 0;
                        long cltId = 0;
                        if (roleId == 3)
                        {
                            managerId = user.Id;
                        }
                        else if (roleId == 2)
                        {
                            fiderId = user.Id;
                        }
                        else if (roleId == 4)
                        {
                            cltId = user.Id;
                        }
                        IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.ManagerGetFreeGrahok(managerId, fiderId, cltId, dataObj.page, dataObj.limit, 1);

                        TotalGrahokRow totalRow = _dishbillDomainService.ManagerGetTotalFreeGrahok(managerId, fiderId, cltId);

                        data.data = dataList;
                        if (totalRow != null && totalRow.total_row > 0)
                            data.total_row = totalRow.total_row;

                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }





        public ActionResult get_all_grahok(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        int roleId = user.RoleId;
                        long managerId = 0;
                        long fiderId = 0;
                        long cltId = 0;
                        if (roleId == 3)
                        {
                            managerId = user.Id;
                        }
                        else if (roleId == 2)
                        {
                            fiderId = user.Id;
                        }
                        else if (roleId == 4)
                        {
                            cltId = user.Id;
                        }
                        IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.ManagerGetAllGrahok(managerId, fiderId, cltId, dataObj.page, dataObj.limit, 1);

                        TotalGrahokRow totalRow = _dishbillDomainService.ManagerGetTotalGrahokNumber(managerId, fiderId, cltId);

                        data.data = dataList;
                        if (totalRow != null && totalRow.total_row > 0)
                            data.total_row = totalRow.total_row;

                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult get_deactive_grahok(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        int roleId = user.RoleId;
                        long managerId = 0;
                        long fiderId = 0;
                        long cltId = 0;
                        if (roleId == 3)
                        {
                            managerId = user.Id;
                        }
                        else if (roleId == 2)
                        {
                            fiderId = user.Id;
                        }
                        else if (roleId == 4)
                        {
                            cltId = user.Id;
                        }
                        IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.ManagerGetAllDeactiveGrahok(managerId, fiderId, cltId, dataObj.page, dataObj.limit, 1);

                        TotalGrahokRow totalRow = _dishbillDomainService.ManagerGetTotalDeactiveGrahok(managerId, fiderId, cltId);

                        data.data = dataList;
                        if (totalRow != null && totalRow.total_row > 0)
                            data.total_row = totalRow.total_row;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult get_active_grahok(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        int roleId = user.RoleId;
                        long managerId = 0;
                        long fiderId = 0;
                        long cltId = 0;
                        if (roleId == 3)
                        {
                            managerId = user.Id;
                        }
                        else if (roleId == 2)
                        {
                            fiderId = user.Id;
                        }
                        else if (roleId == 4)
                        {
                            cltId = user.Id;
                        }
                        IList<GrahokTableReturnColumns> dataList = _dishbillDomainService.ManagerGetAllActiveGrahok(managerId, fiderId, cltId, dataObj.page, dataObj.limit, 1);

                        TotalGrahokRow totalRow = _dishbillDomainService.ManagerGetTotalActiveGrahok(managerId, fiderId, cltId);

                        data.data = dataList;
                        if (totalRow != null && totalRow.total_row > 0)
                            data.total_row = totalRow.total_row;

                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult get_grahok_details(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    long grahokId = 0;
                    grahokId = dataObj.grahokId;
                    if (user != null && user.Id > 0)
                    {
                        GRAHOK_TABLE dataList = _dishbillDomainService.GetGrahokDetailsByGrahokId(grahokId);
                        data.data = dataList;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult do_deactive_grahok(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    long grahokId = 0;
                    grahokId = dataObj.grahokId;
                    if (user != null && user.Id > 0)
                    {
                        GrahokOperationModel operationMessage = new GrahokOperationModel();
                        operationMessage = _dishbillDomainService.DeDeactiveSelectedGrahok(grahokId);
                        data.data = operationMessage;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult do_active_grahok(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    long grahokId = 0;
                    grahokId = dataObj.grahokId;
                    if (user != null && user.Id > 0)
                    {
                        GrahokOperationModel operationMessage = new GrahokOperationModel();
                        operationMessage = _dishbillDomainService.DeActiveSelectedGrahok(grahokId);
                        data.data = operationMessage;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult get_notification(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    long grahokId = 0;
                    grahokId = dataObj.grahokId;
                    if (user != null && user.Id > 0)
                    {
                        IList<NotificationReturnData> dataList = _dishbillDomainService.GetNotificationListByUserId(user.Id, user.RoleId);
                        data.data = dataList;
                        if (dataList != null && dataList.Count > 0)
                            data.total_row = dataList.Count;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }





        public ActionResult update_notification(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);

                    if (user != null && user.Id > 0)
                    {
                        int status = _dishbillDomainService.UpdateNotificationAsSeen(dataObj.notififcation_id);
                        data.data = status;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult update_password(APIUpdatePasswordModal dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);

                    if (user != null && user.Id > 0)
                    {
                        UserOperationData mObj = new UserOperationData();
                        mObj.User = new User();
                        UserOperationData opMessage = new UserOperationData();
                        mObj.User.Id = user.Id;
                        mObj.User = user;
                        mObj.User.Password = dataObj.password;
                        opMessage = _dishbillDomainService.UpdateChangePassword(mObj, false);
                        data.data = opMessage;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }





        public ActionResult collector_list(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);

                    if (user != null && user.Id > 0)
                    {
                        IList<UserNameAndIdList> dataList = _dishbillDomainService.GetCollectorNamesAndIdListByRoleAndUserId(user.Id, user.RoleId);
                        data.data = dataList;
                        if (dataList != null && dataList.Count > 0)
                            data.total_row = dataList.Count;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult manager_name_list(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);

                    if (user != null && user.Id > 0)
                    {
                        IList<UserNameAndIdList> dataList = _dishbillDomainService.GetManagerNameListByFiderId(user.Id);
                        data.data = dataList;
                        if (dataList != null && dataList.Count > 0)
                            data.total_row = dataList.Count;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult monthly_bill_history(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);

                    if (user != null && user.Id > 0)
                    {
                        BilHistory_For_Manager_Fider_Collector dataList = _dishbillDomainService.GetBillHistoryDataByRoleAndId(user.RoleId, user.Id);
                        data.data = dataList;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult daily_bill_history(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);

                    if (user != null && user.Id > 0)
                    {
                        BilHistory_For_Manager_Fider_Collector dataList = _dishbillDomainService.GetDailyBillHistoryDataByRoleAndId(user.RoleId, user.Id);
                        data.data = dataList;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }





        public ActionResult filtered_bill_history(APIReportFilterParam dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);

                    if (user != null && user.Id > 0)
                    {
                        IList<Get_Bill_Collection_History> dataList = _dishbillDomainService.GetBillCollectionHistory(dataObj.fider_id, dataObj.manager_id, dataObj.collector_id, dataObj.start_date, dataObj.end_date, dataObj.page, dataObj.limit, 1);
                        data.data = dataList;

                        TotalGrahokRow totalData = _dishbillDomainService.GetTotalBillCollectionHistory(dataObj.fider_id, dataObj.manager_id, dataObj.collector_id, dataObj.start_date, dataObj.end_date);

                        if (totalData != null && totalData.total_row > 0)
                            data.total_row = totalData.total_row;

                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult filtered_bill_history_summary(APIReportFilterParam dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);

                    if (user != null && user.Id > 0)
                    {
                        BilHistory_For_Manager_Fider_Collector dataList = _dishbillDomainService.GetDailyBillHistoryDataByRoleAndIdAndDate(dataObj.fider_id, dataObj.manager_id, dataObj.collector_id, dataObj.start_date, dataObj.end_date);
                        data.data = dataList;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult get_admin()
        {
            if (string.IsNullOrEmpty(Request.QueryString["apiKey"]) == false && Request.QueryString["apiKey"] == "ed20XCode1")
            {
                IList<AdminAPIModel> data = _dishbillDomainService.GetAPIAdminList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json("Invalid access", JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult dashboard_summary(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        long managerId = 0;
                        long fiderId = 0;
                        long cltId = 0;
                        if (user.RoleId == 3)
                        {
                            managerId = user.Id;
                        }
                        else if (user.RoleId == 2)
                        {
                            fiderId = user.Id;
                        }
                        else if (user.RoleId == 4)
                        {
                            cltId = user.Id;
                        }

                        APIDashboardSumamry dataList = _dishbillDomainService.dashboard_summary(managerId, fiderId, cltId);
                        data.data = dataList;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult send_sms_to_all(SMSOperationModel dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        SMSOperationModel mObj = new SMSOperationModel();
                        mObj.Message = new Messages();
                        SMSOperationModel operationMessage = new SMSOperationModel();
                        mObj.LoginUserId = user.Id;
                        mObj.UserRole = user.RoleId;
                        mObj.MobileNumberList = dataObj.MobileNumberList;
                        mObj.SendMode = dataObj.SendMode;
                        mObj.Message.message = dataObj.message_text;
                        operationMessage = _dishbillDomainService.SendSMSToAllUser(mObj);

                        data.data = operationMessage;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public ActionResult paid_collected_bill(BillPaidAPIModel dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        BillPaidOperationModel isAddSuccess;
                        BillPaidTable mObj = new BillPaidTable();

                        int roleId = user.RoleId;
                        if (roleId == 4)
                        {
                            mObj.ManagerApproved = false;
                            mObj.ManagerId = user.UserManagerId;
                        }
                        else if (roleId == 3)
                        {
                            mObj.FiderApproved = false;
                            mObj.FiderId = user.UserFiderId;
                        }

                        mObj.CollectedBill = dataObj.CollectedBill;
                        mObj.DueAmount = dataObj.DueAmount;
                        mObj.SubmitedId = user.Id;

                        isAddSuccess = _dishbillDomainService.PaidAmountToManagerAndFider(mObj);

                        data.data = isAddSuccess;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "আপনার টাকা সফলভাবে জমা দেয়া হয়েছে।";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }





        public ActionResult get_my_received_bill(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        long mngId = 0;
                        long fiderId = 0;
                        if (user.RoleId == 2)
                            fiderId = user.Id;
                        else if (user.RoleId == 3)
                            mngId = user.Id;

                        IList<BillPaidSummary> dataList = _dishbillDomainService.GetMyReceivedBill(mngId, fiderId, dataObj.page, dataObj.limit, 1);

                        TotalGrahokRow totalData = _dishbillDomainService.GetTotalMyReceivedBill(mngId, fiderId);

                        data.data = dataList;
                        if (totalData != null && totalData.total_row > 0)
                            data.total_row = totalData.total_row;

                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult get_my_paid_bill(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if ((user != null && user.Id > 0) && (user.RoleId == 3 || user.RoleId == 4))
                    {
                        long mngId = 0;
                        long fiderId = 0;
                        if (user.RoleId == 2)
                            fiderId = user.Id;
                        else if (user.RoleId == 3)
                            mngId = user.Id;

                        IList<BillPaidSummary> dataList = _dishbillDomainService.GetMyPaidBill(user.Id, dataObj.page, dataObj.limit, 1);

                        TotalGrahokRow totalData = _dishbillDomainService.GetMyTotalPaidBill(user.Id);

                        data.data = dataList;
                        if (totalData != null && totalData.total_row > 0)
                            data.total_row = totalData.total_row;

                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }


        #region "Live chat API"



        public ActionResult get_chat_requests(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {

                        IList<Get_LiveChatMessageListFromAdmin> dataList = _liveChatMessagesDomainService.GetAllChatMessageFromAdmin(user.Id);

                        TotalGrahokRow totalData = _dishbillDomainService.GetMyTotalPaidBill(user.Id);

                        data.data = dataList;
                        if (dataList != null && dataList.Count > 0)
                            data.total_row = dataList.Count;

                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult send_live_message(APILIVECHATMODAL dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);

                    if (user != null && user.Id > 0)
                    {
                        LiveChatMessages mObj = new LiveChatMessages();
                        bool isAddSuccess = false;
                        long adminUserId = 0;
                        long receiverId = 0;
                        int roleId = user.RoleId;
                        adminUserId = user.Id;

                        if (roleId == 3)
                            receiverId = user.UserFiderId.Value;
                        else if (roleId == 4)
                            receiverId = user.UserManagerId.Value;


                        if (string.IsNullOrEmpty(dataObj.User_Message) == false)
                        {
                            mObj.IP_Address = GetIPAddress();
                            mObj.UserId = adminUserId;
                            mObj.ReceiverId = receiverId;
                            mObj.User_Message = dataObj.User_Message;
                            isAddSuccess = _liveChatMessagesDomainService.SaveLiveChatMessages(mObj, adminUserId, false);

                        } else if (string.IsNullOrEmpty(dataObj.ReplyMessage) == false) {
                            mObj.ReplyMessage = dataObj.ReplyMessage;
                            mObj.IP_Address = dataObj.IP_Address;
                            mObj.ReceiverId = dataObj.ReceiverId;
                            mObj.IsRead = true;
                            mObj.ReaderId = adminUserId;
                            isAddSuccess = _liveChatMessagesDomainService.SaveLiveChatMessages(mObj, adminUserId, true);
                        }

                        
                        data.data = isAddSuccess;
                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.invalid;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult get_chat_history(APIGetMethodParams dataObj)
        {

            APIReturnObject data = new APIReturnObject();
            if (string.IsNullOrEmpty(dataObj.acces_token) == false)
            {
                try
                {
                    User user = _APIDomainServices.GetUserByTokenKey(dataObj.acces_token);
                    if (user != null && user.Id > 0)
                    {
                        long receiverId = 0;
                        int roleId = user.RoleId;
                        string ipAddress = "";

                        if (roleId == 3)
                            receiverId = user.UserFiderId.Value;
                        else if (roleId == 4)
                            receiverId = user.UserManagerId.Value;

                        if (string.IsNullOrEmpty(dataObj.messge_ip) == false)
                        {
                            ipAddress = dataObj.messge_ip;
                        }
                        else {
                            ipAddress = GetIPAddress();
                        }
                         
                        IList<Get_LiveChatMessagesByIpAddress> chatList = _liveChatMessagesDomainService.GetLiveChatMessagesByIpAddress(ipAddress, receiverId);

                        data.data = chatList;
                        if (chatList != null && chatList.Count > 0)
                            data.total_row = chatList.Count;

                        data.status = APIStatusCodes.success;
                        data.error = null;
                        data.message = "Data returned successfully.";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }
                    data.status = APIStatusCodes.invalid;
                    data.message = APIStatusCodes.token_not_found;
                    data.error = APIStatusCodes.token_error;
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    data.status = APIStatusCodes.error;
                    data.message = "Coding Error: " + ex.InnerException;
                    data.error = ex.Message;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.status = APIStatusCodes.fail;
                data.error = APIStatusCodes.token_not_found;
                data.message = APIStatusCodes.access_permission;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }





        #endregion




    }
}