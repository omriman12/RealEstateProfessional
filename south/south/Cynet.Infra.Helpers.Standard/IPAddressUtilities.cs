using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers
{
    public class IPAddressUtilities
    {
        public static string UInt32IpToString(UInt32 ip)
        {
            UInt32 bitMask = 0xff000000;
            string[] seperated = new string[4];
            for (int i = 0; i < 4; i++)
            {
                UInt32 masked = (ip & bitMask) >> ((3 - i) * 8);
                bitMask >>= 8;
                seperated[i] = masked.ToString(CultureInfo.InvariantCulture);
            }
            return String.Join(".", seperated);
        }

        public static long ConvertIpToLong(string addr)
        {
            // careful of sign extension: convert to uint first;
            // unsigned NetworkToHostOrder ought to be provided.
            return (long)(uint)IPAddress.NetworkToHostOrder(
                 (int)IPAddress.Parse(addr).Address);
        }

        public static string ConvertIpToString(long address)
        {
            return IPAddress.Parse(address.ToString()).ToString();
        }

        public static bool IsIpLocal(long ip)
        {
            return 
            (ip==0) ||
            (ip >= 167772160 && ip <= 184549375)  ||
            (ip >= 2885681152 && ip <= 2902458367) || 
            (ip >= 3232235520 && ip <= 3232301055) ||
            (ip >= 2130706433 && ip <= 2147483647);
        }

        public static bool IsIp(string ipStr)
        {
            bool retVal = false;

            IPAddress addr = null;
            retVal = IPAddress.TryParse(ipStr, out addr);

            return retVal;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
