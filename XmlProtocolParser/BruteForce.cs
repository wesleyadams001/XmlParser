using System;
using System.Collections.Generic;

namespace XmlProtocolParser
{
    /// <summary>
    /// Collection of byte array searches that employ the fastest possible brute force approach
    /// to search
    /// </summary>
    public static class BruteForce
    {
        /// <summary>
        /// Search for index of pattern in byteSource
        /// I have found this to be helpful in moving over bytes I
        /// cannot avoid iterating over by skipping around with 
        /// KMP
        /// </summary>
        /// <param name="byteSource"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static unsafe long IndexOf(this byte[] byteSource, byte[] pattern)
        {
            fixed (byte* H = byteSource) fixed (byte* N = pattern)
            {
                long i = 0;
                for (byte* hNext = H, hEnd = H + byteSource.LongLength; hNext < hEnd; i++, hNext++)
                {
                    bool Found = true;
                    for (byte* hInc = hNext, nInc = N, nEnd = N + pattern.LongLength; Found && nInc < nEnd; Found = *nInc == *hInc, nInc++, hInc++) ;
                    if (Found) return i;
                }
                return -1;
            }
        }
        

    }
}
