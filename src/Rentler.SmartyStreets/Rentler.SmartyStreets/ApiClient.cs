using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rentler.SmartyStreets
{
	public class ApiClient
	{
		HttpClient client = new HttpClient();

		static readonly Lazy<ApiClient> instance = new Lazy<ApiClient>();

		public ApiClient() { }

		public static ApiClient Instance
		{
			get { return instance.Value; }
		}

		public Uri CreateAddress(string endpoint, Dictionary<string, string> args)
		{
			var url = "https://api.smartystreets.com/";
			url += endpoint + "?" + args.ToString("=", "&");
			url = Uri.EscapeUriString(url);

			return new Uri(url);
		}

		public async Task<Stream> Get(Uri url)
		{
			var result = await client.GetAsync(url);
			return await result.Content.ReadAsStreamAsync();
		}

		public async Task<Stream> Post(Uri url)
		{
			var result = await client.PostAsync(url, null);
			return await result.Content.ReadAsStreamAsync();
		}
	}
}
