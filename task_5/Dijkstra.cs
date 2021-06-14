using System;
using System.Collections.Generic;
using System.Text;

namespace Ushakov_4_11
{
    public class Dijkstra
    {
        private int nVertices;
        private int nUnvisited;
        private List<Node> AdjacencyMatrix;
        private List<Vertex> Verteces;
        

        public Dijkstra(int vertices, List<Node> matrix)
        {

            //Messy
            nVertices = vertices;
            AdjacencyMatrix = new List<Node>(vertices * vertices);

            foreach (Node n in matrix)
                AdjacencyMatrix.Add(n);

            Verteces = new List<Vertex>();
            for (int i = 0; i < nVertices; i++)
            {
                char c = 'A';
                c = (char)(c + i);
                Verteces.Add(new Vertex(Char.ToString(c), false));
            }

            foreach (var v in Verteces)
            {
                SetNeighbors(v);
            }
        }
        
        public string FindShortestPath(String start, String finish)
        {
            return FindShortestPath(Verteces.Find(v => v.Name == start),
                                    Verteces.Find(v => v.Name == finish));
        }
        public string FindShortestPath(Vertex start, Vertex finish)
        {
            //Inicialization
            Vertex first = Verteces.Find(v => v.Name == start.Name);
            first.Distance = 0;
            nUnvisited = Verteces.Count;

            while(true)
            {
                Vertex currnet = GetMinDistVertex();
                if (currnet == null) break;
                SetDistToNeighbors(currnet);
            }

            return GetPath(start, finish);
        }

        private void SetDistToNeighbors(Vertex vertex)
        {
            vertex.isVisited = true;
            nUnvisited--;

            foreach (var n in vertex.neighbors)
            {
                int nIndex = Verteces.IndexOf(n);
                int cIndex = Verteces.IndexOf(vertex);
                int AMIndex = cIndex * nVertices + nIndex;

                double distance = vertex.Distance + AdjacencyMatrix[AMIndex].weight;

                if (distance < n.Distance)
                {
                    n.Distance = distance;
                    n.Previous = vertex;
                }
            }
        }

        private Vertex GetMinDistVertex()
        {
            double minVal = double.MaxValue;
            Vertex res = null;

            foreach (Vertex v in Verteces) {
                if (nUnvisited != 0 && v.isVisited == false && v.Distance < minVal)
                {
                    res = v;
                    minVal = v.Distance;
                }
            }
            return res;
        }

        public void SetNeighbors(Vertex vertex)
        {
            int index = Verteces.IndexOf(vertex);

            int start = index * nVertices;
            int finish = (index + 1) * nVertices;

            for (int i = start; i < finish; i++)
            {
                if (AdjacencyMatrix[i].weight != -1)
                    vertex.neighbors.Add(Verteces[i - nVertices*index]);
            }
        }

        private string GetPath(Vertex start, Vertex finish)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{finish.Name} -> ");

            while (start != finish)
            {
                finish = finish.Previous;
                if (finish == start)
                    sb.Append(finish.Name);
                else 
                    sb.Append($"{finish.Name} -> "); ;
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            int newLineCount = 0;

            for (int i = 0; i < AdjacencyMatrix.Count; i++)
            {
                if (newLineCount == nVertices)
                {
                    newLineCount = 0;
                    sb.Append("\n");
                }
                if (i != AdjacencyMatrix.Count - 1)
                    sb.Append($"{AdjacencyMatrix[i].weight,3}, ");
                else
                    sb.Append($"{AdjacencyMatrix[i].weight,3}");
                newLineCount++;
            }
            sb.Append("]");

            return sb.ToString();
        }
    }

    public class Vertex
    {
        public string Name;
        public bool isVisited = false;
        public double Distance;
        public List<Vertex> neighbors;
        public Vertex Previous;

        public Vertex(string Name, bool isVisited, double distance = double.MaxValue)
        {
            this.Name = Name;
            this.isVisited = isVisited;
            Distance = distance;
            neighbors = new List<Vertex>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return (obj is Vertex v && v.Name == this.Name);
            
        }

        public string getNeighbors()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var v in neighbors)
            {
                sb.Append($"{v.Name}, ");
            }
            return sb.ToString();
        }
    }

    public class Node {
        public int i, j;
        public double weight;

        public Node(int i, int j, double weight)
        {
            this.i = i;
            this.j = j;
            this.weight = weight;
        }
    }
}
