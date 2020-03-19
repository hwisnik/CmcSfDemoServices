using System;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace DataAccess.Infrastructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["SalesforceServicesCon"].ConnectionString;
        public IDbConnection GetConnection
        {
            get
            {
                var factory = SqlClientFactory.Instance;
                //var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var conn = factory.CreateConnection();
                if (conn == null) throw new Exception("Failed to create database connection");
                conn.ConnectionString = _connectionString;
                conn.Open();
                return conn;
            }
        }

        public SqlConnection GetSqlConnection
        {
            get
            {
                var sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();
                return sqlConnection;
            }

        }
        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
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
