using Autofac;
using Autofac.Extras.DynamicProxy2;
using Autofac.Integration.WebApi;
using CeeKee.PublicLib.Autofac;
using IService;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using Utils;
//using Utils.AOP;

namespace AutofacTest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoFacRegist();
        }


        /// <summary>
        /// 使用AutoFac实现依赖注入
        /// </summary>
        private void AutoFacRegist()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            SetupResolveRules(builder);  //注入  
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();   //PropertiesAutowired 属性自动注入

            //UI项目只用引用service和repository的接口，不用引用实现的dll。
            var IServices = Assembly.Load("IService");
            var Services = Assembly.Load("Service");
            var IRepository = Assembly.Load("IRepository");
            var Repository = Assembly.Load("Repository");

            //根据名称约定（服务层的接口和实现均以Service结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(IServices, Services)
              .Where(t => t.Name.EndsWith("Service") && t.GetCustomAttribute(typeof(ManuallyRegisterAttribute)) == null)
              .PropertiesAutowired()
              .EnableInterfaceInterceptors()  //启动接口拦截器 （启动拦截器有两种方式：EnableInterfaceInterceptors ——动态创建一个接口代理,方法特性需要打在接口方法上，而不是实现类的方法上  EnableClassInterceptors——创建一个目标类的子类代理，只会拦截虚方法）
              .AsImplementedInterfaces();

            //根据名称约定（数据访问层的接口和实现均以Repository结尾），实现数据访问接口和数据访问实现的依赖
            builder.RegisterAssemblyTypes(IRepository, Repository)
              .Where(t => t.Name.EndsWith("Repository"))
              .PropertiesAutowired()
              .EnableInterfaceInterceptors()
              .AsImplementedInterfaces();

            //通过serviceName 区分一个接口多个实现
            //builder.RegisterType<HotelService>().Named<IHotelService>("hotelservice").EnableInterfaceInterceptors();
            //builder.RegisterType<AHotelService>().Named<IHotelService>("ahotelservice");

            //通过serviceKey（可以是枚举等）区分一个接口多个实现
            builder.RegisterType<HotelService>().Keyed<IHotelService>(HotelServiceType.HotelService).EnableInterfaceInterceptors();
            builder.RegisterType<AHotelService>().Keyed<IHotelService>(HotelServiceType.AHotelService);

            //注册拦截器
            builder.RegisterType<GlobalInterceptor>();
        }
    }

    public enum HotelServiceType
    {
        HotelService,

        AHotelService,
    }
}
