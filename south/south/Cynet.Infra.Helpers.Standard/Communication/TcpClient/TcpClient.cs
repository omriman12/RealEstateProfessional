using Cynet.Infra.Helpers.Communication.TcpClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.Communication.TcpClient
{
    public class TcpClient : ITcpClient
    {
        public bool TcpRequest(string ipOrHostname, int port, AddressFamily AddressFamily, int timeout = 10 * 1000)
        {
            bool retVal = false;

            using (Socket socket = new Socket(AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                IAsyncResult result = socket.BeginConnect(ipOrHostname, port, null, null);
                retVal = result.AsyncWaitHandle.WaitOne(timeout, true);

                socket.Close();
            }

            return retVal;
        }

        public TcpConnectionInformation[] GetActiveTcpConnections()
        {
            return IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections();
        }
    }
}
