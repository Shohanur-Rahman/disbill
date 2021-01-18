using Core.DomainService;
using Services.AppServices;
using Services.DataServices.Account;
using Services.Domain;
using Services.Domain.Models.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainServices.Account
{
    public class AccountDomainService : DomainService<User, long>
    {
        private readonly AccountDataService _accountDataService;
        public static Dictionary<string, int> dLoginFailed = new Dictionary<string, int>();
        public AccountDomainService(AccountDataService accountDataService) : base(accountDataService)
            {
            _accountDataService = accountDataService;
        }

        public loginSuccessData Login(string userName, string password)
        {
            return _accountDataService.Login(userName, password);
        }
        
    }

}
