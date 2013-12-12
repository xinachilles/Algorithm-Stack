using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

#region 3.3
//Imagine a (literal) stack of plates. If the stack gets too high, it migh t topple. Therefore,
//in real life, we would likely start a new stack when the previous stack exceeds some
//threshold. Implement a data structure SetOfStacks that mimics this. SetOf-
//Stacks should be composed of several stacks and should create a new stack once
//the previous one exceeds capacity. SetOfStacks.push() and SetOfStacks.
//pop () should behave identically to a single stack (that is, pop () should return the
//same values as it would if there were just a single stack).

//FOLLOW UP
//Implement a function popAt(int index) which performs a pop operation on a
//specific sub-stack.
namespace ClassLibrary.Stack
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
   
    public class StackWithCapacity<T> :List<Node<T>>
    {

        
        public Node<T> top, bottom;
        private int capacity;

        public StackWithCapacity(int capacity) { this.capacity = capacity; }



        public void Join(Node<T> above, Node<T> below)
        {
            if (below != null) below.above = above;
            if (above != null) above.below = below;
        }

        public void Push(T v)
        {
            if (base.Count < capacity)
            {

                Node<T> n = new Node<T>(v);
                if (base.Count == 1) bottom = n;
                Join(n, top);
                top = n;
                base.Add(n);
            }
        }

        public T Pop()
        {
            Node<T> t = top;
            if (top != null)
            {
                top = top.below;
                base.RemoveAt(Count - 1);
            }
                return t.value;
            
        }

        public bool IsEmpty()
        {
            return base.Count == 0;
        }

        public bool IsFull()
        {
            if (base.Count >= capacity) { return true; }
            else { return false; }
        }

        public T RemoveBottom()
        {
            Node<T> b = bottom;
            bottom = bottom.above;
            if (bottom != null) bottom.below = null;
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

    class SetOfStacks<T>
    {

        List<StackWithCapacity<T>> stacks = new List<StackWithCapacity<T>>();
        private int capacity;


        public SetOfStacks(int c)
        {
            capacity = c;
        }

        public void Push(T v)
        {

            StackWithCapacity<T> last = GetLastStack();
            if (last != null)
            { // add to last stack
                if (last.IsFull() != true)
                {
                    last.Push(v);
                }
                else
                {

                    StackWithCapacity<T> stack = new StackWithCapacity<T>(capacity);
                    stack.Push(v);
                    stacks.Add(stack);

                }

            }
            else
            {
                StackWithCapacity<T> stack = new StackWithCapacity<T>(capacity);
                stack.Push(v);
                stacks.Add(stack);
            }



        }
        public T Pop()
        {
            StackWithCapacity<T> last = GetLastStack();
            T v = default(T);
            if (last != null)
            {
                v = last.Pop();
                if (last.Count  == 0) stacks.RemoveAt(stacks.Capacity - 1);
            }
            return v;
        }

        //public bool IsEmpty()
        //{
        //    MyStack<T> last = GetLastStack();
        //    return (last == null);
        //}

        public StackWithCapacity<T> GetLastStack()
        {
            if (stacks.Count == 0) return null;
            return stacks[stacks.Count - 1];
        }

        public T PopAt(int index)
        {
            return leftShift(index, true);

        }


        public T leftShift(int index, bool removeTop)
        {
            StackWithCapacity<T> stack = stacks[index];
            T removed_item;
            if (removeTop) removed_item = stack.Pop();
            else removed_item = stack.RemoveBottom();

            if (stack.IsEmpty())
            {
                stacks.RemoveAt(index);
            }
            else if (stacks.Count  > index + 1)
            {
                T v = leftShift(index + 1, false);
                stack.Push(v);
            }
            return removed_item;
        }

    }
}
#endregion 3.3