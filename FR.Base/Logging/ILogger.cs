using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FR.Logging {
    public interface ILogger {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Ex(Exception ex);
        void Debug(string message);
        void CallEntry(LogLevel logLevel);
        void CallLeave(LogLevel logLevel);
    }
}
