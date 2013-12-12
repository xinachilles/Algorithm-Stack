using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm_Stack
{
    public class Node<T>
    {
        public Node<T> above;
        public Node<T> below;
        public T value;

        public Node(T n)
        {
            value = n;
        }
    }
    // Describe how you could use a single array to implement three stacks
    public class MyStack<T>
    {


        static int stackSize = 100;
        T[] buffer = new T[stackSize * 3];
        int[] stackPointer = { -1, -1, -1 }; // pointers to track top element
        public Node<T> top, bottom;
        public int size = 0;
        private int capacity;

        public void Push(int stackNum, T value)
        {
            /* Check if we have space */
            if (stackPointer[stackNum] + 1 >= stackSize)
            { // Last element
                throw new Exception("Out of space.");
            }
            /* Increment stack pointer and then update top value */
            stackPointer[stackNum]++;
            buffer[absTopOfStack(stackNum)] = value;
        }

        public T Pop(int stackNum)
        {
            if (stackPointer[stackNum] == -1)
            {
                throw new Exception("Trying to pop an empty stack.");
            }
            T value = buffer[absTopOfStack(stackNum)]; // Get top
            buffer[absTopOfStack(stackNum)] = default(T); // Clear index
            stackPointer[stackNum]--; // Decrement pointer
            return value;
        }

        public T Peek(int stackNum)
        {
            int index = absTopOfStack(stackNum);
            return buffer[index];
        }

        bool IsEmpty(int stackNum)
        {
            return stackPointer[stackNum] == -1;
        }

        /* returns index of top of stack "stackNum" in absolute terms */
        private int absTopOfStack(int stackNum)
        {
            return stackNum * stackSize + stackPointer[stackNum];
        }


        public MyStack(int capacity) { this.capacity = capacity; }



        public void Join(Node<T> above, Node<T> below)
        {
            if (below != null) below.above = above;
            if (above != null) above.below = below;
        }

        public bool Push(T v)
        {
            if (size >= capacity)
                return false;
            size++;
            Node<T> n = new Node<T>(v);
            if (size == 1) bottom = n;
            Join(n, top);
            top = n;
            return true;
        }

        public T Pop()
        {
            Node<T> t = top;
            top = top.below;
            size--;
            return t.value;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public bool IsFull()
        {
            if (size >= capacity) { return true; }
            else { return false; }
        }

        public T RemoveBottom()
        {
            Node<T> b = bottom;
            bottom = bottom.above;
            if (bottom != null) bottom.below = null;
            size--;
            return b.value;

        }



        //Write a program to sort a stack in ascending order (with biggest items on top).
        //You may use at most one additional stack to hold items, but you may not copy the
        //elements into any other data structure (such as an array). The stack supports the
        //following operations: push, pop, peek, and isEmpty.

        public static Stack<int> Sort(Stack<int> s)
        {
            Stack<int> r = new Stack<int>();
            while (s.Count > 0)
            {
                int tmp = s.Pop(); // Step 1
                while (r.Count > 0)
                { // Step 2
                    if (r.Peek() >= tmp) break;
                    s.Push(r.Pop());
                }

                r.Push(tmp); // Step 3
            }
            return r;
        }
    }
}
