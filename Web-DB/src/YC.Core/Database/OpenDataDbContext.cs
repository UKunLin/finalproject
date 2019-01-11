using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using YC.Models;

namespace YC.Database
{
    public class OpenDataDbContext:DbContext
    {
        public string ConnectionString
        {
            get
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), @"App_Data\Database.mdf");

                return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={path};Integrated Security=True";
                
            }

        }

        public DbSet<OpenData> OpenData { get; set; }



        public OpenDataDbContext() : base()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }


    }
}
