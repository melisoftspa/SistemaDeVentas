namespace Pdf417EncoderLibraryExt
{
    internal static class Adler32
    {
        /////////////////////////////////////////////////////////////////////
        // Accumulate Adler Checksum
        /////////////////////////////////////////////////////////////////////

        internal static uint Checksum
                (
                byte[] Buffer,
                int Pos,
                int Len
                )
        {
            const uint Adler32Base = 65521;

            // split current Adler chksum into two 
            uint AdlerLow = 1; // AdlerValue & 0xFFFF;
            uint AdlerHigh = 0; // AdlerValue >> 16;

            while (Len > 0)
            {
                // We can defer the modulo operation:
                // Under worst case the starting value of the two halves is 65520 = (AdlerBase - 1)
                // each new byte is maximum 255
                // The low half grows AdlerLow(n) = AdlerBase - 1 + n * 255
                // The high half grows AdlerHigh(n) = (n + 1)*(AdlerBase - 1) + n * (n + 1) * 255 / 2
                // The maximum n before overflow of 32 bit unsigned integer is 5552
                // it is the solution of the following quadratic equation
                // 255 * n * n + (2 * (AdlerBase - 1) + 255) * n + 2 * (AdlerBase - 1 - UInt32.MaxValue) = 0
                Int32 n = Len < 5552 ? Len : 5552;
                Len -= n;
                while (--n >= 0)
                {
                    AdlerLow += (UInt32)Buffer[Pos++];
                    AdlerHigh += AdlerLow;
                }
                AdlerLow %= Adler32Base;
                AdlerHigh %= Adler32Base;
            }
            return ((AdlerHigh << 16) | AdlerLow);
        }
    }
}