using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Config;

namespace Logger
{
    public class Log
    {
	    private static NLog.Logger _log;
	    public static NLog.Logger Instance
	    {
		    get
		    {
			    if (_log != null) return _log;

			    _log = LogManager.GetCurrentClassLogger();
			    _log.Factory.Configuration = new XmlLoggingConfiguration(ConfigFilename);
			    return _log;
		    }
	    }
    }
}
