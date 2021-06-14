using System;

namespace Ushakov_3_5
{ 
    class Program
    {
        public static Stack CreateStack(int N)
        {
            Stack stack = new Stack(N);
            for (int i = 0; i < N; i++) {
                int r = Random();
                Console.WriteLine($"Putting {r} into stack");
                stack.Push(r);
            }

            return stack;
        }

        static void PrintStack(Stack stack)
        {

            if (stack.IsEmpty())
                return;

            int val = stack.Peek();

            stack.Pop();
            PrintStack(stack);
            Console.Write(val + " ");
            stack.Push(val);
        }

        public static int Random()
        {
            Random rand = new Random();
            return rand.Next();
        }

        public static void runTest()
        {  
            Console.WriteLine("Creating a stack and filling it with random integer values (10); Asymtotic complexity: O(n)\n");
            Stack stack = CreateStack(10);
            Console.WriteLine("\nStack values (top to bottom):");
            Console.WriteLine(stack.ToString() + "\n");
            Console.WriteLine("Recursively printing all stack values; Asymtotic complexity: O(n)\n");
            Console.WriteLine("Stack values (bottom to top):");
            PrintStack(stack);
            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            runTest();
        }
    }
}
