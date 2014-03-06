using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using Npgsql;
using Ninject;

namespace Demetriux.RepositoryBase
{
    public interface IDataBaseConnectionFactory : IDbConnectionFactory
    {
        DbConnection CreateConnection(string nameOrConnectionString);
    }

    public partial class SqlConnectionFactory : IDataBaseConnectionFactory
    {
        private string connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return new SqlConnection(nameOrConnectionString);
        }
    }

    public partial class MySqlConnectionFactory : IDataBaseConnectionFactory//, IDbConnectionFactory
    {
        private string connectionString;

        public MySqlConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return new MySqlConnection(nameOrConnectionString);
        }
    }

    public partial class OracleConnectionFactory : IDataBaseConnectionFactory//, IDbConnectionFactory
    {
        private string connectionString;

        public OracleConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return new OracleConnection(nameOrConnectionString);
        }
    }

    public partial class PostGreConnectionFactory : IDataBaseConnectionFactory//, IDbConnectionFactory
    {
        private string connectionString;

        public PostGreConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return new NpgsqlConnection(nameOrConnectionString);
        }
    }

    public enum DataBase
    {
        SQL_SERVER = 1,
        MySQL_SERVER = 2,
        POSTGRESQL = 3
    }

    public class NinjectModules : Ninject.StandardKernel
    {
        

        public NinjectModules()
        {
            Bind<IDbConnectionFactory>().To<SqlConnectionFactory>();
            Bind<IDbConnectionFactory>().To<MySqlConnectionFactory>();
            Bind<IDbConnectionFactory>().To<PostGreConnectionFactory>();

        }

        public NinjectModules(DataBase dataBase)
        {
            switch (dataBase)
            {
                default:
                case DataBase.SQL_SERVER:
                    Bind<IDbConnectionFactory>().To<SqlConnectionFactory>();
                    break;

                case DataBase.MySQL_SERVER:
                    Bind<IDbConnectionFactory>().To<MySqlConnectionFactory>();
                    break;

                case DataBase.POSTGRESQL:
                    Bind<IDbConnectionFactory>().To<PostGreConnectionFactory>();
                    break;

            }
        }
    }
}
