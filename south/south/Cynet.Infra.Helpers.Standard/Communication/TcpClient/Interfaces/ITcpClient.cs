using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.Communication.TcpClient.Interfaces
{
    public interface ITcpClient
    {
        bool TcpRequest(string ipOrHostname, int port, AddressFamily AddressFamily, int timeout = 10 * 1000);
        TcpConnectionInformation[] GetActiveTcpConnections();
    }
}
