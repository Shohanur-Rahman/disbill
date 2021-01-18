using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Domain.dishbill
{

    [Table("MONTH_NAMES")]
    public class MonthNames
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SortName { get; set; }
    }


    public class GrahokDataForPayBill {
        public long id { get; set; }
        public int line_rate { get; set; }
        public int number_of_month { get; set; }
        public DateTime last_bill_date { get; set; }
    }


    public class UserNameAndIdList {
        public long Id { get; set; }
        public string Name { get; set; }
    }


    [Table("BillPaid")]
    public class BillPaidTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? CollectedBill { get; set; }
        public int? DueAmount { get; set; }
        public DateTime? SubmitedDate { get; set; }
        public bool? ManagerApproved { get; set; }
        public bool? FiderApproved { get; set; }
        public long? ManagerId { get; set; }
        public long? FiderId { get; set; }
        public DateTime? ManagerApprovedDate { get; set; }
        public DateTime? FiderApprovedDate { get; set; }
        public long SubmitedId { get; set; }

    }


    public class BillPaidOperationModel
    {
        public BillPaidTable Billpaid { get; set; }
        public string OperationMessage { get; set; }
        public bool isOperationSuccess { get; set; }
        public string error { get; set; }
    }

    public class BillPaidAPIModel
    {
        public int Id { get; set; }
        public int? CollectedBill { get; set; }
        public int? DueAmount { get; set; }
        public DateTime? SubmitedDate { get; set; }
        public bool ManagerApproved { get; set; }
        public bool FiderApproved { get; set; }
        public long ManagerId { get; set; }
        public long FiderId { get; set; }
        public DateTime? ManagerApprovedDate { get; set; }
        public DateTime FiderApprovedDate { get; set; }
        public string acces_token { get; set; }
    }


    [Table("AreaList")]
    public class AreaList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public long ManagerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
    }

    public class AreaApiModal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long ManagerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
        public string acces_token { get; set; }
    }


    public class GetAreaNameListByManagerId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long ManagerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
        public int total_grahok { get; set; }
        public int due_grahok { get; set; }
    }


    [Table("GRAHOK")]
    public class GRAHOK_TABLE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public int? LineRate { get; set; }
        public int? DueAmount { get; set; }
        public int? AdvanceAmount { get; set; }
        public long? FiderId { get; set; }
        public long? ManagerId { get; set; }
        public string HouseOwnerName { get; set; }
        public string GrahokId { get; set; }
        public int? NumberOfTV { get; set; }
        public int? GrahokRoleId { get; set; }
        public bool IsActivated { get; set; }
        public string CompanyName { get; set; }
        public int? AreaId { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string House { get; set; }
        public string Road { get; set; }
        public string PostOffice { get; set; }
        public string Thana { get; set; }
        public string Zila { get; set; }
        public string NIDNo { get; set; }
        public string ReferenceName { get; set; }
        public string ReferenceMobile { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedId { get; set; }
        public DateTime? bill_paid_date { get; set; }
        public DateTime? last_paid { get; set; }
    }




    [Table("BillHistories")]
    public class BillHistoryTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long? GrahokId { get; set; }
        public long? FiderId { get; set; }
        public long? ManagerId { get; set; }
        public long? CollectorId { get; set; }
        public int BillAmount { get; set; }
        public int DueAmount { get; set; }
        public int? AdvanceAmount { get; set; }
        public DateTime? CollectedDate { get; set; }
        public DateTime? BillMonth { get; set; }
        public long? CollectedBy { get; set; }
        public int CollectorRoleId { get; set; }


    }


    public class BillOperationModel
    {
        public BillHistoryTable BillTable { get; set; }
        public Messages Message { get; set; }
        public int MonthId { get; set; }
        public string OperationMessage { get; set; }
        public bool isOperationSuccess { get; set; }
        public string UserName { get; set; }
        public string error { get; set; }
        public object data { get; set; }
        public string message { get; set; }
        public string acces_token { get; set; }
    }


    public class BillCollectOperationModel
    {
        public BillHistoryTable BillTable { get; set; }
        public Messages Message { get; set; }
        public string OperationMessage { get; set; }
        public bool isOperationSuccess { get; set; }
        public string UserName { get; set; }

    }

    public class GrahokOperationModel
    {
        public GRAHOK_TABLE GrahokTable { get; set; }
        public string OperationMessage { get; set; }
        public bool isOperationSuccess { get; set; }
    }

    public class GraholAPIModal
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? LineRate { get; set; }
        public int? DueAmount { get; set; }
        public int? AdvanceAmount { get; set; }
        public long? FiderId { get; set; }
        public long? ManagerId { get; set; }
        public string HouseOwnerName { get; set; }
        public string GrahokId { get; set; }
        public int? NumberOfTV { get; set; }
        public int? GrahokRoleId { get; set; }
        public bool IsActivated { get; set; }
        public string CompanyName { get; set; }
        public int? AreaId { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string House { get; set; }
        public string Road { get; set; }
        public string PostOffice { get; set; }
        public string Thana { get; set; }
        public string Zila { get; set; }
        public string NIDNo { get; set; }
        public string ReferenceName { get; set; }
        public string ReferenceMobile { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedId { get; set; }
        public string acces_token { get; set; }
    }


    public class TotalGrahokRow
    {
        public int total_row { get; set; }
    }

    public class GrahokTableReturnColumns
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? LineRate { get; set; }
        public int? DueAmount { get; set; }
        public int? AdvanceAmount { get; set; }
        public long? ManagerId { get; set; }
        public long? FiderId { get; set; }
        public string HouseOwnerName { get; set; }
        public string GrahokId { get; set; }
        public int? NumberOfTV { get; set; }
        public int? GrahokRoleId { get; set; }
        public bool IsActivated { get; set; }
        public string CompanyName { get; set; }
        public int? AreaId { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string House { get; set; }
        public string Road { get; set; }
        public string PostOffice { get; set; }
        public string Thana { get; set; }
        public string Zila { get; set; }
        public string NIDNo { get; set; }
        public string ReferenceName { get; set; }
        public string ReferenceMobile { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedId { get; set; }
        public int? TotalAmount { get; set; }
        public string paid_date { get; set; }
        public string AreaName { get; set; }
        public int? total_due { get; set; } 
    }


    public class SMSOperationModel
    {
        public Messages Message { get; set; }
        public long GrahokId { get; set; }
        public long UserId { get; set; }
        public string OperationMessage { get; set; }
        public bool isOperationSuccess { get; set; }
        public string SendMode { get; set; }
        public long LoginUserId { get; set; }
        public int UserRole { get; set; }
        public string MobileNumberList { get; set; }
        public string acces_token { get; set; }
        public string message_text { get; set; }

    }

    [Table("GrahokAreaMapper")]
    public class GrahokAreaMapper
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public int AreaId { get; set; }
        public long UserId { get; set; }
    }


    [Table("Messages")]
    public class Messages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public string mobile { get; set; }
        public string message { get; set; }
        public int? status { get; set; }
        public int? local { get; set; }
    }

    public class SMSUpdateStatus
    {
        public int status { get; set; }
        public bool success { get; set; }
        public int local { get; set; }
        public long id { get; set; }
        public string statusText { get; set; }
    }

    public class Bill_History_For_Grahok
    {
        public int BillAmount { get; set; }
        public int DueAmount { get; set; }
        public int AdvanceAmount { get; set; }
        public DateTime? CollectedDate { get; set; }
        public string BillDate { get; set; }
        public string Collector { get; set; }
        public string CollectDate { get; set; }
        public int? total_due { get; set; }
    }

    public class GrahokNameMobile_FiderSignature
    {
        public string GrahokName { get; set; }
        public string Mobile { get; set; }
        public string FiderSign { get; set; }
    }


    public class BilHistory_For_Manager_Fider_Collector
    {
        public int TotalColelction { get; set; }
        public int? BillAmount { get; set; }
        public int? DueAmount { get; set; }
        public int? Advance { get; set; }
    }

    [Table("Notifications")]
    public class Notifications
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Notification { get; set; }
        public bool IsSeen { get; set; }
        public long? CollectorId { get; set; }
        public long? ManagerId { get; set; }
        public long? FiderId { get; set; }
        public DateTime? DateTime { get; set; }
    }

    public class NoticeOpModal
    {
        public Notifications Notification { get; set; }
        public string OperationMessage { get; set; }
        public bool isOperationSuccess { get; set; }
    }

    public class allSMSMobile
    {
        public string mobile { get; set; }
    }

    public class NoticeData
    {
        public int Notice { get; set; }
    }


    public class NotificationReturnData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Notification { get; set; }
        public bool IsSeen { get; set; }
        public long? CollectorId { get; set; }
        public long? ManagerId { get; set; }
        public long? FiderId { get; set; }
        public DateTime? DateTime { get; set; }
        public string Collector { get; set; }
        public string Manager { get; set; }
    }
    

    public class Get_Bill_Collection_History
    {
        public long Id { get; set; }
        public int BillAmount { get; set; }
        public int AdvanceAmount { get; set; }
        public string Grahok { get; set; }
        public string CollecDate { get; set; }
        public string BillMonth { get; set; }
        public string Collector { get; set; }
    }

    public class UserTableDataParams
    {
        public string api_key { get; set; }
        public long? managerId { get; set; }
        public long? fiderId { get; set; }
        public long? adminId { get; set; }
        public long grahokId { get; set; }
        public int? RoleId { get; set; }
    }

    public class GetDataErrorFormate
    {
        public string error { get; set; }
        public object data { get; set; }
        public string message { get; set; }
    }

    public class AdminAPIModel
    {
        public string email { get; set; }
        public string mobile { get; set; }
        public string company { get; set; }
        public string sign { get; set; }
        public int role { get; set; }
        public string thana { get; set; }
    }

    public class CollectionSummary
    {
        public int total_amount { get; set; }
        public long total_collection { get; set; }
        public long collector_collection { get; set; }
        public long manager_collection { get; set; }
        public long fider_collection { get; set; }
        public long due_amount { get; set; }
    }

    public class BillPaidSummary
    {
        public long Id { get; set; }
        public int? CollectedBill { get; set; }
        public int? DueAmount { get; set; }
        public string PaydName { get; set; }
        public string PayDate { get; set; }
        public bool? ManagerApproved { get; set; }
        public bool? FiderApproved { get; set; }
    }



}
