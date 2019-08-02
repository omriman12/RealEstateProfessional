using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.Compressing
{
    public interface ICompressingService
    {
        void CompressData(byte[] inData, out byte[] outData);
        void DecompressData(byte[] inData, out byte[] outData);
    }
}
