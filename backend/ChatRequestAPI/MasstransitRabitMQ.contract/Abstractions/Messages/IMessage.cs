using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace MasstransitRabitMQ.contract.Abstractions.Messages
{
    [ExcludeFromTopology]
    public interface IMessage
    {
        public Guid Id { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
