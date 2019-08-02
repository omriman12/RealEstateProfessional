using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.Compressing
{
    public class ZLibCompressionService : ICompressingService
    {
        public void CompressData(byte[] inData, out byte[] outData)
        {
            outData = null;

            if (inData == null)
            {
                throw new ArgumentNullException("inData");
            }

            using (MemoryStream outMemoryStream = new MemoryStream())
            {
                using (DeflateStream dstream = new DeflateStream(outMemoryStream, CompressionLevel.Optimal))
                {
                    dstream.Write(inData, 0, inData.Length);
                    outData = outMemoryStream.ToArray();
                }
            }
        }

        public void DecompressData(byte[] inData, out byte[] outData)
        {
            outData = null;

            if (inData == null)
            {
                throw new ArgumentNullException("inData");
            }

            using (MemoryStream input = new MemoryStream(inData))
            {
                using (MemoryStream output = new MemoryStream())
                {
                    using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
                    {
                        dstream.CopyTo(output);
                    }
                    outData = output.ToArray();
                }
            }
        }
        

        private void copyStream(System.IO.Stream input, System.IO.Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }
    }
}
