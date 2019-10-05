using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Common.Extensions
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
