using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf417EncoderLibraryExt
{
    internal static class ZLibCompression
    {
        internal static byte[] Compress(byte[] InputBuf)
        {
            // input length
            int InputLen = InputBuf.Length;

            // create output memory stream to receive the compressed buffer
            MemoryStream OutputStream = new MemoryStream();

            // deflate compression object
            DeflateStream Deflate = new DeflateStream(OutputStream, CompressionMode.Compress, true);

            // load input buffer into the compression class
            Deflate.Write(InputBuf, 0, InputLen);

            // compress, flush and close
            Deflate.Close();

            // compressed file length
            int OutputLen = (int)OutputStream.Length;

            // create empty output buffer
            byte[] OutputBuf = new Byte[OutputLen + 18];

            // Header is made out of 16 bits [iiiicccclldxxxxx]
            // iiii is compression information. It is WindowBit - 8 in this case 7. iiii = 0111
            // cccc is compression method. Deflate (8 dec) or Store (0 dec)
            // The first byte is 0x78 for deflate and 0x70 for store
            // ll is compression level 2
            // d is preset dictionary. The preset dictionary is not supported by this program. d is always 0
            // xxx is 5 bit check sum (31 - header % 31)
            // write two bytes in most significant byte first
            OutputBuf[8] = 0x78;
            OutputBuf[9] = 0x9c;

            // copy the compressed result
            OutputStream.Seek(0, SeekOrigin.Begin);
            OutputStream.Read(OutputBuf, 10, OutputLen);
            OutputStream.Close();

            // successful exit
            return OutputBuf;
        }
    }
}
