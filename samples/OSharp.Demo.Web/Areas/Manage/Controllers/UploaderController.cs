using OSharp.Demo.Contracts;
using Qiniu.IO.Resumable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSharp.Demo.Web.Areas.Manage.Controllers
{
    public class UploaderController : Controller
    {
        const int ChunkSize = 1024 * 1024;

        IQiniuContract QiniuContract { get; set; }

        // GET: Manage/Uploader
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult FileUpload(HttpPostedFileBase file)
        {


            if (Request.Files.Count==0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }

            string extension = Path.GetExtension(file.FileName);
            string filePathName = Guid.NewGuid().ToString("N") + extension;

            if (!Directory.Exists(@"c:\temp"))
            {
                Directory.CreateDirectory(@"c:\temp");
            }
            
            filePathName = @"c:\temp\" + filePathName;
            file.SaveAs(filePathName);

            Stream stream = file.InputStream;

            //using (FileStream fs = stream)
            //{

            //}


            ResumablePutFile("magiccook", Guid.NewGuid().ToString("N"),stream);

            return Json(new { success = true });
        }

        public void ResumablePutFile(string bucket, string key, System.IO.Stream stream)
        {

            Qiniu.Conf.Config.ACCESS_KEY = "_DN2l8Cb2ZjfajZWFiJ7uLsRgasrqDDlGkkq5bBS";
            Qiniu.Conf.Config.SECRET_KEY = "34Wig1rOV0wLnGGQ7E7aUDrogXdT8lDLKDsifA_l";

            Qiniu.RS.PutPolicy policy = new Qiniu.RS.PutPolicy(bucket, 3600);
            string uptoken = policy.Token();
            Qiniu.IO.Resumable.Settings setting = new Settings();
            ResumablePutExtra extra = new ResumablePutExtra();
            ResumablePut client = new ResumablePut(setting, extra);
            client.PutFile(uptoken, Guid.NewGuid().ToString("N"), stream);
        }

        public class UploadFileInfo
        {
            /// <summary>
            /// 文件Guid
            /// </summary>
            public string Guid { get; set; }
            /// <summary>
            /// 源文件名称
            /// </summary>
            public string FileName { get; set; }
            /// <summary>
            /// 保存文件名称
            /// </summary>
            public string SaveName { get; set; }

        }


        private void WriteStream(BinaryReader br, string filename)
        {
            byte[] fileContents = new byte[] { };
            var buffer = new byte[ChunkSize];

            while (br.BaseStream.Position<br.BaseStream.Length-1)
            {
                if (br.Read(buffer, 0, ChunkSize) > 0)
                {
                    fileContents = fileContents.Concat(buffer).ToArray();
                }
            }

            


                using (var fs=new FileStream(@"C:\\temp\\"+DateTime.Now.ToString("yyyyMMddHHHmmss")+Path.GetExtension(filename).ToLower(),FileMode.Create))
            {
                using (var bw=new BinaryWriter(fs))
                {
                    bw.Write(fileContents);
                }
            }
        }

    }
}