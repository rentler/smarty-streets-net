using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rentler.SmartyStreets
{
	/// <summary>
	/// Utility class for computing SHA1 and MD5 hashes.
	/// </summary>
	public static class Hashing
	{
		/// <summary>
		/// Hashes a string in precisely the same manner that
		/// FormsAuthentication.HashPasswordForStoringInConfigFile(value, "SHA1") did.
		/// </summary>
		/// <param name="value">The string to convert to a hash</param>
		/// <returns>a SHA1 hashed form of the string value.</returns>
		public static string Sha1(string value)
		{
			SHA1 algorithm = SHA1.Create();
			byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
			string sh1 = "";
			for (int i = 0; i < data.Length; i++)
				sh1 += data[i].ToString("x2").ToUpperInvariant();
			return sh1;
		}

		/// <summary>
		/// Hashes a string in precisely the same manner that
		/// FormsAuthentication.HashPasswordForStoringInConfigFile(value, "MD5") did.
		/// </summary>
		/// <param name="value">The string to convert to a hash</param>
		/// <returns>an MD5 hashed form of the string value.</returns>
		public static string Md5(string value)
		{
			MD5 algorithm = MD5.Create();
			byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
			string md5 = "";
			for (int i = 0; i < data.Length; i++)
				md5 += data[i].ToString("x2").ToUpperInvariant();
			return md5;
		}
	}
}
