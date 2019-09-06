using Microsoft.Extensions.Logging;
namespace EducationApp.BusinessLogicLayer.Common
{
    public static class FileLoggerExtension
    {
        public static ILoggerFactory AddFile(this ILoggerFactory loggerFactory, string FilePath)
        {
            loggerFactory.AddProvider(new FileLoggerProvider(FilePath));
            return loggerFactory;
        }
    }
}
