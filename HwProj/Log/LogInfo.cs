using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace HwProj.Log
{
	public class LogInfo
	{
		public string Action { get; set; }
		public string Controller { get; set; }
		public string UserName { get; set; }
		public string Ip { get; set; }
		public List<KeyValuePair<string, string>> RouteData { get; set; } 
		 = new List<KeyValuePair<string, string>>();

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}