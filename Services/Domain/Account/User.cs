using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Services.Domain
{

    public class UserOperationModel
    {
        public User User { get; set; }
        public bool isAddOperation { get; set; }
        public string message { get; set; }
    }

    public class FiderRegisterModal {
        public long Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public long? UserManagerId { get; set; }
        public long? UserFiderId { get; set; }
        public bool IsActivated { get; set; }
        public bool IsAdminApproved { get; set; }
        public string Avatar { get; set; }
        public string CompanyName { get; set; }
        public string CompanySignature { get; set; }
        public string CompanyLogo { get; set; }
        public int? AreaId { get; set; }
        public string FathersName { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string EncPassword { get; set; }
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
        public string area_ids { get; set; }
    }

    [Table("USERS")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } 
        public string Name { get; set; }
        public int RoleId { get; set;}
        public long? UserManagerId { get; set; }
        public long? UserFiderId { get; set; }
        public bool IsActivated { get; set; }
        public bool IsAdminApproved { get; set; }
        public string Avatar { get; set; }
        public string CompanyName { get; set; }
        public string CompanySignature { get; set; }
        public string CompanyLogo { get; set; }
        public int? AreaId { get; set; }
        public string FathersName { get; set; }
        public string ComplainNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string EncPassword { get; set; }
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

    }

    public class Super_Admin_Get_Manager_List {
        public long Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public bool IsActivated { get; set; }
        public bool IsAdminApproved { get; set; }
        public string Avatar { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public int? AreaId { get; set; }
        public string FathersName { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string EncPassword { get; set; }
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
        public string AreaName { get; set; }
        public string fider_name { get; set; }
        public string manger_name { get; set; }
    }

    public class loginSuccessData {
        public User User { get; set; }
        public string LoginMessage { get; set; }
        public bool IsLoginError { get; set; }
        public string error { get; set; }
        public string role { get; set; }
        public string acces_token { get; set; }
    }

    public class UserOperationData { 
        public User User { get; set; }
        public string OperationMessage { get; set; }
        public bool isOperationSuccess { get; set; }
        public HttpPostedFileBase UserAvatarThumb { get; set; }
        public HttpPostedFileBase CompanyAvatar { get; set; }
        public string error { get; set; }
        public string area_ids { get; set; }
    }
    
}
