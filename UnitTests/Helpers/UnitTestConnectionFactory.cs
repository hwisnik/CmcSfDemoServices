using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Tests.Helpers
{

    public interface IUnitTestConnectionFactory : IDisposable
    {
        IDbConnection GetConnection { get; }
        SqlConnection GetSqlConnection { get; }
    }

    [ExcludeFromCodeCoverage]
    public class UnitTestConnectionFactory : IUnitTestConnectionFactory
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ErmsServicesCon"].ConnectionString;
        public IDbConnection GetConnection
        {
            get
            {
                SqlClientFactory factory = SqlClientFactory.Instance;
                //var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var conn = factory.CreateConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn;
            }
        }

        public SqlConnection GetSqlConnection
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                return sqlConnection;
            }


        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ConnectionFactory() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}


