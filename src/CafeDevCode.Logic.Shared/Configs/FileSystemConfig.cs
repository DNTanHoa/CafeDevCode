using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Logic.Shared.Configs
{
    public class FileSystemConfig
    {
        public static string ConfigName = "FileConfig";

        public string RootFolder { get; set; } = string.Empty;
    }
}
