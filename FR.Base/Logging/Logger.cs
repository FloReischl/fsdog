using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FR.Logging {
    public class Logger : ILogger {
        private LoggingManager _manager;

        public Logger(LoggingManager manager) {
            _manager = manager;
        }

        public void Debug(string message) {
            _manager.Write(LogLevel.Debug, 0, message);
        }

        public void Error(string message) {
            _manager.Write(LogLevel.Error, 0, message);
        }

        public void Exception(Exception ex) {
            _manager.WriteEx(ex, 0);
        }

        public void Info(string message) {
            _manager.Write(LogLevel.Info, 0, message);
        }

        public void Warn(string message) {
            _manager.Write(LogLevel.Warning, 0, message);
        }
    }
}
