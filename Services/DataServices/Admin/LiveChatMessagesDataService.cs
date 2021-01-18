using Core.DataService;
using Services.DataContext;
using Services.Domain.Admin;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices.Admin
{
    public class LiveChatMessagesDataService : EntityContextDataService<LiveChatMessages>
    {
        public LiveChatMessagesDataService(AppDbContext dbContext) : base(dbContext)
        {
            DbContext.Database.CommandTimeout = SiteSettings.DbTimeOut;
        }

        public bool SaveLiveChatMessages(LiveChatMessages mObj, long adminUserId,bool isFromAddmin)
        {


            if (mObj != null && isFromAddmin == false)
            {
                mObj.SendingDate = DateTime.Now;
                DbContext.Set<LiveChatMessages>().Add(mObj);
                DbContext.SaveChanges();

                return true;
            }
            else {
                mObj.SendingDate = DateTime.Now;
                DbContext.Set<LiveChatMessages>().Add(mObj);
                DbContext.SaveChanges();
                string cmd = "update [LiveChatMessages] set IsRead = 1 where IP_Address= '" + mObj.IP_Address + "'";
                int commentCount = DbContext.Database.ExecuteSqlCommand(cmd);

                return true;
            }

        }

        public IList<Get_LiveChatMessagesByIpAddress> GetLiveChatMessagesByIpAddress(string ipAddress, long receiverId)
        {
            SqlParameter prmIpAddress = new SqlParameter("@ipAddress", ipAddress);
            SqlParameter prmReceiverId = new SqlParameter("@receiverId", receiverId);
            IList<Get_LiveChatMessagesByIpAddress> dataList = DbContext.Database.SqlQuery<Get_LiveChatMessagesByIpAddress>("Get_LiveChatMessagesByIpAddress @ipAddress,@receiverId", prmIpAddress, prmReceiverId).ToList();
            return dataList;
        }

        public IList<Get_LiveChatMessageListFromAdmin> GetAllChatMessageFromAdmin(long receiverId)
        {
            SqlParameter prmReceiverId = new SqlParameter("@receiverId", receiverId);
            IList<Get_LiveChatMessageListFromAdmin> dataList = DbContext.Database.SqlQuery<Get_LiveChatMessageListFromAdmin>("Get_LiveChatMessageListFromAdmin @receiverId", prmReceiverId).ToList();
            return dataList;
        }


    }

}
