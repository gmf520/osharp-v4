using OSharp.Core.Data;
using OSharp.Demo.Contracts;
using OSharp.Demo.Models.Identity;


namespace OSharp.Demo.Services
{
    public class TestService : ITestContract
    {
        /// <summary>
        /// 获取或设置 用户仓储对象
        /// </summary>
        public IRepository<User, int> UserRepository { get; set; }

        #region Implementation of ITestContract

        public void Test()
        {
            int count = UserRepository.UpdateDirect(m => true, user => new User() { NickName = "柳柳英侠112" });

        }

        #endregion
    }
}
