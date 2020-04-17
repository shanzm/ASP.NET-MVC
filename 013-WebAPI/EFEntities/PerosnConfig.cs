using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFEntities
{
    public class PerosnConfig : EntityTypeConfiguration<Person>
    {
        public PerosnConfig()
        {
            this.ToTable("T_Persons");
            this.Property(p => p.Name).HasMaxLength(50).IsRequired();
            this.HasKey(p => p.Id);//设置主键
        }
    }
}