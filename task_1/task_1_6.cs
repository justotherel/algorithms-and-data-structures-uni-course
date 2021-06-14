using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace Ushakov_1_3
{
    class Program
    {

        public static T[] Inverse<T>(T[] original)
        {
            T[] reversed = new T[original.Length];

            int j = 0;

            for (int i = original.Length; i-- > 0;)
            {
                reversed[j] = original[i];
                j++;
            }

            return reversed;
        }

        public static int[] InverseIntOnSpot(int[] original) 
        {
            int len = original.Length - 1;

            for (int i = 0; i < original.Length/2; i++)
            {
                //a = a + b
                //b = a - b
                //a = a - b

                original[i] += original[len - i];
                original[len - i] = original[i] - original[len - i];
                original[i] -= original[len - i];

            }

            return original;
        }

        public static T[] InverseOnSpot<T>(T[] original)
        {
            int len = original.Length - 1;

            for (int i = 0; i < original.Length / 2; i++)
            {
                T tempVal = original[i];
                original[i] = original[len-i];
                original[len - i] = tempVal;

            }

            return original;
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
            int[] testReversed = new int[n];
            Array.Copy(testArr, testReversed, n);
            Array.Reverse(testReversed, 0, n);

            test.Tester<int[], int[]> tester = new test.Tester<int[], int[]>(10);

            Console.WriteLine("Testing method 1 (Using additional memory) Asymptotic complexity: O(n) (linear)\n");
            tester.Assert(new Func<int[], int[]>(Inverse<int>), testArr, testReversed);
            Console.WriteLine("");

            int arrAmount = 4;
            List<int[]> arrayList = new List<int[]>();

            for (int i = 0; i < arrAmount; i++)
            {
                arrayList.Add(CreateArray(100000 * (int)Math.Pow(10, i)));
                tester.Run<int>(new Func<int[], int[]>(Inverse<int>), arrayList[arrayList.Count - 1]);
            }

            Console.WriteLine("\nTesting method 2 (Using no additional memory) Asymptotic complexity: O(n) (linear)\n");
            tester.Assert(new Func<int[], int[]>(InverseIntOnSpot), testArr, testReversed);
            Console.WriteLine("");

            for (int i = 0; i < arrAmount; i++)
            {
                tester.Run<int>(new Func<int[], int[]>(InverseIntOnSpot), arrayList[i]);
            }

            Array.Reverse(testArr, 0, n);

            Console.WriteLine("\nTesting method 3 (Using only one additional variable) Asymptotic complexity: O(n) (linear)\n");
            tester.Assert(new Func<int[], int[]>(InverseOnSpot), testArr, testReversed);
            Console.WriteLine("");

            for (int i = 0; i < arrAmount; i++)
            {
                tester.Run<int>(new Func<int[], int[]>(InverseOnSpot), arrayList[i]);
            }
        }

        static void Main(string[] args)
        {
            runTest();
        }
    }
}
