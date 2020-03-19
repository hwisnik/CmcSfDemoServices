using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using System;


namespace BusinessLogic.Services
{

    public class GenericServiceResponse : IServiceResponse<object>, IDisposable
    {
        public Object Entity { get; set; }
        public bool Success { get; set; }
        public RestStatus RestResponseStatus { get; set; }
        public Exception OperationException { get; set; }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        public enum RestStatus
        {
            Success,
            Empty,
            Error,
        }


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
        // ~GenericServiceResponse() {
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

        //public static implicit operator HttpContent(GenericServiceResponse v)
        //{
        //    var resp = HttpContent()

        //    //throw new NotImplementedException();
        //}
        #endregion
    }

}
