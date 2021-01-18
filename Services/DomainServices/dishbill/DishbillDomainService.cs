using Core.DomainService;
using Services.DataServices.dishbill;
using Services.Domain;
using Services.Domain.APIModels;
using Services.Domain.dishbill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainServices.dishbill
{
    public class DishbillDomainService : DomainService<AreaList, long>
    {
        private readonly DishbillDataServices _dishbillDataServices;
        public DishbillDomainService(DishbillDataServices dataService) : base(dataService)
        {
            _dishbillDataServices = dataService;
        }
        public CollectionSummary GetCollectionSummary(long mngId, long fiderId)
        {
            return _dishbillDataServices.GetCollectionSummary(mngId, fiderId);
        }
        public BillPaidOperationModel PaidAmountToManagerAndFider(BillPaidTable mObj)
        {
            return _dishbillDataServices.PaidAmountToManagerAndFider(mObj);
        }
        public SMSOperationModel SendSMSToAllUser(SMSOperationModel mObj)
        {
            return _dishbillDataServices.SendSMSToAllUser(mObj);
        }
        public SMSOperationModel SendSMSToUser(SMSOperationModel mObj)
        {
            return _dishbillDataServices.SendSMSToUser(mObj);
        }

        public bool SaveAreaName(AreaList mObj, bool isAddMode)
        {
            return _dishbillDataServices.SaveAreaName(mObj, isAddMode);
        }
        public UserOperationData SaveManagerInformation(User mObj, int roleId)
        {
            return _dishbillDataServices.SaveManagerInformation(mObj, roleId);
        }
        public bool SaveCollectorAreaMapper(string areaList, long collectorId)
        {
            return _dishbillDataServices.SaveCollectorAreaMapper(areaList, collectorId);
        }
        public UserOperationData DoDeactiveManager(int managerId)
        {
            return _dishbillDataServices.DoDeactiveManager(managerId);
        }
        public UserOperationData DeActiveSelectedManager(int managerId)
        {
            return _dishbillDataServices.DeActiveSelectedManager(managerId);
        }
        public IList<UserNameAndIdList> GetCollectorNamesAndIdListByRoleAndUserId(long mngId, int roleId)
        {
            return _dishbillDataServices.GetCollectorNamesAndIdListByRoleAndUserId(mngId, roleId);
        }
        public IList<GetAreaNameListByManagerId> GetAreanameByManagerId(long mngId)
        {
            return _dishbillDataServices.GetAreanameByManagerId(mngId);
        }

        public IList<UserNameAndIdList> GetManagerNameListByFiderId(long fiderId)
        {
            return _dishbillDataServices.GetManagerNameListByFiderId(fiderId);
        }

        public AreaList GetAreaDetailsByAreaId(int id)
        {
            return _dishbillDataServices.GetAreaDetailsByAreaId(id);
        }
        public IList<GetAreaNameListByManagerId> GetAreaNameByManagerIdWithDetault(long mngId)
        {
            return _dishbillDataServices.GetAreaNameByManagerIdWithDetault(mngId);
        }
        public User GetUserDataByUserId(long mngId)
        {
            return _dishbillDataServices.GetUserDataByUserId(mngId);
        }
        public IList<GrahokAreaMapper> GetCollectorAreaList(long mngId)
        {
            return _dishbillDataServices.GetCollectorAreaList(mngId);
        }
        public IList<User> GetUserListByMobileNumber(string mobileNumber)
        {
            return _dishbillDataServices.GetUserListByMobileNumber(mobileNumber);
        }
        public IList<Super_Admin_Get_Manager_List> AdminGetmanagerList(long loginUserId)
        {
            return _dishbillDataServices.AdminGetmanagerList(loginUserId);
        }
        public IList<BillPaidSummary> GetMyPaidBill(long loginUserId, int page, int limit, int api)
        {
            return _dishbillDataServices.GetMyPaidBill(loginUserId, page, limit, api);
        }

        public TotalGrahokRow GetMyTotalPaidBill(long loginUserId)
        {
            return _dishbillDataServices.GetMyTotalPaidBill(loginUserId);
        }

        public IList<BillPaidSummary> GetMyReceivedBill(long managerId, long fiderId, int page, int limit, int api)
        {
            return _dishbillDataServices.GetMyReceivedBill(managerId, fiderId, page, limit, api);
        }

        public TotalGrahokRow GetTotalMyReceivedBill(long mngId, long fiderId)
        {
            return _dishbillDataServices.GetTotalMyReceivedBill(mngId, fiderId);
        }

        public IList<Super_Admin_Get_Manager_List> ManagerGetCollectorListByManagerId(long mngId, long fiderId)
        {
            return _dishbillDataServices.ManagerGetCollectorListByManagerId(mngId, fiderId);
        }
        public UserOperationData UpdateUserProfile(UserOperationData mObj, bool isAddMode)
        {
            return _dishbillDataServices.UpdateUserProfile(mObj, isAddMode);
        }
        public UserOperationData UpdateChangePassword(UserOperationData mObj, bool isAddMode)
        {
            return _dishbillDataServices.UpdateChangePassword(mObj, isAddMode);
        }

        public GrahokOperationModel SaveGrahokInformation(GrahokOperationModel mObj, int roleId)
        {
            return _dishbillDataServices.SaveGrahokInformation(mObj, roleId);
        }
        public IList<GrahokTableReturnColumns> ManagerGetDueAmountGrahokList(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            return _dishbillDataServices.ManagerGetDueAmountGrahokList(mngId, fiderId, collectorId, page, limit, api);
        }

        public TotalGrahokRow ManagerGetTotalDueAmountGrahokList(long mngId, long fiderId, long collectorId)
        {
            return _dishbillDataServices.ManagerGetTotalDueAmountGrahokList(mngId, fiderId, collectorId);
        }

        public IList<GrahokTableReturnColumns> GetAllGrahokForPayBill(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            return _dishbillDataServices.GetAllGrahokForPayBill(mngId, fiderId, collectorId, page, limit, api);
        }

        public IList<GrahokTableReturnColumns> GetGrahokListBySearchParam(string searchText, long mngId, long fiderId, long collectorId, int page, int limit)
        {
            return _dishbillDataServices.GetGrahokListBySearchParam(searchText, mngId, fiderId, collectorId, page, limit);
        }
        public TotalGrahokRow GetTotalNumberOfGrahokBySearchParam(string searchText, long mngId, long fiderId, long collectorId)
        {
            return _dishbillDataServices.GetTotalNumberOfGrahokBySearchParam(searchText,mngId, fiderId, collectorId);
        }

        public TotalGrahokRow GetTotalGrahokForPayBill(long mngId, long fiderId, long collectorId)
        {
            return _dishbillDataServices.GetTotalGrahokForPayBill(mngId, fiderId, collectorId);
        }

        public IList<GrahokTableReturnColumns> GetAllBillPaidGrahokThisMonth(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            return _dishbillDataServices.GetAllBillPaidGrahokThisMonth(mngId, fiderId, collectorId, page, limit, api);
        }
        public TotalGrahokRow GetTotalGrahokThisMonthPaidBill(long mngId, long fiderId, long collectorId)
        {
            return _dishbillDataServices.GetTotalGrahokThisMonthPaidBill(mngId, fiderId, collectorId);
        }

        public IList<Bill_History_For_Grahok> Get_Bill_History_For_Grahok(long grahokId)
        {
            return _dishbillDataServices.Get_Bill_History_For_Grahok(grahokId);
        }
        public IList<GrahokTableReturnColumns> ManagerGetFreeGrahok(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            return _dishbillDataServices.ManagerGetFreeGrahok(mngId, fiderId,collectorId, page, limit, api);
        }

        public TotalGrahokRow ManagerGetTotalFreeGrahok(long mngId, long fiderId, long collectorId)
        {
            return _dishbillDataServices.ManagerGetTotalFreeGrahok(mngId, fiderId, collectorId);
        }

        public IList<GrahokTableReturnColumns> ManagerGetAllGrahok(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            return _dishbillDataServices.ManagerGetAllGrahok(mngId, fiderId, collectorId, page, limit, api);
        }

        public TotalGrahokRow ManagerGetTotalGrahokNumber(long mngId, long fiderId, long collectorId)
        {
            return _dishbillDataServices.ManagerGetTotalGrahokNumber(mngId, fiderId, collectorId);
        }

        public IList<GrahokTableReturnColumns> ManagerGetAllDeactiveGrahok(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            return _dishbillDataServices.ManagerGetAllDeactiveGrahok(mngId, fiderId, collectorId, page, limit, api);
        }
        public TotalGrahokRow ManagerGetTotalDeactiveGrahok(long mngId, long fiderId, long collectorId)
        {
            return _dishbillDataServices.ManagerGetTotalDeactiveGrahok(mngId, fiderId, collectorId);
        }

        public IList<GrahokTableReturnColumns> ManagerGetAllActiveGrahok(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            return _dishbillDataServices.ManagerGetAllActiveGrahok(mngId, fiderId, collectorId, page, limit, api);
        }

        public TotalGrahokRow ManagerGetTotalActiveGrahok(long mngId, long fiderId, long collectorId)
        {
            return _dishbillDataServices.ManagerGetTotalActiveGrahok(mngId, fiderId, collectorId);
        }

        public GRAHOK_TABLE GetGrahokDetailsByGrahokId(long grahokId)
        {
            return _dishbillDataServices.GetGrahokDetailsByGrahokId(grahokId);
        }

        public GrahokTableReturnColumns GetGrahokTableReturnData(long grahokId)
        {
            return _dishbillDataServices.GetGrahokTableReturnData(grahokId);
        }

        public IList<Super_Admin_Get_Manager_List> SuperAdminGetFiderList(long mngId, int roleId = 0)
        {
            return _dishbillDataServices.SuperAdminGetFiderList(mngId, roleId);
        }

        public GrahokOperationModel DeDeactiveSelectedGrahok(long grahokId)
        {
            return _dishbillDataServices.DeDeactiveSelectedGrahok(grahokId);
        }
        public GrahokOperationModel DeActiveSelectedGrahok(long grahokId)
        {
            return _dishbillDataServices.DeActiveSelectedGrahok(grahokId);
        }
        public int DeleteSelectedGrahok(long grahokId)
        {
            return _dishbillDataServices.DeleteSelectedGrahok(grahokId);
        }

        public BillOperationModel SaveBillCollecInformation(BillOperationModel mObj, bool isAddMode)
        {
            return _dishbillDataServices.SaveBillCollecInformation(mObj, isAddMode);
        }
        public BillCollectOperationModel SaveBillCollectInformation(BillCollectOperationModel mObj, bool isAddMode)
        {
            return _dishbillDataServices.SaveBillCollectInformation(mObj, isAddMode);
        }
        public IList<Messages> GetAllMessageInformation()
        {
            return _dishbillDataServices.GetAllMessageInformation();
        }
        public IList<allSMSMobile> GetAllSMSMobileNumbers(int page, int limit)
        {
            return _dishbillDataServices.GetAllSMSMobileNumbers(page, limit);
        }

        #region"Bill History"
        public BilHistory_For_Manager_Fider_Collector GetBillHistoryDataByRoleAndId(int roleId, long userId)
        {
            return _dishbillDataServices.GetBillHistoryDataByRoleAndId(roleId, userId);
        }
        public BilHistory_For_Manager_Fider_Collector GetDailyBillHistoryDataByRoleAndId(int roleId, long userId)
        {
            return _dishbillDataServices.GetDailyBillHistoryDataByRoleAndId(roleId, userId);
        }
        public BilHistory_For_Manager_Fider_Collector GetDailyBillHistoryDataByRoleAndIdAndDate(long fiderId, long mngId, long cltId, DateTime startDate, DateTime endDate)
        {
            return _dishbillDataServices.GetDailyBillHistoryDataByRoleAndIdAndDate(fiderId, mngId, cltId, startDate, endDate);
        }

        public IList<Get_Bill_Collection_History> GetBillCollectionHistory(long fiderId, long mngId, long cltId, DateTime startDate, DateTime endDate, int page, int limit, int api)
        {
            return _dishbillDataServices.GetBillCollectionHistory(fiderId, mngId, cltId, startDate, endDate, page, limit, api);
        }

        public TotalGrahokRow GetTotalBillCollectionHistory(long fiderId, long mngId, long cltId, DateTime startDate, DateTime endDate)
        {
            return _dishbillDataServices.GetTotalBillCollectionHistory(fiderId, mngId, cltId, startDate, endDate);
        }
        public SMSUpdateStatus UpdateSMS(Messages msg)
        {
            return _dishbillDataServices.UpdateSMS(msg);
        }

        #endregion

        #region"Notification"
        public int GetTotalNotifications(long userId, int roleId)
        {
            return _dishbillDataServices.GetTotalNotifications(userId, roleId);
        }
        public IList<NotificationReturnData> GetNotificationListByUserId(long userId, int roleId = 0)
        {
            return _dishbillDataServices.GetNotificationListByUserId(userId, roleId);
        }
        public int UpdateNotificationAsSeen(int notificationId)
        {
            return _dishbillDataServices.UpdateNotificationAsSeen(notificationId);
        }
        #endregion


        public IList<AdminAPIModel> GetAPIAdminList()
        {
            return _dishbillDataServices.GetAPIAdminList();
        }

        public IList<MonthNames> GetAllMonthsName()
        {
            return _dishbillDataServices.GetAllMonthsName();
        }


        #region "Only From APS"

        public APIDashboardSumamry dashboard_summary(long managerId, long fiderId, long collectorId)
        {
            return _dishbillDataServices.dashboard_summary(managerId, fiderId, collectorId);
        }

        #endregion


    }
}
