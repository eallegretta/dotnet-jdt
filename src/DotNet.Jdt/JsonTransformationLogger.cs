using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Jdt;
using System;

namespace DotNet.Jdt
{
    internal class JsonTransformationLogger : IJsonTransformationLogger
    {
        private readonly ILogger _logger;

        public JsonTransformationLogger(ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger("DotNet.Jdt");
        }

        public void LogError(string message) => _logger.LogError(message);
        public void LogError(string message, string fileName, int lineNumber, int linePosition) => _logger.LogError("{message}. Filename: {filename}, Line: {lineNumber}, Pos: {linePosition}", message, fileName, lineNumber, linePosition);
        public void LogErrorFromException(Exception ex) => _logger.LogError(ex, ex.Message);
        public void LogErrorFromException(Exception ex, string fileName, int lineNumber, int linePosition) => _logger.LogError(ex, "{message}. Filename: {filename}, Line: {lineNumber}, Pos: {linePosition}", ex.Message, fileName, lineNumber, linePosition);
        public void LogMessage(string message) => _logger.LogInformation(message);
        public void LogMessage(string message, string fileName, int lineNumber, int linePosition) => _logger.LogInformation("{message}. Filename: {filename}, Line: {lineNumber}, Pos: {linePosition}", message, fileName, lineNumber, linePosition);
        public void LogWarning(string message) => _logger.LogWarning(message);
        public void LogWarning(string message, string fileName) => _logger.LogWarning("{message}. Filename: {filename}", message, fileName);
        public void LogWarning(string message, string fileName, int lineNumber, int linePosition) => _logger.LogWarning("{message}. Filename: {filename}, Line: {lineNumber}, Pos: {linePosition}", message, fileName, lineNumber, linePosition);
    }
}
