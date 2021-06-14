using System;
using System.Numerics;

namespace Ushakov_1_3
{
    class Program
    {
        private static string[] numbers = { "123456789", "123456789123456789", "123456789123456789123456789", "123456789123456789123456789123456789" };

        public static int GetDigitsSum(string number)
        {
            int result = 0;
            foreach (char d in number) result += int.Parse(d.ToString());
            return result;
        }

        public static int GetDigitsSum(BigInteger number)
        {
            BigInteger mod, sum = 0;

            while (number > 0)
            {
                mod = number % 10;
                sum += mod;
                number /= 10;
            }

            return (int) sum;
        }

        static void Main(string[] args)
        {

            Console.WriteLine("Calculating sum of digits in given a number. Asymptotic complexity of both algorithms is O(n) (linear)\n");
            Console.WriteLine("Method 1:\n");

            foreach (string s in numbers)
                Console.WriteLine($"Sum of digits in number {s} is: {GetDigitsSum(s)}"); 

            Console.WriteLine("\nMethod 2:\n");

            foreach (var s in numbers)
                Console.WriteLine($"Sum of digits in number {s} is: {GetDigitsSum(BigInteger.Parse(s))}");

        }
    }
}
