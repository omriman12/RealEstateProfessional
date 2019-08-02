
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers
{
    public static class Convertor
    {
        public static long IPToLong(string ip)
        {
            try
            {
                string[] ipBytes;
                double num = 0;
                if (!string.IsNullOrEmpty(ip))
                {
                    ipBytes = ip.Split('.');
                    for (int i = ipBytes.Length - 1; i >= 0; i--)
                    {
                        num += ((int.Parse(ipBytes[i]) % 256) * Math.Pow(256, (3 - i)));
                    }
                }
                return (long)num;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static uint IPToUInt(string ip)
        {
            try
            {
                string[] ipBytes;
                double num = 0;
                if (!string.IsNullOrEmpty(ip))
                {
                    ipBytes = ip.Split('.');
                    for (int i = ipBytes.Length - 1; i >= 0; i--)
                    {
                        num += ((int.Parse(ipBytes[i]) % 256) * Math.Pow(256, (3 - i)));
                    }
                }
                return (uint)num;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static ulong MACToLong(string strMacAddress)
        {
            strMacAddress = strMacAddress.Replace("-", "");
            return ulong.Parse(strMacAddress, NumberStyles.HexNumber);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static string LongToIP(long longIP)
        {
            try
            {
                string ip = string.Empty;
                for (int i = 0; i < 4; i++)
                {
                    int num = (int)(longIP / Math.Pow(256, (3 - i)));
                    longIP = longIP - (long)(num * Math.Pow(256, (3 - i)));
                    if (i == 0)
                        ip = num.ToString();
                    else
                        ip = ip + "." + num.ToString();
                }
                return ip;
            }
            catch (Exception ex)
            {
                Console.WriteLine("WTF Error" + ex.ToString());
                return "0.0.0.0";
            }
        }
        /// <summary>
        /// Convert an IP address from string to UInt32
        /// </summary>
        /// <param name="strIP">IP address as string</param>
        /// <returns>IP address as UInt32</returns>
        public static UInt32 StringIpToUInt32(string strIP)
        {
            string[] strIPSplitted = strIP.Split('.');
            UInt32 ip = 0;
            for (int i = 0; i < strIPSplitted.Length; i++)
            {
                ip = (ip << 8) + UInt32.Parse(strIPSplitted[i]);
            }
            return ip;
        }
        /// <summary>
        /// Convert an IP address from UInt32 to string
        /// </summary>
        /// <param name="ip">IP address as UInt32</param>
        /// <returns>IP address as string</returns>
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

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public static byte[] StringToByteArray(string hex)
        {
            if(hex == null)
            {
                return null;
            }
            else
            {
                return Enumerable.Range(0, hex.Length)
                                 .Where(x => x % 2 == 0)
                                 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                                 .ToArray();
            }
        }
        public static string ByteArrayToString(byte[] ba)
        {
            if (ba == null)
            {
                return null;
            }
            else
            {
                StringBuilder hex = new StringBuilder(ba.Length * 2);
                foreach (byte b in ba)
                {
                    hex.AppendFormat("{0:x2}", b);
                }
                return hex.ToString();
            }
        }

        public static string StringToSha1(string str)
        {
            //HashAlgorithm sha256 = new SHA256Managed();
            HashAlgorithm sha1 = new SHA1Managed();
            byte[] byteToHash = Encoding.UTF8.GetBytes(str);
            return BitConverter.ToString(sha1.ComputeHash(byteToHash)).Replace("-", string.Empty).ToLower();
        }

        public static string DataTableToJsonWithStringBuilder(DataTable table)
        {
            var jsonString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                jsonString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                         + "\":" + "\""
                         + getData( table.Rows[i][j].ToString() ) + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                         + "\":" + "\""
                         + getData( table.Rows[i][j].ToString() ) + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        jsonString.Append("}");
                    }
                    else
                    {
                        jsonString.Append("},");
                    }
                }
                jsonString.Append("]");
            }
            return jsonString.ToString();
        }

        private static string getData(string s)
        {
            if (s == null)
            {
                return "";
            }
            string ret = s.Replace(@"\", @"\\");
            ret = ret.Replace("'", @"\'");
            ret = ret.Replace(@"""", @"\""");
            return ret;
        }

        public static string DataSetToJson(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            foreach (DataTable table in ds.Tables)
            {
                var tb = DataTableToJsonWithStringBuilder(table);
                sb.AppendLine(tb);
                sb.Append(",");
            }
            if (ds.Tables.Count > 0)
                sb.Length--;

            sb.Append("]");

            return sb.ToString();
        }
        public static string DateToMySql(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
