using System;
using Microsoft.Owin.Hosting;

namespace OSharp.Demo.Web.Start
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("启动中...\n");

            string baseAddress = "http://localhost:9001/"; // 管道地址
            //开启管道监听
            WebApp.Start<OSharp.Demo.Web.Startup>(url: baseAddress);

            Console.WriteLine("启动成功！\n");
            Console.WriteLine("访问地址：{0} \n", baseAddress);

            Console.WriteLine("按任意键结束...");
            Console.ReadKey();
        }
    }
}
