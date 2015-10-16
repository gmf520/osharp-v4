using OSharp.Demo.Contracts;
using Qiniu.IO.Resumable;
using Qiniu.RS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSharp.Demo.Services
{
    public class QiniuService : IQiniuContract
    {
        public void PutFile(string bucket, string key, string filename)
        {
            throw new NotImplementedException();
        }

        public void ResumablePutFile(string bucket, string key, System.IO.Stream stream)
        {

            Qiniu.Conf.Config.ACCESS_KEY = "_DN2l8Cb2ZjfajZWFiJ7uLsRgasrqDDlGkkq5bBS";
            Qiniu.Conf.Config.SECRET_KEY = "34Wig1rOV0wLnGGQ7E7aUDrogXdT8lDLKDsifA_l";

            PutPolicy policy = new PutPolicy(bucket,3600);
            string uptoken = policy.Token();
            Settings setting = new Settings();
            ResumablePutExtra extra = new ResumablePutExtra();
            ResumablePut client = new ResumablePut(setting, extra);
            client.PutFile(uptoken, Guid.NewGuid().ToString("N"),stream);
        }
    }
}
