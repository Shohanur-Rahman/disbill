using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Domain.APIModels
{

    [Table("api_keys")]
    public class APIKeysTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string acces_token { get; set; }
        public long UserId { get; set; }
    }

    public class APIGetMethodParams
    {
        public string acces_token { get; set; }
        public long? managerId { get; set; }
        public long? fiderId { get; set; }
        public long? adminId { get; set; }
        public long grahokId { get; set; }
        public int? RoleId { get; set; }
        public int notififcation_id { get; set; }
        public long user_id { get; set; }
        public int area_id { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public string messge_ip { get; set; }
        public string search_text { get; set; }
    }

    public class APIReturnObject {
        public string error { get; set; }
        public object data { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public int total_row { get; set; }
    }



    public class APIBillCollectionModal
    {
        public string acces_token { get; set; }
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

    }



    public class APIUpdatePasswordModal
    {
        public string acces_token { get; set; }
        public string password { get; set; }
    }


    public class APIReportFilterParam
    {
        public string acces_token { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public long manager_id { get; set; }
        public long fider_id { get; set; }
        public long collector_id { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }

    public class APIDashboardSumamry {
        public int total_grahok { get; set; }
        public int? total_bill { get; set; }
        public int? due_amount { get; set; }
        public int? advance { get; set; }
    }


    public class APILIVECHATMODAL {
        public long Id { get; set; }
        public string IP_Address { get; set; }
        public string User_Message { get; set; }
        public string ReplyMessage { get; set; }
        public bool IsRead { get; set; }
        public long? ReaderId { get; set; }
        public long? ReceiverId { get; set; }
        public long? UserId { get; set; }
        public DateTime SendingDate { get; set; }
        public string acces_token { get; set; }
    }





}
