# osharp framework version 3.0
## 简介
OSharp是一个依赖于EntityFramework，专注于业务数据模型与EntityFramework解耦的适用于中小型项目开发的（插件式）框架。
相关博客：http://www.cnblogs.com/guomingfeng/tag/OSharp%E6%A1%86%E6%9E%B6/

## 相关技术
1. 技术框架：.NET Framework 4.5
2. 技术平台：ASP.NET MVC5 + WebAPI + SignalR
3. 数据存储：EntityFramework 6
4. 数据序列化：使用JSON.NET作为JSON序列化的主要工具
5. 数据映射：AutoMapper，主要用于数据传输对象DTO与数据实体模型Model之间的相互转化，免于繁杂的对象属性赋值
6. IoC组件：Autofac，定义了一个专用于处理映射的空接口IDependency，用于处理IoC接口与实现的批量映射，避免Autofac与各个层次耦合
7. 日志记录：定义通用日志记录接口与基础API，日志输出方式可以使用现成的任意日志组件（如log4net）
