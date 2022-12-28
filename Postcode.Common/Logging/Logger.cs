using System;
using Microsoft.Extensions.Logging;

namespace Postcode.Common.Logging
{
    public interface ILogger
    {
        void Log(ILogModelCreator logCreator);
    }

    public class Logger : ILogger
    {
        private readonly ILogger<Logger> _logger;

        public Logger(ILogger<Logger> logger)
        {
            _logger = logger;
        }
        public void Log(ILogModelCreator logCreator)
        {
            // Different methods to log.
            //_logger.LogTrace(jsonString);
            //_logger.LogInformation(jsonString);
            //_logger.LogWarning(jsonString);
            _logger.LogCritical(logCreator.LogString());
        }
    }
}

