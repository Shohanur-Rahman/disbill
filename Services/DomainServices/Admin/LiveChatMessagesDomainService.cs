using Core.DomainService;
using Services.DataServices.Admin;
using Services.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainServices.Admin
{
    public class LiveChatMessagesDomainService : DomainService<LiveChatMessages, long>
    {
        private readonly LiveChatMessagesDataService _liveChatMessagesDataService;
        public LiveChatMessagesDomainService(LiveChatMessagesDataService liveChatMessagesDataService) : base(liveChatMessagesDataService)
        {
            _liveChatMessagesDataService = liveChatMessagesDataService;
        }

        public bool SaveLiveChatMessages(LiveChatMessages mObj, long adminUserId, bool isFromAddmin)
        {
            return _liveChatMessagesDataService.SaveLiveChatMessages(mObj, adminUserId, isFromAddmin);
        }

        public IList<Get_LiveChatMessagesByIpAddress> GetLiveChatMessagesByIpAddress(string ipAddress, long receiverId)
        {
            return _liveChatMessagesDataService.GetLiveChatMessagesByIpAddress(ipAddress, receiverId);
        }

        public IList<Get_LiveChatMessageListFromAdmin> GetAllChatMessageFromAdmin(long receiverId)
        {
            return _liveChatMessagesDataService.GetAllChatMessageFromAdmin(receiverId);
        }

    }
}
