using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearMe
{
    public class Node
    {
        public Node Next;
        public Friend Value;

        public Node()
        {
            Value = new Friend();
        }
    }

    class LinkedList
    {
        public Node head;
        public Node current;//This will have latest node
        public int Count;

        public LinkedList()
        {
            head = new Node();
            current = head;
        }

        public void Append(Friend data)
        {
            Node newNode = new Node();
            newNode.Value = data;
            current.Next = newNode;
            current = newNode;
            Count++;
        }
    }
}
