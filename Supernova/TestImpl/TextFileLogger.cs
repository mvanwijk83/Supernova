using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Supernova.Core;
using Supernova.Core.Logging;

namespace Supernova.TestImpl
{
    public class TextFileLogger : ILogger
    {
        private string _timestampFormat = "yyyy-MM-dd HH:mm:ss";

        public string TimestampFormat
        {
            get
            {
                return _timestampFormat;
            }
            set
            {
                _timestampFormat = value;
            }
        }

        public void Write(EventType eventType, string message)
        {
            string path = @"C:\dev\Supernova\Supernova\log.txt";
            string logMessage = $"[{DateTime.Now.ToString(this.TimestampFormat)}] {message}\r\n";
            File.AppendAllText(path, logMessage);
        }
    }
}