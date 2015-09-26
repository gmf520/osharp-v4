using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using OSharp.App.Local.Initialize;
using OSharp.Core.Dependency;


namespace OSharp.Demo.Consoles
{
    public class ConsolesAutofacInitializer : LocalAutofacIocInitializer
    {
        /// <summary>
        /// 注册自定义类型
        /// </summary>
        protected override void RegisterCustomTypes()
        {
            base.RegisterCustomTypes();
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<Program>().PropertiesAutowired().SingleInstance();
            builder.Update(Container);
        }
    }
}
