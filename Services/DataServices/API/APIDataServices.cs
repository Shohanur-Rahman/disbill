using Core.DataService;
using Services.DataContext;
using Services.Domain;
using Services.Domain.APIModels;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices.API
{
    public class APIDataServices : EntityContextDataService<APIKeysTable>
    {
        public APIDataServices(AppDbContext dbContext) : base(dbContext)
        {
            DbContext.Database.CommandTimeout = SiteSettings.DbTimeOut;
        }

        public User GetUserByTokenKey(string key)
        {
            User data = null;
            try
            {
                APIKeysTable userId = DbContext.Set<APIKeysTable>().Where(x => x.acces_token == key).FirstOrDefault();
                if (userId != null && userId.UserId > 0) {
                    data = DbContext.Set<User>().Where(x => x.Id == userId.UserId).FirstOrDefault();
                }
                
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
