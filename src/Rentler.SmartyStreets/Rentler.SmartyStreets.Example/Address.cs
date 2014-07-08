using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rentler.SmartyStreets.Example
{
	public class Address
	{
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }

		/// <summary>
		/// Generally, the safest way to get a consistent, unique string
		/// address is to put the address1, address2, city, and state together.
		/// Zip codes can change at the USPS' whims, and the Delivery Point
		/// Barcode (DPBC) can change as well. See 
		/// <seealso cref="http://smartystreets.com/kb/faq/do-addresses-have-a-unique-identifier"/>
		/// </summary>
		public string SafeUniqueAddress
		{
			get
			{
				string address = string.Empty;

				if (string.IsNullOrWhiteSpace(this.Address1) ||
				   string.IsNullOrWhiteSpace(this.City) ||
				   string.IsNullOrWhiteSpace(this.State))
					throw new ArgumentNullException("Must have full address.");

				//address line
				address += this.Address1.Trim();
				if (!string.IsNullOrWhiteSpace(this.Address2))
					address += " " + this.Address2.Trim();

				//city
				if (address.Length > 0)
					address += ", " + this.City.Trim();
				else
					address += this.City.Trim();

				//state
				if (address.Length > 0)
					address += ", " + this.State.Trim();
				else
					address += this.State.Trim();

				return address;
			}
		}

		public string UniqueHash
		{
			get
			{
				return Hashing.Md5(SafeUniqueAddress);
			}
		}

		public string FullAddress
		{
			get
			{
				string address = string.Empty;

				//address line
				if (!string.IsNullOrWhiteSpace(this.Address1))
				{
					address += this.Address1.Trim();
					if (!string.IsNullOrWhiteSpace(this.Address2))
						address += " " + this.Address2.Trim();
				}

				//city
				if (!string.IsNullOrWhiteSpace(this.City))
					if (address.Length > 0)
						address += ", " + this.City.Trim();
					else
						address += this.City.Trim();

				//state
				if (!string.IsNullOrWhiteSpace(this.State))
					if (address.Length > 0)
						address += ", " + this.State.Trim();
					else
						address += this.State.Trim();

				if (!string.IsNullOrWhiteSpace(this.Zip))
					if (address.Length > 0)
						address += " " + this.Zip.Trim();
					else
						address += this.Zip.Trim();

				return address;
			}
		}

		public static Address FromSmartyStreetsAddress(SmartyStreetsAddress add)
		{
			return new Address
			{
				Address1 = string.Format("{0} {1} {2} {3} {4}",
				   add.components.primary_number,
				   add.components.street_predirection,
				   add.components.street_name,
				   add.components.street_suffix,
				   add.components.street_postdirection).Replace("  ", " ").Trim(),
				Address2 = string.Format("{0} {1}",
					add.components.secondary_designator,
					add.components.secondary_number).Replace("  ", " ").Trim(),
				City = add.components.default_city_name ?? add.components.city_name,
				State = add.components.state_abbreviation,
				Zip = add.components.zipcode
			};
		}
	}
}
