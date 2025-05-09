using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univerisity.Application.Loges
{
	public static class LogExeptions
	{
		public static void LogEx(Exception ex)
		{
			LogFile(ex.Message);
			LogConsole(ex.Message);
			LogDebug(ex.Message);
		}

		private static void LogConsole(string message)  =>  Log.Warning(message);
		private static void LogFile(string message) => Log.Information(message);
		private static void LogDebug(string message) => Log.Debug(message);

	}
}
