// -----------------------------------------------------------------------
//  <copyright file="DbContextBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-06-28 2:20</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using OSharp.Core.Configs;
using OSharp.Core.Data;
using OSharp.Data.Entity.Properties;
using OSharp.Core.Logging;
using OSharp.Utility.Exceptions;
using OSharp.Utility.Extensions;
using OSharp.Utility.Logging;

using TransactionalBehavior = OSharp.Core.Data.TransactionalBehavior;


namespace OSharp.Data.Entity
{
    /// <summary>
    /// 数据库上下文基类
    /// </summary>
    public abstract class DbContextBase<TDbContext> : DbContext, IUnitOfWork
        where TDbContext : DbContext, IUnitOfWork, new()
    {
        protected readonly ILogger Logger = LogManager.GetLogger(typeof(TDbContext));
        private static DbContextConfig _contextConfig;

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="DbContextBase{TDbContext}"/>类型的新实例
        /// </summary>
        protected DbContextBase()
            : base(GetConnectionStringName())
        { }

        /// <summary>
        /// 初始化一个<see cref="DbContextBase{TDbContext}"/>类型的新实例
        /// </summary>
        protected DbContextBase(DbCompiledModel model)
            : base(model)
        { }

        /// <summary>
        /// 初始化一个<see cref="DbContextBase{TDbContext}"/>类型的新实例
        /// </summary>
        protected DbContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        /// <summary>
        /// 初始化一个<see cref="DbContextBase{TDbContext}"/>类型的新实例
        /// </summary>
        protected DbContextBase(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        { }

        /// <summary>
        /// 初始化一个<see cref="DbContextBase{TDbContext}"/>类型的新实例
        /// </summary>
        protected DbContextBase(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        { }

        /// <summary>
        /// 初始化一个<see cref="DbContextBase{TDbContext}"/>类型的新实例
        /// </summary>
        protected DbContextBase(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        { }

        /// <summary>
        /// 初始化一个<see cref="DbContextBase{TDbContext}"/>类型的新实例
        /// </summary>
        protected DbContextBase(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        { }

        #endregion

        /// <summary>
        /// 获取数据库连接字符串的名称
        /// </summary>
        /// <returns>数据库连接字符串对应的名称</returns>
        private static string GetConnectionStringName()
        {
            DbContextConfig contextConfig = GetDbContextConfig();
            if (contextConfig == null || !contextConfig.Enabled)
            {
                return typeof(TDbContext).ToString();
            }
            string name = contextConfig.ConnectionStringName;
            if (ConfigurationManager.ConnectionStrings[name] == null)
            {
                throw new InvalidOperationException(Resources.DbContextBase_ConnectionStringNameNotExist.FormatWith(name));
            }
            return name;
        }

        /// <summary>
        /// 获取OSharp框架数据上下文配置
        /// </summary>
        /// <returns></returns>
        private static DbContextConfig GetDbContextConfig()
        {
            return _contextConfig ?? (_contextConfig = OSharpConfig.Instance.DataConfig.ContextConfigs
                .FirstOrDefault(m => m.ContextType == typeof(TDbContext)));
        }

        /// <summary>
        /// 获取或设置 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 获取或设置 数据日志缓存
        /// </summary>
        public IDataLogCache DataLogCache { get; set; }

        /// <summary>
        /// 获取 是否允许数据库日志记录
        /// </summary>
        protected virtual bool DataLoggingEnabled
        {
            get
            {
                DbContextConfig contextConfig = GetDbContextConfig();
                if (contextConfig == null || !contextConfig.Enabled)
                {
                    return false;
                }
                return contextConfig.DataLoggingEnabled;
            }
        }

        /// <summary>
        /// 在完成对派生上下文的模型的初始化后，并在该模型已锁定并用于初始化上下文之前，将调用此方法。虽然此方法的默认实现不执行任何操作，但可在派生类中重写此方法，这样便能在锁定模型之前对其进行进一步的配置。
        /// </summary>
        /// <param name="modelBuilder">定义要创建的上下文的模型的生成器。</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除一对多的级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //注册实体配置信息
            IEntityMapper[] entityMappers = DbContextManager.Instance.GetEntityMappers(typeof(TDbContext)).ToArray();
            foreach (IEntityMapper mapper in entityMappers)
            {
                mapper.RegistTo(modelBuilder.Configurations);
            }
        }

        #region Implementation of IUnitOfWork

        /// <summary>
        /// 获取 是否开启事务提交
        /// </summary>
        public bool TransactionEnabled
        {
            get { return Database.CurrentTransaction != null; }
        }

        /// <summary>
        /// 显式开启数据上下文事务
        /// </summary>
        /// <param name="isolationLevel">指定连接的事务锁定行为</param>
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (Database.CurrentTransaction == null)
            {
                Database.BeginTransaction(isolationLevel);
            }
        }

        /// <summary>
        /// 提交事务的更改
        /// </summary>
        public void Commit()
        {
            DbContextTransaction transaction = Database.CurrentTransaction;
            if (transaction != null)
            {
                try
                {
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 显式回滚事务，仅在显式开启事务后有用
        /// </summary>
        public void Rollback()
        {
            if (Database.CurrentTransaction != null)
            {
                Database.CurrentTransaction.Rollback();
            }
        }

        /// <summary>
        /// 对数据库执行给定的 DDL/DML 命令。 
        /// 与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。 您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。 
        /// 您提供的任何参数值都将自动转换为 DbParameter。 unitOfWork.ExecuteSqlCommand("UPDATE dbo.Posts SET Rating = 5 WHERE Author = @p0", userSuppliedAuthor); 
        /// 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。 unitOfWork.ExecuteSqlCommand("UPDATE dbo.Posts SET Rating = 5 WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <param name="transactionalBehavior">对于此命令控制事务的创建。</param>
        /// <param name="sql">命令字符串。</param>
        /// <param name="parameters">要应用于命令字符串的参数。</param>
        /// <returns>执行命令后由数据库返回的结果。</returns>
        public virtual int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
        {
            System.Data.Entity.TransactionalBehavior behavior = transactionalBehavior == TransactionalBehavior.DoNotEnsureTransaction
                ? System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction
                : System.Data.Entity.TransactionalBehavior.EnsureTransaction;
            return Database.ExecuteSqlCommand(behavior, sql, parameters);
        }

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回给定泛型类型的元素。 类型可以是包含与从查询返回的列名匹配的属性的任何类型，也可以是简单的基元类型。 该类型不必是实体类型。
        ///  即使返回对象的类型是实体类型，上下文也决不会跟踪此查询的结果。 使用 SqlQuery(String, Object[]) 方法可返回上下文跟踪的实体。 
        /// 与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。 您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。 
        /// 您提供的任何参数值都将自动转换为 DbParameter。 unitOfWork.SqlQuery&lt;Post&gt;("SELECT * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor); 
        /// 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。 unitOfWork.SqlQuery&lt;Post&gt;("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <typeparam name="TElement">查询所返回对象的类型。</typeparam>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见 http://go.microsoft.com/fwlink/?LinkID=398589。</param>
        /// <returns></returns>
        public virtual IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<TElement>(sql, parameters);
        }

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回给定类型的元素。 类型可以是包含与从查询返回的列名匹配的属性的任何类型，也可以是简单的基元类型。 该类型不必是实体类型。 即使返回对象的类型是实体类型，上下文也决不会跟踪此查询的结果。 使用 SqlQuery(String, Object[]) 方法可返回上下文跟踪的实体。 与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。 您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。 您提供的任何参数值都将自动转换为 DbParameter。 context.Database.SqlQuery(typeof(Post), "SELECT * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor); 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。 context.Database.SqlQuery(typeof(Post), "SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <param name="elementType">查询所返回对象的类型。</param>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见 http://go.microsoft.com/fwlink/?LinkID=398589。</param>
        /// <returns></returns>
        public virtual IEnumerable SqlQuery(Type elementType, string sql, params object[] parameters)
        {
            return Database.SqlQuery(elementType, sql, parameters);
        }

        #endregion

        /// <summary>
        /// 提交当前单元操作的更改
        /// </summary>
        /// <returns>操作影响的行数</returns>
        public override int SaveChanges()
        {
            return SaveChanges(true);
        }

        /// <summary>
        /// 提交当前单元操作的更改
        /// </summary>
        /// <param name="validateOnSaveEnabled">提交保存时是否验证实体约束有效性。</param>
        /// <returns>操作影响的行数</returns>
        internal virtual int SaveChanges(bool validateOnSaveEnabled)
        {
            bool isReturn = Configuration.ValidateOnSaveEnabled != validateOnSaveEnabled;
            try
            {
                Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                //记录实体操作日志
                List<DataLog> logs = new List<DataLog>();
                if (DataLoggingEnabled)
                {
                    logs = this.GetEntityDataLogs(ServiceProvider).ToList();
                }
                int count;
                try
                {
                    count = base.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    IEnumerable<DbEntityValidationResult> errorResults = ex.EntityValidationErrors;
                    List<string> ls = (from result in errorResults
                                       let lines = result.ValidationErrors.Select(error => "{0}: {1}".FormatWith(error.PropertyName, error.ErrorMessage)).ToArray()
                                       select "{0}({1})".FormatWith(result.Entry.Entity.GetType().FullName, lines.ExpandAndToString(", "))).ToList();
                    string message = "数据验证引发异常——" + ls.ExpandAndToString(" | ");
                    throw new DataException(message, ex);
                }
                if (count > 0 && DataLoggingEnabled)
                {
                    foreach (DataLog log in logs)
                    {
                        DataLogCache.AddDataLog(log);
                    }
                    //Logger.Info(logs, true);
                }
                //TransactionEnabled = false;
                return count;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = e.InnerException.InnerException as SqlException;
                    string msg = DataHelper.GetSqlExceptionMessage(sqlEx.Number);
                    throw new OSharpException("提交数据更新时发生异常：" + msg, sqlEx);
                }
                throw;
            }
            finally
            {
                if (isReturn)
                {
                    Configuration.ValidateOnSaveEnabled = !validateOnSaveEnabled;
                }
            }
        }

        /// <summary>
        /// 对数据库执行给定的 DDL/DML 命令。 
        /// 与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。 您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。 
        /// 您提供的任何参数值都将自动转换为 DbParameter。 unitOfWork.ExecuteSqlCommand("UPDATE dbo.Posts SET Rating = 5 WHERE Author = @p0", userSuppliedAuthor); 
        /// 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。 unitOfWork.ExecuteSqlCommand("UPDATE dbo.Posts SET Rating = 5 WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <param name="transactionalBehavior">对于此命令控制事务的创建。</param>
        /// <param name="sql">命令字符串。</param>
        /// <param name="parameters">要应用于命令字符串的参数。</param>
        /// <returns>执行命令后由数据库返回的结果。</returns>
        public virtual async Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
        {
            System.Data.Entity.TransactionalBehavior behavior = transactionalBehavior == TransactionalBehavior.DoNotEnsureTransaction
                ? System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction
                : System.Data.Entity.TransactionalBehavior.EnsureTransaction;
            return await Database.ExecuteSqlCommandAsync(behavior, sql, parameters);
        }

        /// <summary>
        /// 异步提交当前单元操作的更改。
        /// </summary>
        /// <returns>操作影响的行数</returns>
        public override Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(true);
        }

        /// <summary>
        /// 提交当前单元操作的更改。
        /// </summary>
        /// <param name="validateOnSaveEnabled">提交保存时是否验证实体约束有效性。</param>
        /// <returns>操作影响的行数</returns>
        internal virtual async Task<int> SaveChangesAsync(bool validateOnSaveEnabled)
        {
            bool isReturn = Configuration.ValidateOnSaveEnabled != validateOnSaveEnabled;
            try
            {
                Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                //记录实体操作日志
                List<DataLog> logs = new List<DataLog>();
                if (DataLoggingEnabled)
                {
                    logs = (await this.GetEntityOperateLogsAsync(ServiceProvider)).ToList();
                }
                int count = 0;
                try
                {
                    count = await base.SaveChangesAsync();
                }
                catch (DbEntityValidationException ex)
                {
                    IEnumerable<DbEntityValidationResult> errorResults = ex.EntityValidationErrors;
                    List<string> ls = (from result in errorResults
                                       let lines = result.ValidationErrors.Select(error => "{0}: {1}".FormatWith(error.PropertyName, error.ErrorMessage)).ToArray()
                                       select "{0}({1})".FormatWith(result.Entry.Entity.GetType().FullName, lines.ExpandAndToString(", "))).ToList();
                    string message = "数据验证引发异常——" + ls.ExpandAndToString(" | ");
                    throw new DataException(message, ex);
                }
                if (count > 0 && DataLoggingEnabled)
                {
                    foreach (DataLog log in logs)
                    {
                        DataLogCache.AddDataLog(log);
                    }
                    //Logger.Info(logs, true);
                }
                //TransactionEnabled = false;
                return count;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = e.InnerException.InnerException as SqlException;
                    string msg = DataHelper.GetSqlExceptionMessage(sqlEx.Number);
                    throw new OSharpException("提交数据更新时发生异常：" + msg, sqlEx);
                }
                throw;
            }
            finally
            {
                if (isReturn)
                {
                    Configuration.ValidateOnSaveEnabled = !validateOnSaveEnabled;
                }
            }
        }
    }
}