using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Cynet.Infra.Helpers.Standard.ExtensionMethods
{
    public static class TcpClientExtensionMethods
    {
        /// <summary>
        /// Connects the specified socket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <param name="timeout">The timeout.</param>
        public static void Connect(this TcpClient client, string host, int port, TimeSpan timeout)
        {
            AsyncConnect(client, (s, a, o) => s.BeginConnect(host, port, a, o), timeout);
        }

        /// <summary>
        /// Connects the specified socket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="addresses">The addresses.</param>
        /// <param name="port">The port.</param>
        /// <param name="timeout">The timeout.</param>
        public static void Connect(this TcpClient socket, IPAddress[] addresses, int port, TimeSpan timeout)
        {
            AsyncConnect(socket, (s, a, o) => s.BeginConnect(addresses, port, a, o), timeout);
        }

        /// <summary>
        /// Asyncs the connect.
        /// </summary>
        /// <param name="client">The socket.</param>
        /// <param name="connect">The connect.</param>
        /// <param name="timeout">The timeout.</param>
        private static void AsyncConnect(TcpClient client, Func<Socket, AsyncCallback, object, IAsyncResult> connect, TimeSpan timeout)
        {
            var asyncResult = connect(client.Client, null, null);
            if (!asyncResult.AsyncWaitHandle.WaitOne(timeout))
            {
                try
                {
                    client.EndConnect(asyncResult);
                }
                catch (SocketException)
                { }
                catch (ObjectDisposedException)
                { }
            }
        }
    }
}
