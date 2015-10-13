using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using Newtonsoft.Json;

using OSharp.App.Local.Initialize;
using OSharp.AutoMapper;
using OSharp.Core;
using OSharp.Core.Caching;
using OSharp.Core.Data;
using OSharp.Core.Data.Entity;
using OSharp.Core.Dependency;
using OSharp.Core.Initialize;
using OSharp.Core.Reflection;
using OSharp.Core.Security;
using OSharp.Demo.Contracts;
using OSharp.Logging.Log4Net;
using OSharp.SiteBase.Initialize;
using OSharp.Utility.Extensions;


namespace OSharp.Demo.Consoles
{
    internal class Program : ISingletonDependency
    {
        private static Program _program;

        public IIocResolver IocResolver { get; set; }

        public IIdentityContract IdentityContract { get; set; }

        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("正在初始化，请稍候……");
                Stopwatch watch = Stopwatch.StartNew();

                IServicesBuilder builder = new ServicesBuilder(new ServiceBuildOptions());
                IServiceCollection services = builder.Build();
                services.AddLog4NetServices();
                services.AddDataServices();
                services.AddAutoMapperServices();
                IIocBuilder iocBuilder = new LocalAutofacIocBuilder(services);
                IFrameworkInitializer initializer = new FrameworkInitializer();
                initializer.Initialize(iocBuilder);

                _program = iocBuilder.ServiceProvider.GetService<Program>();
                watch.Stop();
                Console.WriteLine("程序初始化完毕并启动成功，耗时：{0}", watch.Elapsed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.FormatMessage());
                Console.ReadLine();
                return;
            }
            bool exit = false;
            while (true)
            {
                try
                {
                    Console.WriteLine(@"请输入命令：0; 退出程序，功能命令：1 - n");
                    string input = Console.ReadLine();
                    if (input == null)
                    {
                        continue;
                    }
                    switch (input.ToLower())
                    {
                        case "0":
                            exit = true;
                            break;
                        case "1":
                            Method01();
                            break;
                        case "2":
                            Method02();
                            break;
                        case "3":
                            Method03();
                            break;
                        case "4":
                            Method04();
                            break;
                        case "5":
                            Method05();
                            break;
                        case "6":
                            Method06();
                            break;
                        case "7":
                            Method07();
                            break;
                        case "8":
                            Method08();
                            break;
                        case "9":
                            Method09();
                            break;
                        case "10":
                            Method10();
                            break;
                        case "11":
                            Method11();
                            break;
                        case "12":
                            Method12();
                            break;
                        case "13":
                            Method13();
                            break;
                        case "14":
                            Method14();
                            break;
                        case "15":
                            Method15();
                            break;
                        case "16":
                            Method16();
                            break;
                        case "17":
                            Method17();
                            break;
                        case "18":
                            Method18();
                            break;
                    }
                    if (exit)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.FormatMessage());
                }
            }
        }

        private static void Method01()
        {
            Console.WriteLine("IoC注入解析测试：");
            Console.WriteLine("_program == null: {0}", _program == null);
            Console.WriteLine("_program.IocResolver: {0}", _program.IocResolver.GetType());
            Console.WriteLine("IUnitOfWork: {0}", _program.IocResolver.Resolve<IUnitOfWork>().GetType());
            Console.WriteLine("IRepository<Function, Guid>: {0}", _program.IocResolver.Resolve<IRepository<Function, Guid>>().GetType());
            Console.WriteLine(_program.IdentityContract.Roles.Count());
        }

        private static void Method02()
        {
            IServiceProvider provider = _program.IocResolver.Resolve<IServiceProvider>();
            Console.WriteLine(provider.GetType());
            provider.GetServices<IUnitOfWork>().ToList().ForEach(Console.WriteLine);
            provider.GetServices<IFinder<Assembly>>().ToList().ForEach(Console.WriteLine);
            Console.WriteLine(provider.GetService<IServiceCollection>());

        }

        private static void Method03()
        {
        }

        private static void Method04()
        {
            throw new NotImplementedException();
        }

        private static void Method05()
        {
            throw new NotImplementedException();
        }

        private static void Method06()
        {
            throw new NotImplementedException();
        }

        private static void Method07()
        {
            throw new NotImplementedException();
        }

        private static void Method08()
        {
            throw new NotImplementedException();
        }

        private static void Method09()
        {
            throw new NotImplementedException();
        }

        private static void Method10()
        {
            throw new NotImplementedException();
        }

        private static void Method11()
        {
            throw new NotImplementedException();
        }

        private static void Method12()
        {
            throw new NotImplementedException();
        }

        private static void Method13()
        {
            throw new NotImplementedException();
        }

        private static void Method14()
        {
            throw new NotImplementedException();
        }

        private static void Method15()
        {
            throw new NotImplementedException();
        }

        private static void Method16()
        {
            throw new NotImplementedException();
        }

        private static void Method17()
        {
            throw new NotImplementedException();
        }

        private static void Method18()
        {
            throw new NotImplementedException();
        }
    }
}
