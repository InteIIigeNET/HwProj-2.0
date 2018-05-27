using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using NLog.Config;

namespace HwProj.Log
{
	public class Log
	{
		public static Logger Instance = LogManager.GetCurrentClassLogger();
	}
}