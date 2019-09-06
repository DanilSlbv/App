
using Microsoft.Extensions.Logging;

namespace EducationApp.BusinessLogicLayer.Common
{
    class FileLoggerProvider : ILoggerProvider
    {
        private string _filePath;

        public FileLoggerProvider(string filePath)
        {
            _filePath = filePath;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_filePath);
        }
         
        public void Dispose()
        {
            
        }
    }
}
