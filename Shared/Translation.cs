using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDeff.Shared
{
    public class Translation
    {
        public int Id {get; set;}
        public string OutputText {get; set;} = string.Empty;

        public string? FileName { get; set;}
        public string? StoredFileName { get; set;}
    }
}
