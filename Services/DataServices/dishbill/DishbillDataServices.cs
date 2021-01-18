using Core.DataService;
using Services.AppServices;
using Services.DataContext;
using Services.Domain;
using Services.Domain.APIModels;
using Services.Domain.dishbill;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices.dishbill
{
    public class DishbillDataServices : EntityContextDataService<AreaList>
    {
        public DishbillDataServices(AppDbContext dbContext) : base(dbContext)
        {
            DbContext.Database.CommandTimeout = SiteSettings.DbTimeOut;
        }


        public BillPaidOperationModel PaidAmountToManagerAndFider(BillPaidTable mObj)
        {
            BillPaidOperationModel opModel = new BillPaidOperationModel();
            try
            {
                if (mObj != null)
                {
                    mObj.SubmitedDate = DateTime.Now;
                    DbContext.Set<BillPaidTable>().Add(mObj);
                    DbContext.SaveChanges();

                    opModel.Billpaid = mObj;
                    opModel.isOperationSuccess = true;
                    opModel.OperationMessage = "আপনার টাকা সফলভাবে জমা দেয়া হয়েছে।";
                    return opModel;
                }

                //Assign Return Data
                opModel.Billpaid = mObj;
                opModel.isOperationSuccess = false;
                opModel.OperationMessage = "Something wrong, Contact to your owner!";
                return opModel;

            }
            catch (Exception ex)
            {
                string loginMessage = ex.Message.ToString();
                opModel.OperationMessage = loginMessage;
                opModel.error = loginMessage;
                return opModel;
            }

        }

        public CollectionSummary GetCollectionSummary(long mngId, long fiderId)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                CollectionSummary dataList = DbContext.Database.SqlQuery<CollectionSummary>("Get_Bill_Collection_Summary @mngId,@fiderId", prmManagerId, prmFiderId).FirstOrDefault();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SMSOperationModel SendSMSToAllUser(SMSOperationModel mObj)
        {
            SMSOperationModel rData = new SMSOperationModel();
            try
            {
                string smsMode = mObj.SendMode;

                if (smsMode.Trim() == "grk")
                {
                    //IList<GRAHOK_TABLE> data = DbContext.Set<GRAHOK_TABLE>().ToList();
                    IList<GRAHOK_TABLE> data;
                    if (mObj.UserRole == 3)
                    {
                        data = DbContext.Set<GRAHOK_TABLE>().Where(x => (x.ManagerId == mObj.LoginUserId)).ToList();
                    } 
                    else
                    {
                        data = DbContext.Set<GRAHOK_TABLE>().Where(x => (x.FiderId == mObj.LoginUserId)).ToList();
                    }

                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (string.IsNullOrEmpty(data[i].MobileNumber) == false)
                            {
                                Messages msgObj = new Messages();
                                msgObj.mobile = data[i].MobileNumber;
                                msgObj.message = mObj.Message.message;
                                msgObj.status = 0;
                                msgObj.local = 0;
                                DbContext.Set<Messages>().Add(msgObj);
                                DbContext.SaveChanges();
                            }
                        }
                    }
                }
                else if (smsMode.Trim() == "clt")
                {
                    IList<User> data;
                    if (mObj.UserRole == 3)
                    {
                        data = DbContext.Set<User>().Where(x => (x.RoleId == 4) && (x.UserManagerId == mObj.LoginUserId)).ToList();
                    }
                    else
                    {
                        data = DbContext.Set<User>().Where(x => (x.RoleId == 4) && (x.UserFiderId == mObj.LoginUserId)).ToList();
                    }

                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (string.IsNullOrEmpty(data[i].MobileNumber) == false)
                            {
                                Messages msgObj = new Messages();
                                msgObj.mobile = data[i].MobileNumber;
                                msgObj.message = mObj.Message.message;
                                msgObj.status = 0;
                                msgObj.local = 0;
                                DbContext.Set<Messages>().Add(msgObj);
                                DbContext.SaveChanges();
                            }
                        }
                    }
                }
                else if (smsMode.Trim() == "mng")
                {

                    IList<User> data = DbContext.Set<User>().Where(x => (x.RoleId == 3) && (x.UserFiderId == mObj.LoginUserId)).ToList();

                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (string.IsNullOrEmpty(data[i].MobileNumber) == false)
                            {
                                Messages msgObj = new Messages();
                                msgObj.mobile = data[i].MobileNumber;
                                msgObj.message = mObj.Message.message;
                                msgObj.status = 0;
                                msgObj.local = 0;
                                DbContext.Set<Messages>().Add(msgObj);
                                DbContext.SaveChanges();
                            }
                        }
                    }

                }

                else if (smsMode.Trim() == "ag" && string.IsNullOrEmpty(mObj.MobileNumberList) == false)
                {
                    string[] data = mObj.MobileNumberList.Split(',');

                    if (data != null && data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            if (string.IsNullOrEmpty(data[i]) == false)
                            {
                                Messages msgObj = new Messages();
                                msgObj.mobile = data[i].Trim();
                                msgObj.message = mObj.Message.message;
                                msgObj.status = 0;
                                msgObj.local = 0;
                                DbContext.Set<Messages>().Add(msgObj);
                                DbContext.SaveChanges();
                            }
                        }
                    }

                }


                rData.OperationMessage = "SMS সফল ভাবে সেন্ট করা হয়েছে।";
                rData.isOperationSuccess = true;

                return rData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public SMSOperationModel SendSMSToUser(SMSOperationModel mObj)
        {
            SMSOperationModel rData = new SMSOperationModel();
            try
            {
                string mobileNumber = "";

                if (mObj.UserId > 0)
                {
                    User data = DbContext.Set<User>().Where(x => x.Id == mObj.UserId).FirstOrDefault();
                    mobileNumber = data.MobileNumber;
                }
                else if (mObj.GrahokId > 0)
                {
                    GRAHOK_TABLE data = DbContext.Set<GRAHOK_TABLE>().Where(x => x.Id == mObj.GrahokId).FirstOrDefault();
                    mobileNumber = data.MobileNumber;
                }

                Messages msg = new Messages();
                msg.mobile = mobileNumber;
                msg.message = mObj.Message.message;
                msg.status = 0;
                msg.local = 0;
                DbContext.Set<Messages>().Add(msg);
                DbContext.SaveChanges();


                rData.OperationMessage = "SMS সফল ভাবে সেন্ট করা হয়েছে।";
                rData.isOperationSuccess = true;

                return rData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveAreaName(AreaList mObj, bool isAddMode)
        {
            try
            {
                if (isAddMode == true && mObj.Id > 0)
                {
                    AreaList area = DbContext.Set<AreaList>().Where(x => x.Id == mObj.Id).FirstOrDefault();
                    area.Name = mObj.Name;
                    area.EditedDate = DateTime.Now;
                    DbContext.SaveChanges();
                }
                else if (mObj != null)
                {
                    mObj.CreatedDate = DateTime.Now;
                    DbContext.Set<AreaList>().Add(mObj);
                    DbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private static string GenerateAccessTokenKey(int length = 10)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        private static string CreateRandomPassword(int length)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "0123456789";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        public UserOperationData DoDeactiveManager(int managerId)
        {
            try
            {
                UserOperationData opModel = new UserOperationData();
                if (managerId > 0)
                {

                    User oldItem = DbContext.Set<User>().Where(x => x.Id == managerId).FirstOrDefault();

                    if (oldItem != null)
                    {
                        oldItem.IsActivated = false;
                        DbContext.SaveChanges();

                        //Assign Return Data
                        opModel.isOperationSuccess = true;
                        opModel.OperationMessage = "Manager deactivated.";
                        return opModel;
                    }
                    else
                    {
                        //Assign Return Data
                        opModel.isOperationSuccess = false;
                        opModel.OperationMessage = "Manager not found";
                        return opModel;
                    }
                }

                //Assign Return Data
                opModel.isOperationSuccess = false;
                opModel.OperationMessage = "Something wrong, Contact to your owner!";
                return opModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public UserOperationData DeActiveSelectedManager(int managerId)
        {
            try
            {
                UserOperationData opModel = new UserOperationData();
                if (managerId > 0)
                {

                    User oldItem = DbContext.Set<User>().Where(x => x.Id == managerId).FirstOrDefault();

                    if (oldItem != null)
                    {
                        oldItem.IsActivated = true;
                        oldItem.IsAdminApproved = true;
                        DbContext.SaveChanges();

                        //Assign Return Data
                        opModel.isOperationSuccess = true;
                        opModel.OperationMessage = "ম্যানেজার সক্রিয় করা হয়েছে।";
                        return opModel;
                    }
                    else
                    {
                        //Assign Return Data
                        opModel.isOperationSuccess = false;
                        opModel.OperationMessage = "Manager not found";
                        return opModel;
                    }
                }

                //Assign Return Data
                opModel.isOperationSuccess = false;
                opModel.OperationMessage = "Something wrong, Contact to your owner!";
                return opModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public bool SaveCollectorAreaMapper(string areaList, long collectorId)
        {
            bool isSuccess = false;
            try
            {
                if (string.IsNullOrEmpty(areaList) == false) {
                    int qrySuccess = 0;
                    qrySuccess = DbContext.Database.ExecuteSqlCommand("delete GrahokAreaMapper where UserId=" + collectorId);

                    string[] data = areaList.Split(',');

                    if (data != null && data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            if (string.IsNullOrEmpty(data[i]) == false)
                            { 
                                GrahokAreaMapper mp = new GrahokAreaMapper();
                                mp.AreaId = int.Parse(data[i].Trim());
                                mp.UserId = collectorId;
                                DbContext.Set<GrahokAreaMapper>().Add(mp);
                                DbContext.SaveChanges();
                            }
                        }

                        isSuccess = true;
                    }

                }
                
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public UserOperationData SaveManagerInformation(User mObj, int roleId)
        {
            UserOperationData opModel = new UserOperationData();
            try
            {
                if (mObj != null && mObj.Id > 0)
                {
                    User user = DbContext.Set<User>().Where(x => x.Id == mObj.Id).FirstOrDefault();

                    user.Name = mObj.Name;
                    user.Email = mObj.Email;
                    user.House = mObj.House;
                    user.Road = mObj.Road;
                    user.PostOffice = mObj.PostOffice;
                    user.Thana = mObj.Thana;
                    user.Zila = mObj.Zila;
                    user.NIDNo = mObj.NIDNo;
                    user.ReferenceName = mObj.ReferenceName;
                    user.ReferenceMobile = mObj.ReferenceMobile;
                    DbContext.SaveChanges();


                    //Assign Return Data
                    opModel.User = mObj;
                    opModel.isOperationSuccess = true;
                    opModel.OperationMessage = "ডাটা " + mObj.Id + " সফল ভাবে সম্পাদন করা হয়েছে।";

                    Messages msg = new Messages();
                    msg.mobile = opModel.User.MobileNumber;
                    if (roleId == 2)
                        msg.message = "আপনার ডিজিপে এডমিন প্রোফাইল আপডেট করা হয়েছে। আপনার ডিজিপে একাউন্টি " + opModel.User.MobileNumber + " পাসওয়ার্ডটি " + user.Password + ",ধন্যবাদ ডিজিপে।";
                    else if (roleId == 3)
                        msg.message = "আপনার ডিজিপে ম্যানেজার প্রোফাইল আপডেট করা হয়েছে। আপনার ডিজিপে একাউন্টি " + opModel.User.MobileNumber + " পাসওয়ার্ডটি " + user.Password + ",ধন্যবাদ ডিজিপে।";
                    else if (roleId == 4)
                        msg.message = "আপনার ডিজিপে কালেক্টর প্রোফাইল আপডেট করা হয়েছে। আপনার ডিজিপে একাউন্টি " + opModel.User.MobileNumber + " পাসওয়ার্ডটি " + user.Password + ",ধন্যবাদ ডিজিপে।";
                    else
                        msg.message = "";

                    msg.status = 0;
                    msg.local = 0;
                    DbContext.Set<Messages>().Add(msg);
                    DbContext.SaveChanges();

                    return opModel;

                }
                else if (mObj != null)
                {
                    if (IsUserExistByMobileNumber(mObj.MobileNumber) == false)
                    {
                        string password = CreateRandomPassword(6);
                        string encryptPassword = SimpleCryptService.Factory().Encrypt(password);
                        mObj.Password = password;
                        mObj.EncPassword = encryptPassword;
                        mObj.RoleId = roleId;

                        if (mObj.IsActivated == false)
                            mObj.IsActivated = false;
                        else
                            mObj.IsActivated = true;

                        if (mObj.IsAdminApproved == false)
                            mObj.IsAdminApproved = false;
                        else
                            mObj.IsAdminApproved = true;

                        mObj.CreatedDate = DateTime.Now;
                        DbContext.Set<User>().Add(mObj);
                        DbContext.SaveChanges();

                        //Assign Return Data
                        opModel.User = mObj;
                        opModel.isOperationSuccess = true;
                        opModel.OperationMessage = "ম্যানেজার " + mObj.Id + " সফল ভাবে যুক্ত করা হয়েছে।";

                        Messages msg = new Messages();
                        msg.mobile = opModel.User.MobileNumber;
                        if (roleId == 2)
                            msg.message = "ডিজিপে এডমিন একাউন্ট খোলার জন্য আপনাকে স্বাগতম। আপনার ডিজিপে একাউন্টি " + opModel.User.MobileNumber + " পাসওয়ার্ডটি " + password + ",ধন্যবাদ ডিজিপে।";
                        else if (roleId == 3)
                            msg.message = "ডিজিপে ম্যানেজার একাউন্ট খোলার জন্য আপনাকে স্বাগতম। আপনার ডিজিপে একাউন্টি " + opModel.User.MobileNumber + " পাসওয়ার্ডটি " + password + ",ধন্যবাদ ডিজিপে।";
                        else if (roleId == 4)
                            msg.message = "ডিজিপে কালেক্টর একাউন্ট খোলার জন্য আপনাকে স্বাগতম। আপনার ডিজিপে একাউন্টি " + opModel.User.MobileNumber + " পাসওয়ার্ডটি " + password + ",ধন্যবাদ ডিজিপে।";
                        else
                            msg.message = "";

                        APIKeysTable token = new APIKeysTable();
                        token.UserId = opModel.User.Id;
                        token.acces_token = opModel.User.Id.ToString() + GenerateAccessTokenKey(10);
                        DbContext.Set<APIKeysTable>().Add(token);
                        DbContext.SaveChanges();

                        msg.status = 0;
                        msg.local = 0;
                        DbContext.Set<Messages>().Add(msg);
                        DbContext.SaveChanges();

                        return opModel;
                    }
                    else
                    {
                        //Assign Return Data
                        opModel.User = mObj;
                        opModel.isOperationSuccess = false;
                        opModel.OperationMessage = "মোবাইল নম্বর আগে থেকেই আমাদের সিস্টেম এ আছে।";
                        return opModel;
                    }
                }

                //Assign Return Data
                opModel.User = mObj;
                opModel.isOperationSuccess = false;
                opModel.OperationMessage = "Something wrong, Contact to your owner!";
                return opModel;

            }
            catch (Exception ex)
            {
                string loginMessage = ex.Message.ToString();
                opModel.OperationMessage = loginMessage;
                opModel.error = loginMessage;
                return opModel;
            }

        }

        public IList<GetAreaNameListByManagerId> GetAreanameByManagerId(long mngId)
        {
            try
            {
                SqlParameter prmMNGId = new SqlParameter("@mId", mngId);
                IList<GetAreaNameListByManagerId> dataList = DbContext.Database.SqlQuery<GetAreaNameListByManagerId>("GetAreaNameListByManagerId @mId", prmMNGId).ToList();

                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IList<UserNameAndIdList> GetManagerNameListByFiderId(long fiderId)
        {
            try
            {
                SqlParameter prmFider = new SqlParameter("@fiderId", fiderId);
                IList<UserNameAndIdList> dataList = DbContext.Database.SqlQuery<UserNameAndIdList>("Get_Manager_Names_By_FiderId @fiderId", prmFider).ToList();

                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AreaList GetAreaDetailsByAreaId(int id)
        {
            try
            {
                AreaList data = DbContext.Set<AreaList>().Where(x => x.Id == id).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<UserNameAndIdList> GetCollectorNamesAndIdListByRoleAndUserId(long mngId, int roleId)
        {
            try
            {
                SqlParameter prmMNGId = new SqlParameter("@mngId", mngId);
                SqlParameter prmRoleId = new SqlParameter("@roleId", roleId);
                IList<UserNameAndIdList> dataList = DbContext.Database.SqlQuery<UserNameAndIdList>("Get_Collector_Name_By_ManagerId @mngId,@roleId", prmMNGId, prmRoleId).ToList();

                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<GetAreaNameListByManagerId> GetAreaNameByManagerIdWithDetault(long mngId)
        {
            try
            {
                SqlParameter prmMNGId = new SqlParameter("@mId", mngId);
                IList<GetAreaNameListByManagerId> dataList = DbContext.Database.SqlQuery<GetAreaNameListByManagerId>("Get_AreaName_ListBy_ManagerId_With_Default @mId", prmMNGId).ToList();

                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsUserExistByMobileNumber(string mobileNumber)
        {
            try
            {
                bool isExist = false;
                SqlParameter prmMobile = new SqlParameter("@mobileNumber", mobileNumber);
                IList<User> dataList = DbContext.Database.SqlQuery<User>("Get_User_By_Mobile_Number @mobileNumber", prmMobile).ToList();
                if (dataList != null && dataList.Count > 0)
                    isExist = true;

                return isExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<User> GetUserListByMobileNumber(string mobileNumber)
        {
            try
            {
                SqlParameter prmMobile = new SqlParameter("@mobileNumber", mobileNumber);
                IList<User> dataList = DbContext.Database.SqlQuery<User>("Get_User_By_Mobile_Number @mobileNumber", prmMobile).ToList();

                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Super_Admin_Get_Manager_List> AdminGetmanagerList(long loginUserId)
        {
            try
            {
                SqlParameter prmUserId = new SqlParameter("@fiderId", loginUserId);
                IList<Super_Admin_Get_Manager_List> dataList = DbContext.Database.SqlQuery<Super_Admin_Get_Manager_List>("Fider_Get_Manager_List @fiderId", prmUserId).ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IList<BillPaidSummary> GetMyPaidBill(long loginUserId, int page, int limit, int api)
        {
            try
            {
                SqlParameter prmUserId = new SqlParameter("@userId", loginUserId);
                SqlParameter prmPage = new SqlParameter("@page", page);
                SqlParameter prmLimit = new SqlParameter("@limit", limit);
                SqlParameter prmAPI = new SqlParameter("@api", api);
                IList<BillPaidSummary> dataList = DbContext.Database.SqlQuery<BillPaidSummary>("Get_Submited_Bill_By_SubmitedId @userId,@page,@limit,@api", prmUserId, prmPage, prmLimit, prmAPI).ToList();

                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TotalGrahokRow GetMyTotalPaidBill(long loginUserId)
        {
            try
            {
                SqlParameter prmUserId = new SqlParameter("@userId", loginUserId);
                TotalGrahokRow dataList = DbContext.Database.SqlQuery<TotalGrahokRow>("Get_Total_Submited_Bill_By_SubmitedId @userId", prmUserId).FirstOrDefault();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<BillPaidSummary> GetMyReceivedBill(long managerId, long fiderId, int page, int limit, int api)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@managerId", managerId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmPage = new SqlParameter("@page", page);
                SqlParameter prmLimit = new SqlParameter("@limit", limit);
                SqlParameter prmAPI = new SqlParameter("@api", api);
                IList<BillPaidSummary> dataList = DbContext.Database.SqlQuery<BillPaidSummary>("Get_My_Received_Bill_By_SubmitedId @managerId,@fiderId,@page,@limit,@api", prmManagerId, prmFiderId,prmPage,prmLimit,prmAPI).ToList();

                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public TotalGrahokRow GetTotalMyReceivedBill(long mngId, long fiderId)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                TotalGrahokRow dataList = DbContext.Database.SqlQuery<TotalGrahokRow>("Get_Total_My_Received_Bill_By_SubmitedId @mngId,@fiderId", prmManagerId, prmFiderId).FirstOrDefault();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Super_Admin_Get_Manager_List> ManagerGetCollectorListByManagerId(long mngId, long fiderId)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                IList<Super_Admin_Get_Manager_List> dataList = DbContext.Database.SqlQuery<Super_Admin_Get_Manager_List>("Manager_Get_Collector_List_By_managerId @mngId,@fiderId", prmManagerId, prmFiderId).ToList();
                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
        

        public User GetUserDataByUserId(long mngId)
        {
            try
            {
                User data = DbContext.Set<User>().Where(x => x.Id == mngId).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<GrahokAreaMapper> GetCollectorAreaList(long mngId)
        {
            try
            {
                IList<GrahokAreaMapper> data = DbContext.Set<GrahokAreaMapper>().Where(x => x.UserId == mngId).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public UserOperationData UpdateUserProfile(UserOperationData mObj, bool isAddMode)
        {
            try
            {
                UserOperationData rData = new UserOperationData();
                if (mObj.User != null)
                {
                    User data = DbContext.Set<User>().Where(x => x.Id == mObj.User.Id).FirstOrDefault();
                    if (data != null)
                    {
                        data.Name = mObj.User.Name; 
                        data.Avatar = mObj.User.Avatar;
                        data.CompanyName = mObj.User.CompanyName;
                        data.CompanySignature = mObj.User.CompanySignature;
                        data.CompanyLogo = mObj.User.CompanyLogo;
                        data.FathersName = mObj.User.FathersName;
                        data.Email = mObj.User.Email;
                        data.House = mObj.User.House;
                        data.Road = mObj.User.Road;
                        data.PostOffice = mObj.User.PostOffice;
                        data.Thana = mObj.User.Thana;
                        data.Zila = mObj.User.Zila;
                        data.NIDNo = mObj.User.NIDNo;
                        data.ReferenceName = mObj.User.ReferenceName;
                        data.ReferenceMobile = mObj.User.ReferenceMobile;
                        if (data.RoleId == 4 && mObj.User.UserManagerId != null)
                            data.UserManagerId = mObj.User.UserManagerId;
                        DbContext.SaveChanges();

                        rData.isOperationSuccess = true;
                        rData.OperationMessage = "প্রোফাইল সফল ভাবে আপডেট করা হয়েছে।";
                        return rData;
                    }
                }

                return rData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserOperationData UpdateChangePassword(UserOperationData mObj, bool isAddMode)
        {
            try
            {
                UserOperationData rData = new UserOperationData();
                if (mObj.User != null)
                {
                    User data = DbContext.Set<User>().Where(x => x.Id == mObj.User.Id).FirstOrDefault();
                    if (data != null)
                    {
                        string encryptPassword = SimpleCryptService.Factory().Encrypt(mObj.User.Password);
                        data.Password = mObj.User.Password;
                        data.EncPassword = encryptPassword;
                        DbContext.SaveChanges();

                        rData.isOperationSuccess = true;
                        rData.OperationMessage = "পাসওয়ার্ড সফল ভাবে পরিবর্তন করা হয়েছে।";
                        return rData;
                    }
                }

                rData.isOperationSuccess = false;
                rData.OperationMessage = "Something wrong, Contact to your owner!";
                return rData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public GrahokOperationModel SaveGrahokInformation(GrahokOperationModel mObj, int roleId)
        {
            try
            {
                GrahokOperationModel opModel = new GrahokOperationModel();
                if (mObj.GrahokTable != null && mObj.GrahokTable.Id > 0)
                {
                    GRAHOK_TABLE grahok = DbContext.Set<GRAHOK_TABLE>().Where(x => x.Id == mObj.GrahokTable.Id).FirstOrDefault();

                    grahok.Name = mObj.GrahokTable.Name;
                    grahok.LineRate = mObj.GrahokTable.LineRate;
                    grahok.DueAmount = mObj.GrahokTable.DueAmount;
                    grahok.AdvanceAmount = mObj.GrahokTable.AdvanceAmount;
                    grahok.HouseOwnerName = mObj.GrahokTable.HouseOwnerName;
                    grahok.GrahokId = mObj.GrahokTable.GrahokId;
                    grahok.NumberOfTV = mObj.GrahokTable.NumberOfTV;
                    grahok.CompanyName = mObj.GrahokTable.CompanyName;
                    grahok.MobileNumber = mObj.GrahokTable.MobileNumber;
                    grahok.Email = mObj.GrahokTable.Email;
                    grahok.House = mObj.GrahokTable.House;
                    grahok.Road = mObj.GrahokTable.Road;
                    grahok.PostOffice = mObj.GrahokTable.PostOffice;
                    grahok.Thana = mObj.GrahokTable.Thana;
                    grahok.Zila = mObj.GrahokTable.Zila;
                    grahok.NIDNo = mObj.GrahokTable.NIDNo;
                    grahok.ReferenceName = mObj.GrahokTable.ReferenceName;
                    grahok.ReferenceMobile = mObj.GrahokTable.ReferenceMobile;
                    grahok.AreaId = mObj.GrahokTable.AreaId;

                    DbContext.SaveChanges();

                    //Assign Return Data
                    opModel.GrahokTable = mObj.GrahokTable;
                    opModel.isOperationSuccess = true;
                    opModel.OperationMessage = "গ্রাহক " + mObj.GrahokTable.Id + " সফল ভাবে সম্পাদন করা হয়েছে।";
                    return opModel;

                }
                else if (mObj.GrahokTable != null)
                {
                    mObj.GrahokTable.GrahokRoleId = roleId;
                    mObj.GrahokTable.IsActivated = true;
                    mObj.GrahokTable.CreatedDate = DateTime.Now;
                    DbContext.Set<GRAHOK_TABLE>().Add(mObj.GrahokTable);
                    DbContext.SaveChanges();

                    //Assign Return Data
                    opModel.GrahokTable = mObj.GrahokTable;
                    opModel.isOperationSuccess = true;
                    opModel.OperationMessage = "গ্রাহক " + mObj.GrahokTable.Id + " সফল ভাবে যুক্ত করা হয়েছে।";
                    return opModel;
                }

                //Assign Return Data
                opModel.GrahokTable = opModel.GrahokTable;
                opModel.isOperationSuccess = false;
                opModel.OperationMessage = "Something wrong, Contact to your owner!";
                return opModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public IList<GrahokTableReturnColumns> ManagerGetDueAmountGrahokList(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                SqlParameter prmPage = new SqlParameter("@page", page);
                SqlParameter prmLimit = new SqlParameter("@limit", limit);
                SqlParameter prmAPI = new SqlParameter("@api", api);
                IList<GrahokTableReturnColumns> dataList = DbContext.Database.SqlQuery<GrahokTableReturnColumns>("Get_Due_Amount_Grahok_list_By_ManagerId @mngId,@fiderId,@cltId,@page,@limit,@api", prmManagerId, prmFiderId, prmCollectorId, prmPage, prmLimit, prmAPI).ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public TotalGrahokRow ManagerGetTotalDueAmountGrahokList(long mngId, long fiderId, long collectorId)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                TotalGrahokRow dataList = DbContext.Database.SqlQuery<TotalGrahokRow>("Get_Total_Due_Amount_Grahok_list_By_ManagerId @mngId,@fiderId,@cltId", prmManagerId, prmFiderId, prmCollectorId).FirstOrDefault();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IList<GrahokTableReturnColumns> GetAllGrahokForPayBill(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                SqlParameter prmPage = new SqlParameter("@page", page);
                SqlParameter prmLimit = new SqlParameter("@limit", limit);
                SqlParameter prmAPI = new SqlParameter("@api", api);
                IList<GrahokTableReturnColumns> dataList = DbContext.Database.SqlQuery<GrahokTableReturnColumns>("Get_Grahok_List_For_Bill_Collection_List @mngId,@fiderId,@cltId,@page,@limit,@api", prmManagerId, prmFiderId, prmCollectorId, prmPage, prmLimit, prmAPI).ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public IList<GrahokTableReturnColumns> GetGrahokListBySearchParam(string searchText, long mngId, long fiderId, long collectorId, int page, int limit)
        {
            try
            {
                SqlParameter prmSearchText = new SqlParameter("@searchText", searchText);
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                SqlParameter prmPage = new SqlParameter("@page", page);
                SqlParameter prmLimit = new SqlParameter("@limit", limit);
                IList<GrahokTableReturnColumns> dataList = DbContext.Database.SqlQuery<GrahokTableReturnColumns>("Get_All_Grahok_By_Search_Param @mngId,@fiderId,@cltId,@searchText,@page,@limit", prmManagerId, prmFiderId, prmCollectorId, prmSearchText, prmPage, prmLimit).ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TotalGrahokRow GetTotalNumberOfGrahokBySearchParam(string searchText, long mngId, long fiderId, long collectorId)
        {
            try
            {
                SqlParameter prmSearchText = new SqlParameter("@searchText", searchText);
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                TotalGrahokRow dataList = DbContext.Database.SqlQuery<TotalGrahokRow>("Get_Total_Number_Of_Grahok_By_Search_Param @mngId,@fiderId,@cltId,@searchText", prmManagerId, prmFiderId, prmCollectorId,prmSearchText).FirstOrDefault();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TotalGrahokRow GetTotalGrahokForPayBill(long mngId, long fiderId, long collectorId)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                TotalGrahokRow dataList = DbContext.Database.SqlQuery<TotalGrahokRow>("Get_Total_Grahok_List_For_Bill_Collection_List @mngId,@fiderId,@cltId", prmManagerId, prmFiderId, prmCollectorId).FirstOrDefault();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public IList<GrahokTableReturnColumns> GetAllBillPaidGrahokThisMonth(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                SqlParameter prmPage = new SqlParameter("@page", page);
                SqlParameter prmLimit = new SqlParameter("@limit", limit);
                SqlParameter prmAPI = new SqlParameter("@api", api);
                IList<GrahokTableReturnColumns> dataList = DbContext.Database.SqlQuery<GrahokTableReturnColumns>("Get_Paid_Grahok_list_For_manager_And_Fider @mngId,@fiderId,@cltId,@page,@limit,@api", prmManagerId, prmFiderId,prmCollectorId, prmPage, prmLimit, prmAPI).ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TotalGrahokRow GetTotalGrahokThisMonthPaidBill(long mngId, long fiderId, long collectorId)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                TotalGrahokRow dataList = DbContext.Database.SqlQuery<TotalGrahokRow>("Get_Total_Paid_Grahok_list_For_manager_And_Fider @mngId,@fiderId,@cltId", prmManagerId, prmFiderId, prmCollectorId).FirstOrDefault();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IList<Bill_History_For_Grahok> Get_Bill_History_For_Grahok(long grahokId)
        {
            try
            {
                SqlParameter prmGrahokId = new SqlParameter("@grahokId", grahokId);
                IList<Bill_History_For_Grahok> dataList = DbContext.Database.SqlQuery<Bill_History_For_Grahok>("Get_Bill_History_By_Grahok_Id @grahokId", prmGrahokId).ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<GrahokTableReturnColumns> ManagerGetFreeGrahok(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                SqlParameter prmPage = new SqlParameter("@page", page);
                SqlParameter prmLimit = new SqlParameter("@limit", limit);
                SqlParameter prmAPI = new SqlParameter("@api", api);
                IList<GrahokTableReturnColumns> dataList = DbContext.Database.SqlQuery<GrahokTableReturnColumns>("Get_Free_Grahok_By_Manager_id @mngId,@fiderId,@cltId,@page,@limit,@api", prmManagerId, prmFiderId, prmCollectorId, prmPage, prmLimit, prmAPI).ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TotalGrahokRow ManagerGetTotalFreeGrahok(long mngId, long fiderId, long collectorId)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                TotalGrahokRow dataList = DbContext.Database.SqlQuery<TotalGrahokRow>("Get_Total_Free_Grahok_By_Manager_id @mngId,@fiderId,@cltId", prmManagerId, prmFiderId, prmCollectorId).FirstOrDefault();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int DeleteSelectedGrahok(long grahokId)
        {
            int qrySuccess = 0;
            try
            {
                qrySuccess = DbContext.Database.ExecuteSqlCommand("delete GRAHOK where id =" + grahokId);

                return qrySuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<GrahokTableReturnColumns> ManagerGetAllGrahok(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            try
            {
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                SqlParameter prmPage = new SqlParameter("@page", page);
                SqlParameter prmLimit = new SqlParameter("@limit", limit);
                SqlParameter prmAPI = new SqlParameter("@api", api);
                IList<GrahokTableReturnColumns> dataList = DbContext.Database.SqlQuery<GrahokTableReturnColumns>("Get_All_Grahok_By_Manager_id @mngId,@fiderId,@cltId,@page,@limit,@api", prmManagerId, prmFiderId, prmCollectorId,prmPage,prmLimit, prmAPI).ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TotalGrahokRow ManagerGetTotalGrahokNumber(long mngId, long fiderId, long collectorId)
        {
            try
            {
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                TotalGrahokRow dataList = DbContext.Database.SqlQuery<TotalGrahokRow>("Get_Total_Number_Grahok_By_Manager_id @mngId,@fiderId,@cltId", prmManagerId, prmFiderId, prmCollectorId).FirstOrDefault();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IList<GrahokTableReturnColumns> ManagerGetAllDeactiveGrahok(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                SqlParameter prmPage = new SqlParameter("@page", page);
                SqlParameter prmLimit = new SqlParameter("@limit", limit);
                SqlParameter prmAPI = new SqlParameter("@api", api);
                IList<GrahokTableReturnColumns> dataList = DbContext.Database.SqlQuery<GrahokTableReturnColumns>("Get_Deactive_Grahok_By_Manager_id @mngId,@fiderId,@cltId,@page,@limit,@api", prmManagerId, prmFiderId, prmCollectorId, prmPage, prmLimit, prmAPI).ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TotalGrahokRow ManagerGetTotalDeactiveGrahok(long mngId, long fiderId, long collectorId)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);

                TotalGrahokRow dataList = DbContext.Database.SqlQuery<TotalGrahokRow>("Get_Total_Deactive_Grahok_By_Manager_id @mngId,@fiderId,@cltId", prmManagerId, prmFiderId, prmCollectorId).FirstOrDefault();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IList<GrahokTableReturnColumns> ManagerGetAllActiveGrahok(long mngId, long fiderId, long collectorId, int page, int limit, int api)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                SqlParameter prmPage = new SqlParameter("@page", page);
                SqlParameter prmLimit = new SqlParameter("@limit", limit);
                SqlParameter prmAPI = new SqlParameter("@api", api);
                IList<GrahokTableReturnColumns> dataList = DbContext.Database.SqlQuery<GrahokTableReturnColumns>("Get_Active_Grahok_By_Manager_id @mngId,@fiderId,@cltId,@page,@limit,@api", prmManagerId, prmFiderId, prmCollectorId, prmPage, prmLimit, prmAPI).ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TotalGrahokRow ManagerGetTotalActiveGrahok(long mngId, long fiderId, long collectorId)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCollectorId = new SqlParameter("@cltId", collectorId);
                TotalGrahokRow dataList = DbContext.Database.SqlQuery<TotalGrahokRow>("Get_Total_Active_Grahok_By_Manager_id @mngId,@fiderId,@cltId", prmManagerId, prmFiderId, prmCollectorId).FirstOrDefault();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public GRAHOK_TABLE GetGrahokDetailsByGrahokId(long grahokId)
        {
            try
            {
                GRAHOK_TABLE data = DbContext.Set<GRAHOK_TABLE>().Where(x => x.Id == grahokId).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public GrahokTableReturnColumns GetGrahokTableReturnData(long grahokId)
        {
            try
            {
                SqlParameter prmGrahokId = new SqlParameter("@grahokId", grahokId);
                GrahokTableReturnColumns dataList = DbContext.Database.SqlQuery<GrahokTableReturnColumns>("Get_Grahok_Table_Return_Data @grahokId", prmGrahokId).FirstOrDefault();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Super_Admin_Get_Manager_List> SuperAdminGetFiderList(long mngId, int roleId = 0)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", mngId);
                SqlParameter prmRoleId = new SqlParameter("@roleId", roleId);
                IList<Super_Admin_Get_Manager_List> dataList = DbContext.Database.SqlQuery<Super_Admin_Get_Manager_List>("Super_Admin_Get_Fider_list @mngId,@roleId", prmManagerId, prmRoleId).ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public GrahokOperationModel DeDeactiveSelectedGrahok(long grahokId)
        {
            try
            {
                GrahokOperationModel opModel = new GrahokOperationModel();
                if (grahokId > 0)
                {

                    GRAHOK_TABLE oldItem = DbContext.Set<GRAHOK_TABLE>().Where(x => x.Id == grahokId).FirstOrDefault();

                    if (oldItem != null)
                    {
                        oldItem.IsActivated = false;
                        DbContext.SaveChanges();

                        //Assign Return Data
                        opModel.isOperationSuccess = true;
                        opModel.OperationMessage = "গ্রাহক বন্ধ করা হয়েছে।";
                        return opModel;
                    }
                    else
                    {
                        //Assign Return Data
                        opModel.isOperationSuccess = false;
                        opModel.OperationMessage = "Grahok not found";
                        return opModel;
                    }
                }

                //Assign Return Data
                opModel.isOperationSuccess = false;
                opModel.OperationMessage = "Something wrong, Contact to your owner!";
                return opModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public GrahokOperationModel DeActiveSelectedGrahok(long grahokId)
        {
            try
            {
                GrahokOperationModel opModel = new GrahokOperationModel();
                if (grahokId > 0)
                {

                    GRAHOK_TABLE oldItem = DbContext.Set<GRAHOK_TABLE>().Where(x => x.Id == grahokId).FirstOrDefault();

                    if (oldItem != null)
                    {
                        oldItem.IsActivated = true;
                        DbContext.SaveChanges();

                        //Assign Return Data
                        opModel.isOperationSuccess = true;
                        opModel.OperationMessage = "গ্রাহক সক্রিয় করা হয়েছে।";
                        return opModel;
                    }
                    else
                    {
                        //Assign Return Data
                        opModel.isOperationSuccess = false;
                        opModel.OperationMessage = "Grahok not found";
                        return opModel;
                    }
                }

                //Assign Return Data
                opModel.isOperationSuccess = false;
                opModel.OperationMessage = "Something wrong, Contact to your owner!";
                return opModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public BillOperationModel SaveBillCollecInformation(BillOperationModel mObj, bool isAddMode)
        {
            try
            {
                BillOperationModel opModel = new BillOperationModel();
                if (mObj.BillTable != null)
                {

                    SqlParameter pGrahok = new SqlParameter("@grahokId", mObj.BillTable.GrahokId);
                    GrahokDataForPayBill oldInfo = DbContext.Database.SqlQuery<GrahokDataForPayBill>("Get_Grahok_Bill_Collect_data @grahokId", pGrahok).FirstOrDefault();

                    DateTime endDate = oldInfo.last_bill_date;

                    if (oldInfo.line_rate > 0) {



                        while (mObj.BillTable.BillAmount >= oldInfo.line_rate) {
                            BillHistoryTable insert = new BillHistoryTable();
                            endDate = endDate.AddMonths(1);
                            insert.GrahokId = mObj.BillTable.GrahokId;
                            insert.FiderId = mObj.BillTable.FiderId;
                            insert.ManagerId = mObj.BillTable.ManagerId;
                            insert.DueAmount = 0;
                            insert.AdvanceAmount = 0;
                            insert.BillAmount = oldInfo.line_rate;
                            insert.BillMonth = endDate;
                            insert.CollectedDate = DateTime.Now;
                            insert.CollectedBy = mObj.BillTable.CollectedBy;
                            DbContext.Set<BillHistoryTable>().Add(insert);
                            DbContext.SaveChanges();

                            mObj.BillTable.BillAmount = (mObj.BillTable.BillAmount - oldInfo.line_rate);
                        }

                    }
                    
                     
                    GRAHOK_TABLE grahokData = DbContext.Set<GRAHOK_TABLE>().Where(x => x.Id == mObj.BillTable.GrahokId).FirstOrDefault();

                    if (grahokData.LineRate != null && grahokData.LineRate > 0)
                    {
                        grahokData.DueAmount = 0;
                        grahokData.AdvanceAmount = mObj.BillTable.BillAmount;
                        grahokData.last_paid = endDate;
                        grahokData.bill_paid_date = DateTime.Now;
                        DbContext.SaveChanges();
                    }


                    SqlParameter prmGrahokId = new SqlParameter("@grahokId", mObj.BillTable.GrahokId);
                    SqlParameter prmFiderId = new SqlParameter("@fiderId", mObj.BillTable.FiderId);
                    GrahokNameMobile_FiderSignature fiderSignature = DbContext.Database.SqlQuery<GrahokNameMobile_FiderSignature>("Get_GrahokNameMobile_FiderSignature @grahokId,@fiderId", prmGrahokId, prmFiderId).FirstOrDefault();

                    if (fiderSignature != null)
                    {
                        Messages msg = new Messages();
                        msg.mobile = fiderSignature.Mobile;
                        msg.message = "গ্রাহকঃ " + grahokData.Name + ",আইডিঃ " + grahokData.GrahokId + ", অক্টোবর মাস ডিশবিল আদায় ৳" + grahokData.LineRate.Value + ", বকেয়া ৳" + grahokData.DueAmount.Value + " -" + fiderSignature.FiderSign;
                        msg.status = 0;
                        msg.local = 0;
                        DbContext.Set<Messages>().Add(msg);
                        DbContext.SaveChanges();
                    }

                    Notifications notification = new Notifications();
                    notification.Title = "New notification from " + mObj.UserName;
                    notification.Notification = grahokData.Name + ", " + endDate.ToString("MM-yyyy") + " মাসের বিল পরিশোধ করেছে";
                    notification.IsSeen = false;
                    notification.CollectorId = mObj.BillTable.CollectorId;
                    notification.ManagerId = mObj.BillTable.ManagerId;
                    notification.FiderId = mObj.BillTable.FiderId;
                    notification.DateTime = DateTime.Now;
                    DbContext.Set<Notifications>().Add(notification);
                    DbContext.SaveChanges();



                    //Assign Return Data
                    opModel.BillTable = mObj.BillTable;
                    opModel.isOperationSuccess = true;
                    opModel.OperationMessage = "বিল সফল ভাবে প্রদান করা হয়েছে।";
                    return opModel;
                }

                //Assign Return Data
                opModel.BillTable = opModel.BillTable;
                opModel.isOperationSuccess = false;
                opModel.OperationMessage = "Something wrong, Contact to your owner!";
                return opModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public BillCollectOperationModel SaveBillCollectInformation(BillCollectOperationModel mObj, bool isAddMode)
        {
            try
            {
                BillCollectOperationModel opModel = new BillCollectOperationModel();
                if (mObj.BillTable != null)
                {

                    SqlParameter pGrahok = new SqlParameter("@grahokId", mObj.BillTable.GrahokId);
                    GrahokDataForPayBill oldInfo = DbContext.Database.SqlQuery<GrahokDataForPayBill>("Get_Grahok_Bill_Collect_data @grahokId", pGrahok).FirstOrDefault();

                    DateTime endDate = oldInfo.last_bill_date;

                    if (oldInfo.line_rate > 0) {



                        while (mObj.BillTable.BillAmount >= oldInfo.line_rate) {
                            BillHistoryTable insert = new BillHistoryTable();
                            endDate = endDate.AddMonths(1);
                            insert.GrahokId = mObj.BillTable.GrahokId;
                            insert.FiderId = mObj.BillTable.FiderId;
                            insert.ManagerId = mObj.BillTable.ManagerId;
                            insert.DueAmount = 0;
                            insert.AdvanceAmount = 0;
                            insert.BillAmount = oldInfo.line_rate;
                            insert.BillMonth = endDate;
                            insert.CollectedBy = mObj.BillTable.CollectedBy;
                            insert.CollectedDate = DateTime.Now;
                            DbContext.Set<BillHistoryTable>().Add(insert);
                            DbContext.SaveChanges();

                            mObj.BillTable.BillAmount = (mObj.BillTable.BillAmount - oldInfo.line_rate);
                        }

                    }
                    
                     
                    GRAHOK_TABLE grahokData = DbContext.Set<GRAHOK_TABLE>().Where(x => x.Id == mObj.BillTable.GrahokId).FirstOrDefault();

                    if (grahokData.LineRate != null && grahokData.LineRate > 0)
                    {
                        grahokData.DueAmount = 0;
                        grahokData.AdvanceAmount = mObj.BillTable.BillAmount;
                        grahokData.last_paid = endDate;
                        grahokData.bill_paid_date = DateTime.Now;
                        DbContext.SaveChanges();
                    }


                    SqlParameter prmGrahokId = new SqlParameter("@grahokId", mObj.BillTable.GrahokId);
                    SqlParameter prmFiderId = new SqlParameter("@fiderId", mObj.BillTable.FiderId);
                    GrahokNameMobile_FiderSignature fiderSignature = DbContext.Database.SqlQuery<GrahokNameMobile_FiderSignature>("Get_GrahokNameMobile_FiderSignature @grahokId,@fiderId", prmGrahokId, prmFiderId).FirstOrDefault();

                    if (fiderSignature != null)
                    {
                        Messages msg = new Messages();
                        msg.mobile = fiderSignature.Mobile;
                        msg.message = "গ্রাহকঃ " + grahokData.Name + ",আইডিঃ " + grahokData.GrahokId + ", অক্টোবর মাস ডিশবিল আদায় ৳" + grahokData.LineRate.Value + ", বকেয়া ৳" + grahokData.DueAmount.Value + " -" + fiderSignature.FiderSign;
                        msg.status = 0;
                        msg.local = 0;
                        DbContext.Set<Messages>().Add(msg);
                        DbContext.SaveChanges();
                    }

                    Notifications notification = new Notifications();
                    notification.Title = "New notification from " + mObj.UserName;
                    notification.Notification = grahokData.Name + ", " + endDate.ToString("MM-yyyy") + " মাসের বিল পরিশোধ করেছে";
                    notification.IsSeen = false;
                    notification.CollectorId = mObj.BillTable.CollectorId;
                    notification.ManagerId = mObj.BillTable.ManagerId;
                    notification.FiderId = mObj.BillTable.FiderId;
                    notification.DateTime = DateTime.Now;
                    DbContext.Set<Notifications>().Add(notification);
                    DbContext.SaveChanges();



                    //Assign Return Data
                    opModel.BillTable = mObj.BillTable;
                    opModel.isOperationSuccess = true;
                    opModel.OperationMessage = "বিল সফল ভাবে প্রদান করা হয়েছে।";
                    return opModel;
                }

                //Assign Return Data
                opModel.BillTable = opModel.BillTable;
                opModel.isOperationSuccess = false;
                opModel.OperationMessage = "Something wrong, Contact to your owner!";
                return opModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IList<Messages> GetAllMessageInformation()
        {
            try
            {
                IList<Messages> dataList = DbContext.Database.SqlQuery<Messages>("select top 20 * from Messages where status = 0").ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<allSMSMobile> GetAllSMSMobileNumbers(int page, int limit)
        {
            try
            {
                SqlParameter prmPage = new SqlParameter("@page", page);
                SqlParameter prmLimit = new SqlParameter("@limit", limit);
                IList<allSMSMobile> dataList = DbContext.Database.SqlQuery<allSMSMobile>("Up_Get_SMS_Grahok_Number @page, @limit", prmPage, prmLimit).ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region"Bill History"

        public BilHistory_For_Manager_Fider_Collector GetDailyBillHistoryDataByRoleAndIdAndDate(long fiderId, long mngId, long cltId, DateTime startDate, DateTime endDate)
        {
            try
            {



                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmManagerId = new SqlParameter("@managerId", mngId);
                SqlParameter prmCollectorId = new SqlParameter("@collectorId", cltId);
                SqlParameter prmStartDate = new SqlParameter("@startDate", startDate);
                SqlParameter prmEndDate = new SqlParameter("@endDate", endDate);
                BilHistory_For_Manager_Fider_Collector history = DbContext.Database.SqlQuery<BilHistory_For_Manager_Fider_Collector>("Get_Filtered_Bil_History_Report_By_Fider_Manager_Collector_Id @fiderId,@managerId,@collectorId,@startDate,@endDate", prmFiderId, prmManagerId, prmCollectorId, prmStartDate, prmEndDate).FirstOrDefault();

                return history;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public IList<Get_Bill_Collection_History> GetBillCollectionHistory(long fiderId, long mngId, long cltId, DateTime startDate, DateTime endDate, int page, int limit, int api)
        { 
            try
            {

                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmManagerId = new SqlParameter("@managerId", mngId);
                SqlParameter prmCollectorId = new SqlParameter("@collectorId", cltId);
                SqlParameter prmStartDate = new SqlParameter("@startDate", startDate);
                SqlParameter prmEndDate = new SqlParameter("@endDate", endDate);
                SqlParameter prmPage = new SqlParameter("@page", page);
                SqlParameter prmLimit = new SqlParameter("@limit", limit);
                SqlParameter prmAPI = new SqlParameter("@api", api);
                IList<Get_Bill_Collection_History> history = DbContext.Database.SqlQuery<Get_Bill_Collection_History>("Get_Bill_Collection_History @fiderId,@managerId,@collectorId,@startDate,@endDate,@page,@limit,@api", prmFiderId, prmManagerId, prmCollectorId, prmStartDate, prmEndDate, prmPage, prmLimit, prmAPI).ToList();

                return history;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public TotalGrahokRow GetTotalBillCollectionHistory(long fiderId, long mngId, long cltId, DateTime startDate, DateTime endDate)
        {
            try
            {

                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmManagerId = new SqlParameter("@managerId", mngId);
                SqlParameter prmCollectorId = new SqlParameter("@collectorId", cltId);
                SqlParameter prmStartDate = new SqlParameter("@startDate", startDate);
                SqlParameter prmEndDate = new SqlParameter("@endDate", endDate);
                TotalGrahokRow history = DbContext.Database.SqlQuery<TotalGrahokRow>("Get_Total_Bill_Collection_History @fiderId,@managerId,@collectorId,@startDate,@endDate", prmFiderId, prmManagerId, prmCollectorId, prmStartDate, prmEndDate).FirstOrDefault();

                return history;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public BilHistory_For_Manager_Fider_Collector GetBillHistoryDataByRoleAndId(int roleId, long userId)
        {
            try
            {
                long fiderId = 0;
                long managerId = 0;
                long collectorId = 0;

                if (roleId == 2)
                    fiderId = userId;

                if (roleId == 3)
                    managerId = userId;

                if (roleId == 4)
                    collectorId = userId;


                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmManagerId = new SqlParameter("@managerId", managerId);
                SqlParameter prmCollectorId = new SqlParameter("@collectorId", collectorId);

                BilHistory_For_Manager_Fider_Collector history = DbContext.Database.SqlQuery<BilHistory_For_Manager_Fider_Collector>("Get_Monthly_Bil_History_Report_By_Fider_Manager_Collector_Id @fiderId,@managerId,@collectorId", prmFiderId, prmManagerId, prmCollectorId).FirstOrDefault();

                return history;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public BilHistory_For_Manager_Fider_Collector GetDailyBillHistoryDataByRoleAndId(int roleId, long userId)
        {
            try
            {
                long fiderId = 0;
                long managerId = 0;
                long collectorId = 0;

                if (roleId == 2)
                    fiderId = userId;

                if (roleId == 3)
                    managerId = userId;

                if (roleId == 4)
                    collectorId = userId;


                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmManagerId = new SqlParameter("@managerId", managerId);
                SqlParameter prmCollectorId = new SqlParameter("@collectorId", collectorId);

                BilHistory_For_Manager_Fider_Collector history = DbContext.Database.SqlQuery<BilHistory_For_Manager_Fider_Collector>("Get_Daily_Bil_History_Report_By_Fider_Manager_Collector_Id @fiderId,@managerId,@collectorId", prmFiderId, prmManagerId, prmCollectorId).FirstOrDefault();

                return history;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public SMSUpdateStatus UpdateSMS(Messages msg)
        {
            try
            {
                SMSUpdateStatus opModel = new SMSUpdateStatus();
                if (msg.id > 0)
                {

                    Messages oldItem = DbContext.Set<Messages>().Where(x => x.id == msg.id).FirstOrDefault();

                    if (oldItem != null)
                    {
                        oldItem.local = msg.local;
                        oldItem.status = msg.status;
                        DbContext.SaveChanges();

                        //Assign Return Data
                        opModel.id = oldItem.id;
                        opModel.status = msg.status.Value;
                        opModel.local = msg.local.Value;
                        opModel.success = true;
                        if (msg.status.Value == 1)
                            opModel.statusText = "Sent";
                        else if (msg.status.Value == 2)
                            opModel.statusText = "Not sent";
                        else if (msg.status.Value == 2)
                            opModel.statusText = "Error";

                        return opModel;
                    }
                    else
                    {
                        //Assign Return Data
                        opModel.status = msg.status.Value;
                        opModel.local = msg.local.Value;
                        opModel.success = true;
                        opModel.statusText = "Web Error";
                        return opModel;
                    }
                }

                //Assign Return Data
                opModel.status = msg.status.Value;
                opModel.local = msg.local.Value;
                opModel.success = true;
                opModel.statusText = "Error";
                return opModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion


        #region"Notification"

        public int GetTotalNotifications(long userId, int roleId)
        {
            try
            {
                NoticeData data = new NoticeData();
                if (roleId == 3)
                    data = DbContext.Database.SqlQuery<NoticeData>("select count(Id) Notice from Notifications where IsSeen=0 and ManagerId = " + userId).FirstOrDefault();
                else if (roleId == 2)
                    data = DbContext.Database.SqlQuery<NoticeData>("select count(Id) Notice from Notifications where IsSeen=0 and FiderId = " + userId).FirstOrDefault();

                return data.Notice;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IList<NotificationReturnData> GetNotificationListByUserId(long userId, int roleId = 0)
        {
            try
            {
                SqlParameter prmUserId = new SqlParameter("@userId", userId);
                SqlParameter prmRoleId = new SqlParameter("@roleId", roleId);
                IList<NotificationReturnData> dataList = DbContext.Database.SqlQuery<NotificationReturnData>("Get_Notification_By_UserRoleId_And_UserId @userId,@roleId", prmUserId, prmRoleId).ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int UpdateNotificationAsSeen(int notificationId)
        {
            try
            {
                int qrySuccess = 0;
                qrySuccess = DbContext.Database.ExecuteSqlCommand("update Notifications set isseen =1 where id=" + notificationId);

                return qrySuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        public IList<AdminAPIModel> GetAPIAdminList()
        {
            try
            {
                IList<AdminAPIModel> dataList = DbContext.Database.SqlQuery<AdminAPIModel>("Up_Get_SMS_Admin").ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IList<MonthNames> GetAllMonthsName()
        {
            try
            {
                IList<MonthNames> dataList = DbContext.Database.SqlQuery<MonthNames>("select * from MONTH_NAMES").ToList();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #region"Only For APIS"
        public APIDashboardSumamry dashboard_summary(long managerId, long fiderId, long collectorId)
        {
            try
            {
                SqlParameter prmManagerId = new SqlParameter("@mngId", managerId);
                SqlParameter prmFiderId = new SqlParameter("@fiderId", fiderId);
                SqlParameter prmCltId = new SqlParameter("@cltId", collectorId);
                APIDashboardSumamry dataList = DbContext.Database.SqlQuery<APIDashboardSumamry>("Get_APPS_Dashboard_Summary @mngId,@fiderId,@cltId", prmManagerId, prmFiderId, prmCltId).FirstOrDefault();

                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}
