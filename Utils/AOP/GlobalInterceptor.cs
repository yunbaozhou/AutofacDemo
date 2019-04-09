using Castle.DynamicProxy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.AOP
{
    public class GlobalInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var customAttributes = GetCustomAttributes(invocation);
            PreProcess(invocation, customAttributes);
            invocation.Proceed();
            AfterProcess(invocation, customAttributes);

            string log = "Test";
            invocation.ReturnValue = $"拦截器修改结果——{invocation.ReturnValue}——{JsonConvert.SerializeObject(customAttributes)}";
        }

        /// <summary>
        /// 获取方法特性
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        private static List<BaseCustomInterceptorAttribute> GetCustomAttributes(IInvocation invocation)
        {
            List<BaseCustomInterceptorAttribute> customInterceptorAttributes = new List<BaseCustomInterceptorAttribute>();
            invocation.Method.GetCustomAttributes(false).ToList().ForEach(item =>
            {
                if (item.GetType().IsSubclassOf(typeof(BaseCustomInterceptorAttribute)))
                {
                    customInterceptorAttributes.Add(item as BaseCustomInterceptorAttribute);
                }
            });

            return customInterceptorAttributes;
        }

        /// <summary>
        /// 目标方法执行前执行
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="interceptorAttributes"></param>
        private void PreProcess(IInvocation invocation, IEnumerable<BaseCustomInterceptorAttribute> interceptorAttributes)
        {
            interceptorAttributes.ToList().ForEach(item =>
            {
                Task.Factory.StartNew(() =>
                {
                    item.PreProcess(invocation);
                });
            });
        }

        /// <summary>
        /// 目标方法执行后执行
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="interceptorAttributes"></param>
        private void AfterProcess(IInvocation invocation, IEnumerable<BaseCustomInterceptorAttribute> interceptorAttributes)
        {
            interceptorAttributes.ToList().ForEach(item =>
            {
                Task.Factory.StartNew(() =>
                {
                    item.AfterProcess(invocation);
                });
            });
        }
    }
}
