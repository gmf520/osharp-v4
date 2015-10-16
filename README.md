# osharp framework version 3.x

 - [简介][1]
 - [相关技术][2]
 - [快速启动][3]

## <a id="title01"/>简介
1. OSharp是一个依赖于EntityFramework，专注于业务数据模型与EntityFramework解耦的适用于中小型项目开发的（插件式）框架。
2. 相关博客：[OSharp框架解说系列文章](http://www.cnblogs.com/guomingfeng/p/osharp-overall-design.html)
3. QQ交流群：85895249 [![OSharp开发框架交流群](http://pub.idqqimg.com/wpa/images/group.png)](http://shang.qq.com/wpa/qunwpa?idkey=250897a8ee4c2d3146d798a6111274bfa7bb6306d0f07418bfc6d8c45f26e269 "OSharp开发框架交流群")

## <a id="title02"/>相关技术
1. 技术框架：.NET Framework 4.5
2. 技术平台：ASP.NET MVC5 + WebAPI5 + SignalR2
3. 数据存储：EntityFramework 6.1.3
4. 数据序列化：使用JSON.NET作为JSON序列化的主要工具
5. 数据映射：AutoMapper，主要用于数据传输对象DTO与数据实体模型Model之间的相互转化，免于繁杂的对象属性赋值
6. IoC组件：参考ASP.NET 5，从框架级别对依赖注入功能进行了抽象与封装，并提供基于Autofac的依赖注入实现
7. 日志记录：定义通用日志记录接口与基础API，日志输出方式可以使用现成的任意日志组件（如log4net）

## <a id="title03"/>快速启动
#### 从nuget中引用需要的osharp组件
#### 在Web.Config中添名为name="default"的数据库连接
    <connectionStrings>
        <add name="default" connectionString="Data Source=.; Integrated Security=True; Initial Catalog=OSharp.Default; Pooling=True; MultipleActiveResultSets=True;" providerName="System.Data.SqlClient" />
    </connectionStrings>
#### 在Global的Application_Start方法中添加初始化代码
	IServicesBuilder builder = new ServicesBuilder();
	IServiceCollection services = builder.Build();
	services.AddDataServices();
	IFrameworkInitializer initializer = new FrameworkInitializer();
	initializer.Initialize(new MvcAutofacIocBuilder(services));
#### 添加EntityInfo与Function实体类的实体映射配置
    public class EntityInfoConfiguration : EntityConfigurationBase<EntityInfo, Guid>
    { }
    public class FunctionConfiguration : EntityConfigurationBase<Function, Guid>
    { }
#### 运行项目，即可初始化完成，将会自动生成相应的数据库
    
    


  [1]: #title01
  [2]: #title02
  [3]: #title03
