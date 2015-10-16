using OSharp.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSharp.Demo.Contracts
{
    public interface IQiniuContract: IScopeDependency
    {

        /// <summary>
        /// 普通上传
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <param name="filename"></param>
        void PutFile(string bucket, string key, string filename);

        /// <summary>
        /// 断点上传
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <param name="filename"></param>
        void ResumablePutFile(string bucket, string key, System.IO.Stream stream);
    }
}
