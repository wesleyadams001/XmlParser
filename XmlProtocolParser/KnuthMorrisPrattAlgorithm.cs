using System;
using System.Collections.Generic;
using System.Text;

namespace XmlProtocolParser
{
    class KnuthMorrisPrattAlgorithm
    {
       /// <summary>
       /// Use implementation of KMP to find first occurance of a byte pattern 
       /// </summary>
       /// <param name="input"></param>
       /// <param name="pattern"></param>
       /// <returns></returns>
        public int Search(byte[] input, byte[] pattern)
        {
            //initialize
            int N = input.Length;
            int M = pattern.Length;

            //catches for bad inputs
            if (N < M) return -1;
            if (N == M && input == pattern) return 0;
            if (M == 0) return 0;

            //allocate array
            int[] lpsArray = new int[M];
            int matchedIndex = -1;

            //set up longest prefix suffix table
            LongestPrefixSuffix(pattern, ref lpsArray);

            int i = 0, j = 0;
            while (i < N)
            {
                if (input[i] == pattern[j])
                {
                    i++;
                    j++;
                }

                // found a match at i-j
                if (j == M)
                {
                    matchedIndex = i - j;
                    //Console.WriteLine((i - j).ToString());
                    j = lpsArray[j - 1];
                }
                else if (i < N && input[i] != pattern[j])
                {
                    if (j != 0)
                    {
                        j = lpsArray[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            return matchedIndex;
        }

        public void LongestPrefixSuffix(byte[] pattern, ref int[] lpsArray)
        {
            int M = pattern.Length;
            int len = 0;
            lpsArray[0] = 0;
            int i = 1;

            while (i < M)
            {
                if (pattern[i] == pattern[len])
                {
                    len++;
                    lpsArray[i] = len;
                    i++;
                }
                else
                {
                    if (len == 0)
                    {
                        lpsArray[i] = 0;
                        i++;
                    }
                    else
                    {
                        len = lpsArray[len - 1];
                    }
                }
            }
        }
        
    }
}
