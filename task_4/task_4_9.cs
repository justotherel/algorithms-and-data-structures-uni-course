
using System.Text;

namespace Ushakov_4_9
{
    public class Node
    {
        public object data { get; set; }
        public Node next { get; set; }
        public Node prev { get; set; }

    };

    public class DLCList
    {
        private Node head;
        public int Count { get; set; }

        public Node First, Last;

        public DLCList()
        {
            head = null;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public void AddFirst(object obj)
        {
            if (IsEmpty())
            {
                head = new Node
                {
                    data = obj,
                    prev = null,
                    next = null
                };
                First = head;
                Last = head;
                Count++;
                return;
            }

            if (head.next == null)
            {
                Node newNode = new Node
                {
                    data = obj,
                    next = null,
                    prev = null,

                };

                Node temp = head;
                head = newNode; // now newNode
                newNode = temp; //now head

                newNode.next = head;
                newNode.prev = head;

                head.next = newNode;
                head.prev = newNode;

                First = head;
                Count++;
            }
            else
            {

                Node newNode = new Node
                {
                    data = obj,
                    next = head,
                    prev = Last,

                };

                Node temp = head;
                head = newNode; // now newNode
                newNode = temp; //now head

                newNode.prev = Last;
                Last.next = head;
                First = head;


                Count++;


                /* Node temp = head;
                 head = newNode;
                 newNode = temp;

                 head.prev.next = newNode;
                 head.prev = newNode;
                 Last = newNode;
                 Count++;*/
            }
        }

        public void AddLast(object obj)
        {
            if (IsEmpty())
            {
                head = new Node
                {
                    data = obj,
                    prev = null,
                    next = null
                };
                First = head;
                Last = head;
                Count++;
                return;
            }

            if(head.next == null)
            {
                Node newNode = new Node
                {
                    data = obj,
                    next = head,
                    prev = head,

                };

                Last = newNode;
                head.next = newNode;
                head.prev = newNode;
                Count++;

            } else
            {
                
                Node newNode = new Node
                {
                    data = obj,
                    next = head,
                    prev = head.prev,

                };

                head.prev.next = newNode;
                head.prev = newNode;
                Last = newNode;
                Count++;
            }
            
        }

        public object Find(object obj)
        {
            Node current = head;
            if (current.data.Equals(obj)) return head.data;
            current = head.next;

            while (current != head)
            {
                if (current.data.Equals(obj)) return current.data;
                current = current.next;
            }

            return null;
        }

        public bool Remove(object obj)
        {
            Node current = head;

            if (current.data.Equals(obj) && (current.next == current || current.next == null))
            {
                head = null;
                Count--;
                return true;
                
            }

            if (current.data.Equals(obj))
            {

                current.data = current.next.data;
                current.next = current.next.next;
                current.next.next.prev = current;

                Count--;
                /*current.prev.next = current.next;
                current.next.prev = current.prev;*/

                return true;
            }

            current = head.next;

            while (current != head)
            {
                if (current.data.Equals(obj))
                {
                    current.prev.next = current.next;
                    current.next.prev = current.prev;
                    Count--;
                    return true;
                }
                current = current.next;
            }

            return false;
        }

        public bool Clear()
        {
            while(head != null)
            {
                Remove(head.data);
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (head == null) return "";

            sb.Append($"{head.data} ");
            if (head.next == null) return sb.ToString();

            Node current = head.next;

            while (current != head)
            {
                sb.Append($"{current.data} ");
                current = current.next;
            }

            return sb.ToString();
        }

    }
}


