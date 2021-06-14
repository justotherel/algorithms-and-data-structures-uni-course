using System;
using System.Diagnostics;
using System.Linq;

namespace test
{
    class Tester<InVal, ReturnVal>
    {

        private int Tries { get; set; }
        private readonly Stopwatch timer;

        public Tester(int tries = 10)
        {
            Tries = tries;
            timer = new Stopwatch();
        }

        // Doesn't work with arrays
        public void Run(Func<InVal, ReturnVal> Method, InVal parameter)
        {
            long ms = 0;
            for (int i = 0; i < Tries; ++i)
            {
                timer.Start();
                Method.Invoke(parameter);
                timer.Stop();
                ms += timer.ElapsedMilliseconds;
            }

            timer.Reset();
            Log(ms / Tries, parameter);

        }

        public void Run<T>(Func<T[], T[]> Method, T[] parameter)
        {
            long ms = 0;
            for (int i = 0; i < Tries; ++i)
            {
                timer.Start();
                Method.Invoke(parameter);
                timer.Stop();
                ms += timer.ElapsedMilliseconds;
            }

            timer.Reset();
            Log<T>(ms / Tries, parameter);

        }

        public void Run<T>(Func<InVal, T, T, ReturnVal> Method,
            InVal firstParam, T secondParam, T thirdParam)
        {
            long ms = 0;
            for (int i = 0; i < Tries; ++i)
            {
                timer.Start();
                Method.Invoke(firstParam, secondParam, thirdParam);
                timer.Stop();
                ms += timer.ElapsedMilliseconds;
            }

            Log(ms / Tries, firstParam);
        }

        public void Run<T>(Func<T[], T, T, ReturnVal> Method,
            T[] firstParam, T secondParam, T thirdParam)
        {
            long ms = 0;
            for (int i = 0; i < Tries; ++i)
            {
                timer.Start();
                Method.Invoke(firstParam, secondParam, thirdParam);
                timer.Stop();
                ms += timer.ElapsedMilliseconds;
            }

            Log<T>(ms / Tries, firstParam);
        }

        public void Assert(Func<int[], int[]> Method, int[] parameter, int[] expected)
        {
            int[] res = Method.Invoke(parameter);

            if (res.SequenceEqual(expected))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Test: ОК\nExpected: {string.Join(", ", expected)}\nGot: {string.Join(", ", res)}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Test: NOT ОК\nExpected: {string.Join(", ", expected)}\nGot: {string.Join(", ", res)}");
                Console.ResetColor();
            }
        }

        // Doesn't work with arrays
        public void Assert(Func<InVal, ReturnVal> Method, InVal parameter, ReturnVal expected)
        {
            ReturnVal res = Method.Invoke(parameter);

            if (res.Equals(expected))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Test: NOT OK\nExpected: { expected}\nGot: {res}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Test: NOT OK\nExpected: { expected}\nGot: {res}");
                Console.ResetColor();
            }
        }

        public void Assert<T>(Func<InVal, T, T, ReturnVal> Method,
            InVal firstParam, T secondParam, T thirdParam, ReturnVal expected)
        {
            ReturnVal res = Method.Invoke(firstParam, secondParam, thirdParam);

            if (res.Equals(expected))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Test: OK\nExpected: { expected}\nGot: {res}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Test: NOT OK\nExpected: { expected}\nGot: {res}");
                Console.ResetColor();
            }
        }

        private void Log<T>(long ms, T[] parameter)
        {
            Console.WriteLine($"Parameter: {parameter.Length}; Number of tests: {Tries}; Average completion time: {ms} ms");
        }

        // Doesn't work with arrays
        private void Log(long ms, InVal parameter)
        {
            Console.WriteLine($"Parameter: {parameter}); Number of tests: {Tries}; Average completion time: {ms} ms");
        }

    }
}