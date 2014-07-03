using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvsConsole
{
	public static class App
	{
		private static string smartyStreetsAuthToken;

		public static string SmartyStreetsAuthToken
		{
			get
			{
				if (smartyStreetsAuthToken == null)
					smartyStreetsAuthToken = ConfigurationManager.AppSettings["SmaryStreetsAuthToken"];
				return smartyStreetsAuthToken;
			}
		}

		private static string smartyStreetsAuthId;

		public static string SmartyStreetsAuthId
		{
			get
			{
				if (smartyStreetsAuthId == null)
					smartyStreetsAuthId = ConfigurationManager.AppSettings["SmaryStreetsAuthId"];
				return smartyStreetsAuthId;
			}
		}
	}
}
