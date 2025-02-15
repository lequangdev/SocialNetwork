using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection.Options
{
        public class MasstransitConfiguration
        {
            public string? Host { get; set; }
            public string? VHost { get; set; }
            public string? UserName { get; set; }
            public string? Password { get; set; }
            public string? ExchangeName { get; set; }
            public string? ExchangeType { get; set; }
            public string? SmsQueueName { get; set; }
            public string? EmailQueueName { get; set; }
        }
 }


