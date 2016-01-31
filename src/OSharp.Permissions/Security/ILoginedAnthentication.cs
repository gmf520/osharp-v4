using System.Security.Claims;

using OSharp.Core.Dependency;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义登录验证类型的功能权限检查
    /// </summary>
    /// <typeparam name="TFunction">功能类型</typeparam>
    /// <typeparam name="TFunctionKey">功能编号类型</typeparam>
    public interface ILoginedAnthentication<in TFunction, TFunctionKey> : ISingletonDependency
        where TFunction : FunctionBase<TFunctionKey>
    {
        /// <summary>
        /// 执行功能权限验证
        /// </summary>
        /// <param name="user">在线用户信息</param>
        /// <param name="function">功能信息</param>
        /// <returns>权限验证结果</returns>
        AuthenticationResult Authenticate(ClaimsPrincipal user, TFunction function);
    }
}
