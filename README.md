# osharp framework version 3.x

 - [简介][1]
 - [相关技术][2]
 - [快速启动][3]
 - [开发计划与进度][4]
 - [版本更新日志][5]

## <a id="title01"/>简介
1. OSharp是个快速开发框架，但不是一个大而全的包罗万象的框架，严格的说，OSharp中什么都没有实现。与其他大而全的框架最大的不同点，就是OSharp只做抽象封装，不做实现。依赖注入、ORM、对象映射、日志、缓存等等功能，都只定义了一套最基础最通用的抽象封装，提供了一套统一的API、约定与规则，并定义了部分执行流程，主要是让项目在一定的规范下进行开发。所有的功能实现端，都是通过现有的成熟的第三方组件来实现的，除了EntityFramework之外，所有的第三方实现都可以轻松的替换成另一种第三方实现，OSharp框架正是要起隔离作用，保证这种变更不会对业务代码造成影响，使用统一的API来进行业务实现，解除与第三方实现的耦合，保持业务代码的规范与稳定。
2. 相关博客：[OSharp框架解说系列文章](http://www.cnblogs.com/guomingfeng/p/osharp-overall-design.html)
3. QQ交流群：85895249 [![OSharp开发框架交流群](http://pub.idqqimg.com/wpa/images/group.png)](http://shang.qq.com/wpa/qunwpa?idkey=250897a8ee4c2d3146d798a6111274bfa7bb6306d0f07418bfc6d8c45f26e269 "OSharp开发框架交流群")

## <a id="title02"/>相关技术
1. 技术框架：.NET Framework 4.5
2. 技术平台：ASP.NET MVC5 + WebAPI5 + SignalR2
3. 数据存储：EntityFramework 6.1.3
4. 数据序列化：使用JSON.NET作为JSON序列化的主要工具
5. 数据映射：定义通用对象映射操作API，并提供基于AutoMapper的实现，主要用于数据传输对象DTO与数据实体模型Model之间的相互转化，免于繁杂的对象属性赋值
6. IoC组件：参考ASP.NET 5，从框架级别对依赖注入功能进行了抽象与封装，并提供基于Autofac的依赖注入实现
7. 日志记录：定义通用日志记录接口与基础API，日志输出方式可以使用现成的任意日志组件（如log4net）

## <a id="title03"/>快速启动
#### 从nuget中引用需要的osharp组件
#### 在Web.Config中添名为name="default"的数据库连接
```
<connectionStrings>
    <add name="default" connectionString="Data Source=.; Integrated Security=True; Initial Catalog=OSharp.Default; Pooling=True; MultipleActiveResultSets=True;" providerName="System.Data.SqlClient" />
</connectionStrings>
```

#### 在Global的Application_Start方法中添加初始化代码
```
IServicesBuilder builder = new ServicesBuilder();
IServiceCollection services = builder.Build();
services.AddDataServices();
IFrameworkInitializer initializer = new FrameworkInitializer();
initializer.Initialize(new MvcAutofacIocBuilder(services));
```

#### 添加EntityInfo与Function实体类的实体映射配置
```
public class EntityInfoConfiguration : EntityConfigurationBase<EntityInfo, Guid>
{ }
public class FunctionConfiguration : EntityConfigurationBase<Function, Guid>
{ }
```

#### 运行项目，即可初始化完成，将会自动生成相应的数据库
    
    


  [1]: #title01
  [2]: #title02
  [3]: #title03
  [4]: https://github.com/i66soft/osharp/wiki/plan-rate
  [5]: https://github.com/i66soft/osharp/wiki/update-logs
