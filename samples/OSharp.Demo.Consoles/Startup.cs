using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using OSharp.Autofac;
using OSharp.Core;
using OSharp.Core.Caching;
using OSharp.Core.Data;
using OSharp.Core.Data.Entity;
using OSharp.Core.Dependency;
using OSharp.Demo.Dtos;
using OSharp.SiteBase.Initialize;
using OSharp.Utility.Logging;


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
            DtoMappers.MapperRegister();
        }

        private static void AutofacRegisters()
        {
            FrameworkConsoleInitializer initializer = new FrameworkConsoleInitializer();
            var autofacConsoleIocInitializer = new AutofacConsoleIocInitializer();
            initializer.ConsoleIocInitializer = autofacConsoleIocInitializer;
            Container = autofacConsoleIocInitializer.GetComtainer();
            initializer.Initialize();
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
