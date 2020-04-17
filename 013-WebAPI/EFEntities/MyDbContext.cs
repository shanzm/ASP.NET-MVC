using EFEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EFEntities
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=connStr")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //法1--无需单独的配置类PersonConfig.cs
            //modelBuilder.Entity<Person>().ToTable("T_Persons");
            //modelBuilder.Entity<Person>().HasKey(p => p.Id);

            //法2--需要单独的配置类PersonConfig.cs,仅配置Person 
            //modelBuilder.Configurations.Add(new PerosnConfig());

            //法3--将所有的在运行的EF实体类配置
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Person> persons { get; set; }
    }
}