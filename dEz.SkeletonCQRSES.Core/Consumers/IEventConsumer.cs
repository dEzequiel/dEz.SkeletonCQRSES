using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.ES.Core.Consumers
{
    public interface IEventConsumer
    {
        void Consume(string topic);
    }
}
