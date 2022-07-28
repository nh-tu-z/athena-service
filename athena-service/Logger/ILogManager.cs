namespace AthenaService.Logger
{
    public interface ILogManager
    {
        void Error(Exception ex, string message, string prefix = "");
        void Error(string message, string prefix = "");
        void Error(Exception ex, string template, params object[] propertyValues);
        void Information(string message, string prefix = "");
        void Information(string template, params object[] propertyValues);
        void Warning(string template, params object[] propertyValues);
    }
}
