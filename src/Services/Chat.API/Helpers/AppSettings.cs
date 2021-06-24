using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Helpers
{
    public class AppSettings
    {
        public AppSettings()
        {
        }

        public AppSettings(string secret)
        {
            Secret = secret;
        }

        public string Secret { get; set; }
    }
}