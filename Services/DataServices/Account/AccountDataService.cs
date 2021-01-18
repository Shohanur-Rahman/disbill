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

namespace Services.DataServices.Account
{
    public class AccountDataService : EntityContextDataService<User>
    {
        public AccountDataService(AppDbContext dbContext) : base(dbContext)
        {
            DbContext.Database.CommandTimeout = SiteSettings.DbTimeOut;
        }

        public loginSuccessData Login(string userName, string password)
        {
            try
            {
                loginSuccessData loginData = new loginSuccessData();
                string loginMessage = "You are logged in successfully.";
                User user = new User();
                loginData.IsLoginError = true;
                //Check Currect user
                string encryptPassword = SimpleCryptService.Factory().Encrypt(password);
                user = DbContext.Set<User>().FirstOrDefault(x => (x.MobileNumber == userName) && x.EncPassword == encryptPassword &&
                   x.IsActivated == true && x.IsAdminApproved == true);

               


                if (user != null)
                {
                    APIKeysTable token = DbContext.Set<APIKeysTable>().Where(x => x.UserId == user.Id).FirstOrDefault();

                    if (user.RoleId == 1)
                        loginData.role = "Super Admin";
                    else if (user.RoleId == 2)
                        loginData.role = "Admin";
                    else if (user.RoleId == 3)
                        loginData.role = "Manager";
                    else if (user.RoleId == 4)
                        loginData.role = "Collector";

                    loginData.acces_token = token.acces_token;
                    loginData.IsLoginError = false;
                    loginData.User = user;
                    loginData.LoginMessage = loginMessage;
                    return loginData;

                }
                else {
                    //Check user name
                    User mobile = DbContext.Set<User>().FirstOrDefault(x => (x.MobileNumber == userName));
                    if (mobile == null)
                    {
                        loginData.IsLoginError = true;
                        loginMessage = "মোবাইল নম্বর খুঁজে পাওয়া যায়নি";
                        loginData.User = null;
                        loginData.LoginMessage = loginMessage;
                        return loginData;
                    }
                    else {
                        User userPassword = DbContext.Set<User>().FirstOrDefault(x => (x.MobileNumber == userName) && x.EncPassword == encryptPassword);
                        if (userPassword == null)
                        {
                            loginData.IsLoginError = true;
                            loginMessage = "সঠিক পাসওয়ার্ড দিন";
                            loginData.User = null;
                            loginData.LoginMessage = loginMessage;
                            return loginData;
                        }
                        else {
                            loginData.IsLoginError = true;
                            loginMessage = "আপনার এ্যাকাউন্টটি বন্ধ আছে, আপনার এডমিনিস্ট্রেটর এর সাথে যোগাযোগ করুন";
                            loginData.User = null;
                            loginData.LoginMessage = loginMessage;
                            return loginData;

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                loginSuccessData loginData = new loginSuccessData();
                string loginMessage = ex.Message.ToString();
                loginData.LoginMessage = loginMessage;
                loginData.error = loginMessage;
                return loginData;
            }

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


        public UserOperationModel UpdatePassword(UserOperationModel mObj)
        {
            UserOperationModel loginData = new UserOperationModel();
            try
            {
                string loginMessage = "";
                string password = CreateRandomPassword(6);
                //Check Currect user
                string encryptPassword = SimpleCryptService.Factory().Encrypt(password);
                
                //Check user name
                User user = DbContext.Set<User>().FirstOrDefault(x => (x.MobileNumber == mObj.User.MobileNumber));
                if (user == null)
                {
                    loginData.isAddOperation = false;
                    loginMessage = "মোবাইল নম্বর খুঁজে পাওয়া যায়নি";
                    loginData.User = null;
                    loginData.message = loginMessage;
                    return loginData;
                }

                user.Password = password;
                user.EncPassword = encryptPassword;
                DbContext.SaveChanges();

                Messages msg = new Messages();
                msg.mobile = mObj.User.MobileNumber;
                msg.message = "আপনার ডিজিপে প্রোফাইল আপডেট করা হয়েছে। আপনার ডিজিপে একাউন্টি " + mObj.User.MobileNumber + " পাসওয়ার্ডটি " + user.Password + ",ধন্যবাদ ডিজিপে।";
                msg.status = 0;
                msg.local = 0;
                DbContext.Set<Messages>().Add(msg);
                DbContext.SaveChanges();

                loginData.isAddOperation = true;
                loginMessage = "পাসওয়ার্ড পরিবর্তন করা হয়েছে।  ";
                loginData.User = null;
                loginData.message = loginMessage;
                return loginData;

            }
            catch (Exception ex)
            {
                string loginMessage = ex.Message.ToString();
                loginData.message = loginMessage;
                return loginData;
            }

        }





    }
}
