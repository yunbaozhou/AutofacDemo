using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Autofac.Features.Indexed;

namespace Utils
{
    public class AutofacManagerUtils
    {
        /// <summary>
        /// 通过服务名获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static T GetInstance<T>(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                return default(T);
            }
            var scope = GlobalConfiguration.Configuration.DependencyResolver.GetRootLifetimeScope();
            return scope.ResolveNamed<T>(serviceName);
        }

        /// <summary>
        /// 通过serviceKey（枚举）获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceKey"></param>
        /// <returns></returns>
        public static T GetInstance<T>(Enum serviceKey)
        {
            var scope = GlobalConfiguration.Configuration.DependencyResolver.GetRootLifetimeScope();
            IIndex<Enum, T> IIndex = scope.Resolve<IIndex<Enum, T>>();
            return IIndex[serviceKey];
        }
    }
}
