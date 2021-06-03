using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YukiToolkit.Tools {
	public class Logger {
		public string? LogFile { get; set; }
		public void Log(string format, params object[] args) {
			var stackFrame = new System.Diagnostics.StackTrace(true).GetFrame(1);
			var fileName = stackFrame.GetFileName();
			fileName = Path.GetFileName(fileName);
			var lineNumber = stackFrame.GetFileLineNumber();
			string line;
			if (args.Length == 0) {
				line = $"[{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss.fff zz}] [{fileName}:{lineNumber}] " + format;
			}
			else {
				line = string.Format($"[{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss.fff zz}] [{fileName}:{lineNumber}] " + format, args);
			}
			Console.WriteLine(line);
			if (LogFile != null) {
				File.AppendAllText(LogFile, line + "\n");
			}
		}

		public Logger(string? logFile = null) {
			LogFile = logFile;
		}
	}
}
