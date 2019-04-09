using Castle.DynamicProxy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.AOP
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LoggerAttribute : BaseCustomInterceptorAttribute
    {
        /// <summary>
        /// 排序值
        /// </summary>
        public override int Order => 1;

        /// <summary>
        /// 模块描述
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 方法功能描述
        /// </summary>
        public string MethodFuncDesc { get; set; }

        /// <summary>
        /// 异常情况下是否发送钉钉提醒
        /// </summary>
        public bool IsSendMsg { get; set; }

        /// <summary>
        /// 日志构造函数
        /// </summary>
        /// <param name="module"></param>
        /// <param name="methodFuncDesc"></param>
        /// <param name="isSendMsg"></param>
        public LoggerAttribute(string module, string methodFuncDesc, bool isSendMsg = false)
        {
            Module = module;
            MethodFuncDesc = methodFuncDesc;
            IsSendMsg = IsSendMsg;
        }

        public override void PreProcess(IInvocation invocation)
        {
            string log = $"{JsonConvert.SerializeObject(invocation.Arguments)}";
        }

        public override void AfterProcess(IInvocation invocation)
        {
            string log = $"{JsonConvert.SerializeObject(invocation.ReturnValue)}";
        }
    }
}
