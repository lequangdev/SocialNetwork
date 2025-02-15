using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection.Options
{
    public record EntityFrameWorkConfiguration
    {
        public string? DefaultConnection {  get; set; }
    }
}
