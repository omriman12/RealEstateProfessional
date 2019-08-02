using Cynet.Infra.Helpers.ConsumerProducer.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.ConsumerProducer
{
    public class ConsumerProducer<T>  : IConsumerProducer<T>, IDisposable
    {
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(ConsumerProducer<T>));

        private ConcurrentQueue<T> m_Queue = null;
        private string m_QueueName;
        private int m_MaxMessagesInQueue;
        //private CancellationTokenSource m_CancelToken = null;
        private const int MAX_MESSAGES_IN_QUEUE = 100000;
        public ConsumerProducer(int maxMsgInQueue = MAX_MESSAGES_IN_QUEUE, string queueName = "N/A")
        {
            m_Queue = new ConcurrentQueue<T>();
            m_QueueName = queueName;
            m_MaxMessagesInQueue = maxMsgInQueue;
        }

        public int Count
        {
            get
            {
                return m_Queue.Count;
            }
        }

        public void Enqueue(T item)
        {
            try
            {
                if (m_Queue.Count < m_MaxMessagesInQueue)
                {
                    m_Queue.Enqueue(item);         
                }
                else
                {
                    s_Logger.Warn(String.Format("The queue:{0} is reached its maxumum  of:{1} messages!!!, new messages will not be handled!", m_QueueName, m_MaxMessagesInQueue));
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error(String.Format("Failed to enqueue new message to queue:{0}", m_QueueName), ex);
                throw;
            }
        }

        public T Dequeue()
        {
            T retVal;
            try
            {
                var stattus = m_Queue.TryDequeue(out retVal);
                if (stattus == false)
                {
                    throw new ApplicationException(string.Format("failed to dequeue for queue named:{0}", m_QueueName));
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error(String.Format("Failed to dequeue new message to queue:{0}", m_QueueName), ex);
                throw;
            }


            return retVal;
        }

        public bool IsQueueEmpty()
        {
            return m_Queue.Count == 0;
        }

        

        public void Dispose()
        {
            //if (m_CancelToken != null)
            //{
            //    m_CancelToken.Cancel();
            //}
        }
    }
}
