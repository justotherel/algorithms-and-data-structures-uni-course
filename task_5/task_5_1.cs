using System;
using System.Collections.Generic;

namespace Ushakov_4_11
{ 
    class Program
    {
        static int[] a, temp;
        static int count;

        static int[] BubbleSort(int[] arr)
        {
            int temp;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j + 1] < arr[j])
                    {
                        temp = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
            return arr;
        }

        public static void Merge(int l, int m, int r)
        {
            int i = l;
            int j = m;
            for (int k = l; k < r; k++)
            {
                if (j == r || (i < m && a[i] <= a[j]))
                {
                    temp[k] = a[i];
                    i++;
                }
                else
                {
                    count += m - i;
                    temp[k] = a[j];
                    j++;
                }
            }
            Array.Copy(temp, l, a, l, r - l);
        }

        public static void MergeSort(int l, int r)
        {
            if (r <= l + 1) return;
            int m = (l + r) >> 1;
            MergeSort(l, m);
            MergeSort(m, r);
            Merge(l, m, r);
        }

        public static int[] Decorator(int[] a)
        {
            MergeSort(0, a.Length);
            return a;
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

            a = new int[] {5, 4, 3, 2, 1};
            temp = new int[5];
            int[] aSorted = a;
            Array.Sort(aSorted);

            test.Tester<int[], int[]> tester = new test.Tester<int[], int[]>(1);

            Console.WriteLine("Testing Bubble sort algorithm; Asymptotic complexity: O(n*n) (quadratic)\n");
            tester.Assert(new Func<int[], int[]>(BubbleSort), a, aSorted);
            Console.WriteLine("");

            List<int[]> arrayList = new List<int[]>();

            arrayList.Add(CreateArray(10000));
            arrayList.Add(CreateArray(50000));
            arrayList.Add(CreateArray(100000));

            tester.Run<int>(new Func<int[], int[]>(BubbleSort), arrayList[0]);
            tester.Run<int>(new Func<int[], int[]>(BubbleSort), arrayList[1]);
            tester.Run<int>(new Func<int[], int[]>(BubbleSort), arrayList[2]);

            tester = new test.Tester<int[], int[]>(10);

            Console.WriteLine("\nTesting Merge sort algorithm; Asymptotic complexity: O(n*log(n)); Memory usage: O(n) \n");
            tester.Assert(new Func<int[], int[]>(Decorator), a, aSorted);
            Console.WriteLine("");

            a = arrayList[0];
            temp = new int[10000];
            tester.Run<int>(new Func<int[], int[]>(Decorator), arrayList[0]);
            a = arrayList[1];
            temp = new int[50000];
            tester.Run<int>(new Func<int[], int[]>(Decorator), arrayList[1]);
            a = arrayList[2];
            temp = new int[100000];
            tester.Run<int>(new Func<int[], int[]>(Decorator), arrayList[2]);

        }

        static void Main(string[] args)
        {
            runTest();
        }
    }
}
