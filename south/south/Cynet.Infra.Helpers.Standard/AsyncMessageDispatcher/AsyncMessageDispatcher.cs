using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.AsyncMessageDispatcher
{
    public abstract class AsyncMessgeDispatcher<T> : IDisposable
    {
        private ConcurrentQueue<T> m_MsgQueue = null;
        private Task m_MsgDispatchrThread = null;
        private CancellationTokenSource m_CancelToken = null;
        private SemaphoreSlim m_MsgQueueSemaphore = null;
        private string m_MsgQueueName = null;
        private int m_MaxMessagesInQueue;
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(AsyncMessgeDispatcher<T>));

        private const int MAX_MESSAGES_IN_QUEUE = 100000;
        public void Init(int maxMsgInQueue = MAX_MESSAGES_IN_QUEUE, string queueName = "N/A")
        {
            m_MaxMessagesInQueue = maxMsgInQueue;
            m_MsgQueueName = queueName;
            m_MsgQueue = new ConcurrentQueue<T>();
            m_CancelToken = new CancellationTokenSource();
            m_MsgQueueSemaphore = new SemaphoreSlim(0);
            m_MsgDispatchrThread = Task.Factory.StartNew(msgDispatchThread, m_CancelToken.Token,
                TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }


        private async void msgDispatchThread()
        {
            s_Logger.Info(String.Format("starting asyn message dispatcher:{0}", m_MsgQueueName));
            try
            {
                while (!m_CancelToken.IsCancellationRequested)
                {
                    //Wait for incoming message
                    m_MsgQueueSemaphore.Wait(m_CancelToken.Token);

                    if (m_CancelToken.IsCancellationRequested)
                    {
                        continue;
                    }

                    T message;
                    bool isMsgOk = m_MsgQueue.TryDequeue(out message);
                    if (!isMsgOk)
                    {
                        s_Logger.Error("Failed to extract message from message queue");
                    }
                    else
                    {
                        try
                        {
                            //Dispatch message to client
                            await HandleMessageAsync(message);
                        }
                        catch (Exception ex)
                        {
                            s_Logger.Error("Error occured after message dispatch", ex);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                if (!m_CancelToken.IsCancellationRequested)
                {
                    s_Logger.Error(String.Format("Error occured during message dispatcher:{0}  thread runtime", m_MsgQueueName), ex);
                }
                else
                {
                    s_Logger.Info(String.Format("async message dispatcher:{0} was terminated by request", m_MsgQueueName));
                }
            }
        }

        public void RegisterMsg(T msg)
        {
            try
            {
                if (m_MsgQueue.Count < m_MaxMessagesInQueue)
                {
                    m_MsgQueue.Enqueue(msg);
                    m_MsgQueueSemaphore.Release();
                }
                else
                {
                    s_Logger.Warn(String.Format("The queue:{0} is reached its maxumum  of:{1} messages!!!, new messages will not be handled!", m_MsgQueueName, m_MaxMessagesInQueue));
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error(String.Format("Failed to enqueue new message to async dispatch queue:{0}", m_MsgQueueName), ex);
                throw;
            }
        }


        /// <summary>
        /// This method will be implemented by the derived that will use the async dispatch service
        /// </summary>
        /// <param name="msg"></param>
        protected abstract Task HandleMessageAsync(T msg);

        public void Dispose()
        {
            if (m_CancelToken != null)
            {
                m_CancelToken.Cancel();
            }
        }
    }
}

