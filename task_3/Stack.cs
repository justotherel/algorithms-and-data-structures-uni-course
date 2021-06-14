using System;
using System.Text;

namespace Ushakov_3_5
{
    internal class Stack
    {
        readonly int MAX = int.MaxValue;
        int top;
        int[] stack;

        internal bool IsEmpty()
        {
            return (top < 0);
        }
        public Stack(int N = 1000)
        {
            stack = new int[N];
            top = -1;
        }
        internal bool Push(int data)
        {
            if (top >= MAX)
            {
                Console.WriteLine("Stack Overflow");
                return false;
            }
            else
            {
                stack[++top] = data;
                return true;
            }
        }

        internal int Pop()
        {
            if (top < 0)
            {
                Console.WriteLine("Stack Underflow");
                return 0;
            }
            else
            {
                int value = stack[top--];
                return value;
            }
        }

        internal int Peek()
        {
            if (top < 0)
            {
                Console.WriteLine("Stack Underflow");
                return 0;
            }
            else
            {
                return stack[top];
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = top; i >= 0; i--)
            {
                sb.Append(stack[i] + " ");   
            }

            return sb.ToString() ;
        }
    }
}
