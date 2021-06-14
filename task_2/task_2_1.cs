using System;
using System.Collections.Generic;
using System.Linq;

namespace Ushakov_2_1
{ 
    class Program
    {
        public static int Max(int[] original, int offset, int max)
        {
            if (offset == original.Length - 1)
            {
                if (original[offset] > max)
                    return original[offset];

                return max;
            }
            else if (original[offset] > max)
                return Max(original, offset + 1, original[offset]);

            else
                return Max(original, offset + 1, max);
        }
        public static int[] CreateArray(int length)
        {
            var rand = new Random();

            int[] array = new int[length];

            for (int i = 0; i < length; i++)
                array[i] = rand.Next();

            return array;
        }
        public static void runTest()
        {
            int n = 3;
            int[] testArr = CreateArray(n);
            int max = testArr.Max(), arrAmount = 4;

            List<int[]> arrayList = new List<int[]>();

            test.Tester<int[], int> tester = new test.Tester<int[], int>(10);

            Console.WriteLine("Testing recursive max element algorithm; Asymptotic complexity: O(n) (linear)\n");
            tester.Assert<int>(new Func<int[], int, int, int>(Max), testArr, 0, testArr[0], max);
            Console.WriteLine("");

            for (int i = 0; i < arrAmount; i++)
            {
                arrayList.Add(CreateArray(10 * (int)Math.Pow(10, i)));
                tester.Run<int>(new Func<int[], int, int, int>(Max), arrayList[arrayList.Count - 1], 0, arrayList[arrayList.Count-1][0]);
            }

        }
        static void Main(string[] args)
        {
            runTest();
        }
    }
}
