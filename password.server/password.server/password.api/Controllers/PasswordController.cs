using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using password.api.Services;
using password.api.Models;
using Microsoft.Extensions.Caching.Memory;

namespace password.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private IMemoryCache _cache;
        private PasswordServices _passwordServices;

        public PasswordController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
            _passwordServices = new PasswordServices(_cache);
        }

        [HttpPost("use")]
        public object Use([FromBody] Credentials credentials)
        {
            bool isAccessGranted = _passwordServices.Use(credentials);
            return new { isAccessGranted };
        }

        [HttpPost("add")]
        public object Add([FromBody] Credentials credentials)
        { 
            string oneTimePassword =  _passwordServices.AddAsync(credentials.UserId.ToString());
            return new { password = oneTimePassword };
        }
    }
}
