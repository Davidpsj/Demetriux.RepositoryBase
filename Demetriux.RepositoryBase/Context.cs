using Demetriux.RepositoryBase.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demetriux.RepositoryBase
{
    public partial class Context : DbContext
    {
        public String ConnectionString
        {
            get
            {
                return Database.Connection.ConnectionString;
            }
            set
            {
                Database.Connection.ConnectionString = value;
            }
        }

        public Context() 
        {
            Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        }
    }
}
