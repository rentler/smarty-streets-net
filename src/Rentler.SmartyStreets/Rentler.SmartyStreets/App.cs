using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentler.SmartyStreets
{
	/// <summary>
	/// Utility class to strongly type and simplify retrieval of 
	/// configuration settings from an App.config or Web.config.
	/// Used by the SmartyStreetsClient if settings are not passed
	/// in its constructor.
	/// </summary>
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
