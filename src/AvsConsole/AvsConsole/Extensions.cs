using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvsConsole
{
	public static class Extensions
	{
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
