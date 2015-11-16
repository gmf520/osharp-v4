﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Data.Entity;
using OSharp.Data.Entity.Migrations;


namespace OSharp.Demo.Data
{
    public class CreateDatabaseIfNotExistsWithSeed : CreateDatabaseIfNotExistsWithSeedBase<DefaultDbContext>
    {
        public CreateDatabaseIfNotExistsWithSeed()
        {
            SeedActions.Add(new CreateDatabaseSeedAction());
        }
    }
}
