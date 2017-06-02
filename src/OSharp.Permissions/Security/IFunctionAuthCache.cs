using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Dependency;


namespace OSharp.Core.Security
{
    /// <summary>
    /// 定义功能权限缓存的功能
    /// </summary>
    public interface IFunctionAuthCache<TFunctionKey> : ISingletonDependency
    {
        /// <summary>
        /// 创建功能权限缓存
        /// </summary>
        void BuildCaches();

        /// <summary>
        /// 移除指定功能的缓存
        /// </summary>
        /// <param name="functionIds">功能编号集合</param>
        void RemoveFunctionCaches(TFunctionKey[]functionIds);
        
        /// <summary>
        /// 移除指定用户的缓存
        /// </summary>
        /// <param name="userNames">用户编号集合</param>
        void RemoveUserCaches(string[]userNames);

        /// <summary>
        /// 获取能执行指定功能的所有角色
        /// </summary>
        /// <param name="functionId">功能编号</param>
        /// <returns>能执行功能的角色名称集合</returns>
        string[] GetFunctionRoles(TFunctionKey functionId);

        /// <summary>
        /// 获取指定用户的所有特权功能
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户的所有特权功能</returns>
        TFunctionKey[] GetUserFunctions(string userName);
    }
}
