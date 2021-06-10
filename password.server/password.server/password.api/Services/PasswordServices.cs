using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using password.api.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace password.api.Services
{
    public class PasswordServices
    {
        private IMemoryCache _cache;
        public PasswordServices(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }    

        public string AddAsync (string userId)
        {
            OneTimePassword oneTimePassword = new OneTimePassword();
            oneTimePassword.UserId = userId;
            GeneratePassword(oneTimePassword);
            _cache.Set(userId, oneTimePassword, TimeSpan.FromSeconds(30));
            return oneTimePassword.Password;
        }

        public bool Use(Credentials credentials)
        {
            if(_cache.TryGetValue(credentials.UserId, out object value) && credentials.Password == ((OneTimePassword)value).Password)
            {
                _cache.Remove(credentials.UserId);
                return true;
            }
            return false;
        }

        private void GeneratePassword(OneTimePassword password)
        {
            var hmacHash = new HMACSHA256();
            hmacHash.Key = Encoding.ASCII.GetBytes(password.UserId);
            var hashCode = hmacHash.ComputeHash(BitConverter.GetBytes(password.CreatedAtUtc.Ticks));
            var offset = hashCode[19] & 0xf;
            var truncatedHash = (hashCode[offset++] & 0x7f) << 24 | (hashCode[offset++] & 0xff) << 16 | (hashCode[offset++] & 0xff) << 8 | (hashCode[offset++] & 0xff);
            password.Password = truncatedHash.ToString();
        }
    }
}
