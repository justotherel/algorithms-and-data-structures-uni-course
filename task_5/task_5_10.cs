// File contains methods for testing Dijkstra algorithm

using System;
using System.Collections.Generic;
using System.IO;

namespace Ushakov_4_11
{ 
    class Program
    {
       
        // Uses Adjacency matrix from file Example.txt
        // Finds shortest route between two verteces
        // Takes Number of veteces, name of starting vertex and name of finishing vertex
        // Verteces names are capitalized letters [A, B, C..]
        // Using graphs with more than 28 verceses will resoult in non-letter verteces Names
        public static void RunTest(string FileName, string start, string end)
        {
            List<Node> AM = GetAMfromFile(FileName);

            if (AM == null)
            {
                Console.WriteLine("Invalid inpup in TXT file");
                return;
            }

            //Not the best emplementation 
            double sqrt = Math.Sqrt(AM.Count);
            int n = (int)sqrt;

            if (n * n != sqrt * sqrt)
            {
                Console.WriteLine("Invalid inpup in TXT file");
                return;
            }

            Dijkstra dAlg = new Dijkstra(n, AM);
            Console.WriteLine($"Using graph from file {FileName}");
            Console.WriteLine($"Adjacency matrix for a given graph with {n} edges:\n");
            Console.WriteLine(dAlg.ToString() + "\n");
            Console.WriteLine($"Finding the shortest rout from {start} to {end}\n");
            Console.WriteLine(dAlg.FindShortestPath(start, end));
        }

        // Finds shortest route between two verteces
        // Takes Number of veteces, name of starting vertex and name of finishing vertex 
        // Using N > 28 will lead to non-letter verteces names starting from 29th vertex
        public static void RunTest(int N, string start, string end)
        {
            List<Node> AM  = GenerateGraph(N);
            Dijkstra dAlg = new Dijkstra(N, AM);

            Console.WriteLine("Using auto-generated graph");
            Console.WriteLine($"Adjacency matrix for a generated graph with {N} edges:\n");
            Console.WriteLine(dAlg.ToString() + "\n");
            Console.WriteLine($"Finding the shortest rout from {start} to {end}\n");
            Console.WriteLine(dAlg.FindShortestPath(start, end));
        }

        // Finds shortset route from vertex A (first vertex) to random vertex  
        // Using N > 28 will lead   to non-letter verteces names starting from 29th vertex
        public static void RunTest(int N)
        {
            Random random = new Random();

            char start = 'A';
            char finish = start; 
            finish += (char)random.Next(65, 65 + N);

            List<Node> AM = new List<Node>();
            AM = GenerateGraph(N);
            Dijkstra dAlg = new Dijkstra(N, AM);

            Console.WriteLine("Using auto-generated graph");
            Console.WriteLine($"Adjacency matrix for a generated graph with {N} edges:\n");
            Console.WriteLine(dAlg.ToString() + "\n");
            Console.WriteLine($"Finding the shortest rout from {start} to {finish}\n");
            Console.WriteLine(dAlg.FindShortestPath(Char.ToString(start), Char.ToString(finish)));
        }

        // Generates a weighted[1 to 20] graph with N-1 edges,
        // Uses Fisher-Yates shuffle (to garantee connectivity)
        // Adds random edges after, results in up to N-1 + N/2 edges
        public static List<Node> GenerateGraph(int N)
        {
            int[] verteces = new int[N];
            for (int i = 0; i < N; i++)
            {
                verteces[i] = i;
            }
            verteces = Shuffle(verteces);
            Random random = new Random();
            double[] ArrAM = new double[N * N];
            Array.Fill(ArrAM, -1);

            List<Node> AM = new List<Node>();

            for (int k = 0; k < N; k++)
            {
                int t = k * (N + 1);
                ArrAM[k * (N + 1)] = 0;
            }

            for (int i = 0; i < N - 1; i++)
            {
                int index1 = verteces[i];
                int index2 = verteces[i + 1];
                int AMIndex = index1 * N + index2;

                double weight = random.Next(1, 20);

                if (ArrAM[AMIndex] == -1)
                {
                    ArrAM[AMIndex] = weight;
                } else
                {
                    Console.WriteLine($"Error, tried to assign val to already assigned variable at index {AMIndex}, index1 {index1}, index2 {index2}");
                }

                AMIndex = index2 * N + index1;

                if (ArrAM[AMIndex] == -1)
                {
                    ArrAM[AMIndex] = weight;
                }
                else
                {
                    Console.WriteLine($"Error, tried to assign val to already assigned variable at index {AMIndex}, index1 {index1}, index2 {index2}");
                }
            }

            int E = N / 2 - 1;

            for (int i = 0; i < E; i++)
            {
                int index1 = random.Next(0, N - 1);
                int index2 = random.Next(0, N - 1);
                int AMIndex1 = index1 * N + index2;
                int AMIndex2 = index2 * N + index1;
                double weight = Random(1, 20);

                while (index1 == index2)
                {
                    index1 = Random(0, N - 1);
                }

                if (ArrAM[AMIndex1] == -1 && ArrAM[AMIndex2] == -1)
                {
                    ArrAM[AMIndex1] = weight;
                    ArrAM[AMIndex2] = weight;
                }

            }

            for (int i = 0; i < ArrAM.Length; i++)
            {
                AM.Add(new Node(0, 0, ArrAM[i]));
            }

            return AM;
        }

        public static int Random(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        //Fisher-Yates shuffle
        public static int[] Shuffle(int[] verteces)
        {
            Random random = new Random();
            for (int i = verteces.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i);
                int temp = verteces[i];
                verteces[i] = verteces[j];
                verteces[j] = temp;
            }

            return verteces;
        }

        //Very messy code
        public static List<Node> GetAMfromFile (string fileName)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            var path = Path.Combine(projectDirectory, fileName);

            string[] lines = System.IO.File.ReadAllLines(path);
            List<Node> res = new List<Node>();
            string[] tokens;

            tokens = lines[0].Split(",");
            double[] line = { };
            int lineLen;

            try
            {
                line = Array.ConvertAll(tokens, s => double.Parse(s));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (line.Length != lines.Length) return null;

            foreach (double d in line)
            {
                res.Add(new Node(0, 0, d));
            }

            lineLen  = line.Length;

            for (int i = 1; i < lines.Length; i++)
            {
                tokens = lines[i].Split(",");
                try
                {
                    line = Array.ConvertAll(tokens, s => double.Parse(s));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                if (lineLen != line.Length) return null;
                
                foreach (double d in line)
                {
                    res.Add(new Node(0, 0, d));
                }
            }
            
            return res;
        }

        static void Main(string[] args)
        {
            //RunTest();
            RunTest("Example.txt", "A", "E");
            Console.WriteLine("");
            RunTest(20, "A", "F");
        }
    }
}
