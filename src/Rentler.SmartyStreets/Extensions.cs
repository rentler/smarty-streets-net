using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentler.SmartyStreets
{
	/// <summary>
	/// Utility class for commonly used extension methods.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Used to handle serialization of something like a
		/// Dictionary<string,string> to a proper querystring.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="keyValueSeparator"></param>
		/// <param name="sequenceSeparator"></param>
		/// <returns></returns>
		public static string ToString(
			this Dictionary<string, string> source,
			string keyValueSeparator, string sequenceSeparator)
		{
			if (source == null)
				throw new ArgumentException("Parameter source can not be null.");
			var pairs = source
				.Where(k => !string.IsNullOrWhiteSpace(k.Value))
				.Select(x => string.Format("{0}{1}{2}", x.Key, keyValueSeparator, x.Value)).ToArray();

			return string.Join(sequenceSeparator, pairs);
		}
	}
}
