using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentler.SmartyStreets
{
	/// <summary>
	/// Represents the results of a City/State/Zip lookup.
	/// See http://smartystreets.com/kb/liveaddress-api/zipcode-api
	/// for documentation.
	/// </summary>
	public class SmartyStreetsCityStateZipLookup
	{
		public List<CityState> city_states { get; set; }
		public List<Zipcode> zipcodes { get; set; }
	}

	public class CityState
	{
		public string city { get; set; }
		public string state_abbreviation { get; set; }
		public string state { get; set; }
	}

	public class Zipcode
	{
		public string zipcode { get; set; }
		public string zipcode_type { get; set; }
		public string county_fips { get; set; }
		public string county_name { get; set; }
		public double latitude { get; set; }
		public double longitude { get; set; }
	}
}
