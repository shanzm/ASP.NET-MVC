using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            this.ToTable("T_Users");
            this.Property(u => u.Id).IsRequired();
            //this.Property(u => u.Password).IsRequired();
        }
    }
}