using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using OSharp.Data.Entity;


namespace OSharp.Demo.Web.Data
{
    public class MyDbContextInitializer : DbContextInitializerBase<DefaultDbContext>
    {
        public MyDbContextInitializer()
        {
            MigrateInitializer = new MigrateDatabaseToLatestVersion<DefaultDbContext, MyAutoMigrationsConfiguration<DefaultDbContext>>();
        }
    }
}