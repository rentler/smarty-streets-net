using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentler.SmartyStreets
{
	/// <summary>
	/// Represents the results of a street address lookup. Includes
	/// the index of the address (a single address lookup can resolve to 
	/// several possible results), basic address information, and
	/// detailed additional data in the Components, Metadata, and Analysis
	/// objects. See http://smartystreets.com/kb/liveaddress-api/field-definitions
	/// for documentation on each field.
	/// </summary>
	public class SmartyStreetsAddress
	{
		public int input_index { get; set; }
		public int candidate_index { get; set; }
		public string delivery_line_1 { get; set; }
		public string last_line { get; set; }
		public string delivery_point_barcode { get; set; }
		public Components components { get; set; }
		public Metadata metadata { get; set; }
		public Analysis analysis { get; set; }
	}

	/// <summary>
	/// Represents the full breakdown of the resolved address.
	/// See http://smartystreets.com/kb/liveaddress-api/field-definitions#components
	/// for documentation on each field.
	/// </summary>
	public class Components
	{
		public string primary_number { get; set; }
		public string street_predirection { get; set; }
		public string street_name { get; set; }
		public string street_postdirection { get; set; }
		public string street_suffix { get; set; }
		public string secondary_number { get; set; }
		public string secondary_designator { get; set; }
		public string city_name { get; set; }

		public string default_city_name { get; set; }
		public string state_abbreviation { get; set; }
		public string zipcode { get; set; }
		public string plus4_code { get; set; }
		public string delivery_point { get; set; }
		public string delivery_point_check_digit { get; set; }
	}

	/// <summary>
	/// Represents extra data on the resolved address, including
	/// geolocation, precision, time zones, etc. See
	/// http://smartystreets.com/kb/liveaddress-api/field-definitions#metadata
	/// for documentation on each field.
	/// </summary>
	public class Metadata
	{
		public string record_type { get; set; }
		public string zip_type { get; set; }
		public string county_fips { get; set; }
		public string county_name { get; set; }
		public string carrier_route { get; set; }
		public string congressional_district { get; set; }
		public string building_default_indicator { get; set; }
		public string rdi { get; set; }
		public string elot_sequence { get; set; }
		public string elot_sort { get; set; }
		public double latitude { get; set; }
		public double longitude { get; set; }
		public string precision { get; set; }
		public string time_zone { get; set; }
		public int utc_offset { get; set; }
		public bool dst { get; set; }
	}

	/// <summary>
	/// Represents detailed information about the state of
	/// the resolved address. Whether it is deliverable,
	/// vacant, footnotes to indicate what SmartyStreets changed
	/// to match the exact address, and so on. See
	/// http://smartystreets.com/kb/liveaddress-api/field-definitions#analysis
	/// for documentation on each field.
	/// </summary>
	public class Analysis
	{
		public string dpv_match_code { get; set; }
		public string dpv_footnotes { get; set; }
		public string dpv_cmra { get; set; }
		public string dpv_vacant { get; set; }
		public string active { get; set; }
		public string footnotes { get; set; }
	}
}
