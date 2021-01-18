using System.Web.Mvc;
using System.Configuration;
using Unity;
using Unity.Mvc5;
using Unity.Injection;
using Services.DomainServices;
using Services.DataServices;
using Services.DataContext;
using Services.PDFService;

namespace Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            //== Attach or Register Database Context In the Application ===

            //DbContextOptions<AppDbContext> option = new DbContextOptionsBuilder<AppDbContext>()
            //.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString).Options;

          //Services.DataContext.AppDbContext dbContext = new Services.DataContext.AppDbContext();
            container.RegisterType<AppDbContext>(new InjectionConstructor());
            //=====

            // e.g. container.RegisterType<ITestService, TestService>();


            //container.RegisterType<UserDataService>();
            //container.RegisterType<UserDomainService>();

     

            //container.RegisterType<UserDataService>();
            //container.RegisterType<UserDomainService>();

            //container.RegisterType<RoleDataService>();

            

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}