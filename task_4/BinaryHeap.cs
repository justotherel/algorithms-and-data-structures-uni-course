using System;
using System.Collections.Generic;
using System.Linq;

namespace Ushakov_4_11
{
    internal class BinaryHeap
    {
        List<int> Heap = new List<int>();

        public void Insert(int value)
        {
            Heap.Add(value);
            ShiftUp(Heap.Count - 1);
        }

        public static IList<T> Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }

        public int ExtractMax()
        {
            int max = Heap[0];
            Heap[0] = -1;
            ShiftDown(0);
            return max;
        }

        void ShiftUp(int index)
        {
            int parent = (index - 1) / 2;
            if (Heap[parent] < Heap[index])
            {
                Swap(Heap, parent, index);
                ShiftUp(parent);
            }
        }

        void ShiftDown(int index)
        {
            int child1 = 2 * index + 1, child2 = 2 * index + 2;
            int size = Heap.Count, max = 0;
            if (child1 < size && child2 >= size)
            {
                if (Heap[index] < Heap[child1])
                {
                    Swap(Heap, index, child1);
                    ShiftDown(child1);
                }
            }
            else if (child2 < size)
            {
                if (Heap[child1] < Heap[child2]) max = child2;
                else max = child1;
                if (Heap[index] < Heap[max])
                {
                    Swap(Heap, index, max);
                    ShiftDown(max);
                }
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Heap);
        }

        public override bool Equals(object obj)
        {
            return obj is BinaryHeap heap && Heap.Sequencx  eEqual(heap.Heap);
        }
    }

}
