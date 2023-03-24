namespace MathApi.Services
{
    public interface IHeadersSetter
    {
        public void SetHeaders(HttpResponse response);
    }
    
    public class HeadersSetter : IHeadersSetter
    {
        public void SetHeaders(HttpResponse response)
        {
            response.Headers.AccessControlAllowOrigin = "*";
            response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
        }
    }
}

