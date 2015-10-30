// -----------------------------------------------------------------------
//  <copyright file="CodeFirstDbContext.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-07-17 17:34</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using OSharp.Core.Logging;
using OSharp.Utility.Exceptions;
using OSharp.Utility.Extensions;
using OSharp.Utility.Logging;


namespace OSharp.Core.Data.Entity
{
    /// <summary>
    /// EntityFramework-CodeFirst数据上下文
    /// </summary>
    public class CodeFirstDbContext : DbContext//, IUnitOfWork, IDependency
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(CodeFirstDbContext));

        /// <summary>
        /// 初始化一个<see cref="CodeFirstDbContext"/>类型的新实例
        /// </summary>
        public CodeFirstDbContext()
            : this(GetConnectionStringName())
        { }

        /// <summary>
        /// 使用连接名称或连接字符串 初始化一个<see cref="CodeFirstDbContext"/>类型的新实例
        /// </summary>
        public CodeFirstDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        /// <summary>
        /// 获取或设置 是否开启事务提交
        /// </summary>
        public bool TransactionEnabled { get; set; }

        /// <summary>
        /// 获取 数据库连接串名称
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionStringName()
        {
            string name = ConfigurationManager.AppSettings.Get("OSharp-ConnectionStringName") ?? "default";
            return name;
        }

        /// <summary>
        /// 获取 是否允许数据日志记录
        /// </summary>
        private static bool DataLoggingEnabled
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("OSharp-DataLoggingEnabled").CastTo(false);
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
        public int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
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
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
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
        public IEnumerable SqlQuery(Type elementType, string sql, params object[] parameters)
        {
            return Database.SqlQuery(elementType, sql, parameters);
        }

        /// <summary>
        /// 提交当前单元操作的更改。
        /// </summary>
        /// <returns>操作影响的行数</returns>
        public override int SaveChanges()
        {
            return SaveChanges(true);
        }

        /// <summary>
        /// 提交当前单元操作的更改。
        /// </summary>
        /// <param name="validateOnSaveEnabled">提交保存时是否验证实体约束有效性。</param>
        /// <returns>操作影响的行数</returns>
        internal int SaveChanges(bool validateOnSaveEnabled)
        {
            bool isReturn = Configuration.ValidateOnSaveEnabled != validateOnSaveEnabled;
            try
            {
                Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                //记录实体操作日志
                List<DataLog>logs = new List<DataLog>();
                if (DataLoggingEnabled)
                {
                    logs = this.GetEntityOperateLogs().ToList();
                }
                int count = base.SaveChanges();
                if (count > 0 && DataLoggingEnabled)
                {
                    Logger.Info(logs);
                }
                TransactionEnabled = false;
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
#if NET45

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
        public async Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
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
        internal async Task<int> SaveChangesAsync(bool validateOnSaveEnabled)
        {
            bool isReturn = Configuration.ValidateOnSaveEnabled != validateOnSaveEnabled;
            try
            {
                Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                //记录实体操作日志
                List<DataLog> logs = new List<DataLog>();
                if (DataLoggingEnabled)
                {
                    logs = (await this.GetEntityOperateLogsAsync()).ToList();
                }
                int count = await base.SaveChangesAsync();
                if (count > 0 && DataLoggingEnabled)
                {
                    Logger.Info(logs);
                }
                TransactionEnabled = false;
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
#endif
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除一对多的级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //注册实体配置信息
            ICollection<IEntityMapper> entityMappers = DatabaseInitializer.EntityMappers;
            foreach (IEntityMapper mapper in entityMappers)
            {
                mapper.RegistTo(modelBuilder.Configurations);
            }
        }
    }
}