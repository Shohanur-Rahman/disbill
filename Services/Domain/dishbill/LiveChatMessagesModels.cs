using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Domain.Admin
{

    [Table("LiveChatMessages")] 
    public class LiveChatMessages {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string IP_Address { get; set; }
        public string User_Message { get; set; }
        public string ReplyMessage { get; set; }
        public bool IsRead { get; set; }
        public long? ReaderId { get; set; }
        public long? ReceiverId { get; set; }
        public long? UserId { get; set; }
        public DateTime SendingDate { get; set; }
    }


    public class Get_LiveChatMessagesByIpAddress {
        public long Id { get; set; }
        public string IP_Address { get; set; }
        public string User_Message { get; set; }
        public string ReplyMessage { get; set; }
        public bool IsRead { get; set; }
        public long? ReaderId { get; set; }
        public long? UserId { get; set; }
        public DateTime SendingDate { get; set; }
        public string Admin_Name { get; set; }
    }

    public class Get_LiveChatMessageListFromAdmin {
        public int TotalUnRead { get; set; }
        public string IP_Address { get; set; }
        public long? receiverid { get; set; }
    }

}
