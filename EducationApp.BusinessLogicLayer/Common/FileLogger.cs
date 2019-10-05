using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace EducationApp.BusinessLogicLayer.Common
{
    public class FileLogger:ILogger    
    {
        private readonly string _filePath;
        private readonly object _lock;
        public FileLogger(string filePath)
        {
            _filePath = filePath;
            _lock = new object();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock(_lock) {
                    File.AppendAllText(_filePath, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}
