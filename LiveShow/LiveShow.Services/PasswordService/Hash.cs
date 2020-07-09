using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace LiveShow.Services.PasswordService
{
	public class Hash
	{
		public static string Create(string value, string salt)
		{
			var valueBytes = KeyDerivation.Pbkdf2(
								password: value,
								salt: System.Text.Encoding.UTF8.GetBytes(salt),
								prf: KeyDerivationPrf.HMACSHA512,
								iterationCount: 10000,
								numBytesRequested: 256 / 8);

			return Convert.ToBase64String(valueBytes);
		}
	}
}

