using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EF_core
{
    public record EntityFrameWorkConfiguration
    {
        public string? DefaultConnection { get; set; }
    }
}
