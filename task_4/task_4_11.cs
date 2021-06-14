using System;

namespace Ushakov_4_11
{ 
    class Program
    {
       
        public static void runTest()
        {
            BinaryHeap heap1 = new BinaryHeap();
            BinaryHeap heap2 = new BinaryHeap();
            BinaryHeap heap3 = new BinaryHeap();

            Console.WriteLine("Creating binary heap heap1 and adding 1, 3, 5, 2, 10 to it");
            heap1.Insert(1);
            heap1.Insert(3);
            heap1.Insert(5);
            heap1.Insert(2);
            heap1.Insert(10);

            Console.WriteLine("Creating binary heap heap2 and adding 1, 3, 5, 2, 10 to it");
            heap2.Insert(1);
            heap2.Insert(3);
            heap2.Insert(5);
            heap2.Insert(2);
            heap2.Insert(10);

            Console.WriteLine("Creating binary heap heap3 and adding 10, 5, 22 to it");
            heap3.Insert(10);
            heap3.Insert(5);
            heap3.Insert(22);

            Console.WriteLine($"\nChecking if heap1 equals heap2: {heap1.Equals(heap2)}");
            Console.WriteLine($"Checking if heap1 equals heap3: {heap1.Equals(heap3)}");
        }

        static void Main(string[] args)
        {
            runTest();
        }
    }
}
