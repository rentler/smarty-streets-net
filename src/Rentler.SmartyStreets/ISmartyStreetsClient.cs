using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentler.SmartyStreets
{
	public interface ISmartyStreetsClient
	{
		Task<IEnumerable<SmartyStreetsAddress>> GetStreetAddressAsync(
			string street = null, string secondary = null,
			string street2 = null, string city = null,
			string state = null, string zipcode = null);

		Task<IEnumerable<SmartyStreetsCityStateZipLookup>> GetLookupAsync(
			string city = null, string state = null,
			string zip = null);
	}
}
