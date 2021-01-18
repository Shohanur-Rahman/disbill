using Core.DomainService;
using Services.DataServices.API;
using Services.Domain;
using Services.Domain.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainServices.API
{
    public class APIDomainServices : DomainService<APIKeysTable, long>
    {
        private readonly APIDataServices _APIDataServices;
        public APIDomainServices(APIDataServices dataService) : base(dataService)
        {
            _APIDataServices = dataService;
        }

        public User GetUserByTokenKey(string key)
        {
            return _APIDataServices.GetUserByTokenKey(key);
        }

    }
}
