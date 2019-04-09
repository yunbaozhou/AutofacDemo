using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils.AOP
{
    /// <summary>
    /// 自定义拦截器特性基类
    /// </summary>
    public abstract class BaseCustomInterceptorAttribute : Attribute
    {
        /// <summary>
        /// 特性排序值
        /// </summary>
        public abstract int Order { get; }

        /// <summary>
        /// 目标方法执行前执行
        /// </summary>
        /// <param name="invocation"></param>
        public abstract void PreProcess(IInvocation invocation);

        /// <summary>
        /// /目标方法执行后执行
        /// </summary>
        /// <param name="invocation"></param>
        public abstract void AfterProcess(IInvocation invocation);
    }
}
