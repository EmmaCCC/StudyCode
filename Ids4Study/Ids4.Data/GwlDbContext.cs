﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ids4.Data
{
    public class GwlDbContext : DbContext
    {
        public DbSet<AppClient> AppClients { get; set; }

        public GwlDbContext()
        {
            this.Database.EnsureCreated();
        }
        // 配置连接字符串
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=GwlDb;Trusted_Connection=True;");
        }
    }
}
