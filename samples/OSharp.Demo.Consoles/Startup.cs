using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using OSharp.Core;
using OSharp.Core.Caching;
using OSharp.Core.Data;
using OSharp.Core.Data.Entity;
using OSharp.Core.Dependency;


namespace OSharp.Demo.Consoles
{
    public static class Startup
    {
        public static IContainer Container { get; private set; }

        public static void Start()
        {
            AutofacRegisters();
            CachingInit();
            LoggingInit();
            DatabaseInit();
        }

        private static void AutofacRegisters()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>));

            Type baseType = typeof(ILifetimeScopeDependency);
            string path = Directory.GetCurrentDirectory();
            Assembly[] assemblies = Directory.GetFiles(path, "*.dll").Select(m => Assembly.LoadFrom(m))
                .Union(new[] { Assembly.GetExecutingAssembly() }).ToArray();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                .AsSelf()   //自身服务，用于没有接口的类
                .AsImplementedInterfaces()  //接口服务
                .PropertiesAutowired()  //属性注入
                .InstancePerLifetimeScope();    //保证生命周期基于请求

            Container = builder.Build();
        }

        private static void CachingInit()
        {
            
        }

        private static void LoggingInit()
        {
            //LogManager.AddLoggerAdapter(new Log4NetLoggerAdapter());
        }

        private static void DatabaseInit()
        {
            //const string file = "OSharp.Demo.Services.dll";
            //Assembly assembly = Assembly.LoadFrom(file);
            //DatabaseInitializer.AddMapperAssembly(assembly);
            //DatabaseInitializer.Initialize();
        }
    }
}
