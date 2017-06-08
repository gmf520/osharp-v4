namespace OSharp.Web.Http.Logging
{
    public interface ILoggingRepository
    {
        void Log(ApiLoggingInfo loggingInfo);
    }
}
