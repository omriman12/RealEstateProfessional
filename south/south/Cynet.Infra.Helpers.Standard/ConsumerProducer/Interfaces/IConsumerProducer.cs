using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.ConsumerProducer.Interfaces
{
    public interface IConsumerProducer<T>
    {
        //props
        int Count { get; }

        //func
        void Enqueue(T item);
        T Dequeue();
        bool IsQueueEmpty();
    }
}
