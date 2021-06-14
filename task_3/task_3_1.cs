using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ushakov_1_3
{ 
    class Program
    {

        public static int[] CreateArray(int length, int minVal = 0, int maxVal = int.MaxValue)
        {
            var rand = new Random();

            int[] array = new int[length];

            for (int i = 0; i < length; i++)
                array[i] = rand.Next();

            return array;
        }

        public static int[] CreateArray(int length)
        {
            var rand = new Random();

            int[] array = new int[length];

            for (int i = 0; i < length; i++)
                array[i] = i + 1;

            return array;
        }

        public static void runTest()
        {
            int[] testArr = { 1, 1, 1 };
            Stopwatch timer = new Stopwatch();
            int max = testArr.Max(), arrAmount = 3, Tries = 10;
  
            List<int[]> arrayList = new List<int[]>();
            Console.WriteLine("Testing Find() method; Asymtotic complexity: O(n), worst case scenario: O(n)\n");

            for (int i = 0; i < arrAmount; i++)
            {
                long ms = 0;
                int N = 10000 * (int)Math.Pow(10, i);
                arrayList.Add(CreateArray(N));

                for (int j = 0; j < Tries; j++)
                {
                    timer.Start();
                    int res = Array.Find<int>(arrayList[i], n => n == N - 1);
                    timer.Stop();
                    ms += timer.ElapsedMilliseconds;
                }

                timer.Reset();

                Log(ms / Tries, N, Tries);
            }

            Console.WriteLine("\nTesting binary search; Asymtotic complexity: O(log(n)), worst case scenario: O(log(n))\n");

            for (int i = 0; i < arrAmount; i++)
            {
                long ms = 0;
                int N = 10000 * (int)Math.Pow(10, i);

                for (int j = 0; j < Tries; j++)
                {
                    timer.Start();
                    int res = Array.BinarySearch(arrayList[i], N - 10);
                    timer.Stop();
                    ms += timer.ElapsedMilliseconds;
                }

                timer.Reset();

                Log(ms / Tries, N, Tries);
            }

        }

        public static void Log(long ms, int N, int Tries)
        {
            Console.WriteLine($"Parameter: {N}; Number of tests: {Tries}; Average completion time: {ms} ms");
        }

        static void Main(string[] args)
        {
            runTest();
        }
    }
}
