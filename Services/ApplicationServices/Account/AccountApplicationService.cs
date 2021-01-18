using Services.ApplicationEntity.Account;
using Services.AppServices;
using Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ApplicationServices.Account
{
    public class AccountApplicationService
    {
        protected AccountAppEntity _accountAppEntityInstance { get; set; }

        public AccountApplicationService(AccountAppEntity accountAppEntity)
        {
            _accountAppEntityInstance = accountAppEntity;
        }


        public User Login(string userName, string password)
        {
            return _accountAppEntityInstance.AccountDomainService.Login(userName, password);
        }

        public User ProcessNewUserLogin(string userName, string password, string key, long cookieUserId, string userIp, bool isfromSingIn)
        {
            return _accountAppEntityInstance.AccountDomainService.ProcessNewUserLogin(userName, password, key, cookieUserId, userIp, isfromSingIn);
        }

        public bool SaveNewBazarData(BAZARS_DATA mObj)
        {
            return _accountAppEntityInstance.AccountDomainService.SaveNewBazarData(mObj);
        }

        public bool SaveNewMealData(MEAL_DATA mObj)
        {
            return _accountAppEntityInstance.AccountDomainService.SaveNewMealData(mObj);
        }

        public bool SaveNewMemberToDB(User mObj,long adminUserId)
        {
            return _accountAppEntityInstance.AccountDomainService.SaveNewMemberToDB(mObj, adminUserId);
        }

    }
}
