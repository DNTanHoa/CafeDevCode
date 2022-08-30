using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Shared.Configs
{
    public class SiteConfig
    {
        public static string ConfigName { get; set; } = "SiteConfig";
        public string HttpUrl { get; set; } = string.Empty;
        public string HttpsUrl { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string SiteMapDirectory { get; set; } = string.Empty;
        public string SiteMapPath { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}
