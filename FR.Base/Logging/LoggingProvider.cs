// Decompiled with JetBrains decompiler
// Type: FR.Logging.LoggingProvider
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace FR.Logging {
    public static class LoggingProvider {
        private static readonly LoggingManager _manager = new LoggingManager() {
            Devices = { new LoggingDeviceConsole() }
        };

        public static LoggingManager Manager { get; set; } = _manager;

        public static ILogger CreateLogger() => new Logger(_manager);

        public static void Flush() {
            var devices = _manager?.Devices ?? new List<ILoggingDevice>();

            foreach (var device in devices) {
                device.Flush();
            }
        }
    }
}
