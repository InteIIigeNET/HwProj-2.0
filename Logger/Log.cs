using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Logger
{
    public class Log
    {
	    public static readonly NLog.Logger Instance = LogManager.GetCurrentClassLogger();
	}
}
