using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection.Options
{
    public class SerilogConfiguration
    {
        public string MinimumLevel { get; set; } = "Information";
        public List<SerilogSink> WriteTo { get; set; } = new();
    }
    public class SerilogSink
    {
        public string Name { get; set; } = "";
        public Dictionary<string, string> Args { get; set; } = new();
    }
}

