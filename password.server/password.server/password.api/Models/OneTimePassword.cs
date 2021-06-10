using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace password.api.Models
{
    public class OneTimePassword
    {
        public DateTime CreatedAtUtc { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
        public OneTimePassword() : base()
        {
            CreatedAtUtc = DateTime.UtcNow;
        }
    }
}
