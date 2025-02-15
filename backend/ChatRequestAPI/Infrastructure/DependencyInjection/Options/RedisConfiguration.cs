using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection.Options
{
    public class RedisConfiguration
    {
        public bool Enabled { get; set; }
        public string? ConnectionString { get; set; }
    }
}
