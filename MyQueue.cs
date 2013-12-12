using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Stack
{
    class MyQueue<T>
    {
        Stack<T> stackNewest, stackOldest;
        public MyQueue()
        {
            stackNewest = new Stack<T>();
            stackOldest = new Stack<T>();
        }
        public int size()
        {
            return stackNewest.Count() + stackOldest.Count();
        }
        public void add(T value)
        {
            /* Push onto stackNewest., which always has the newest
            * elements on top */
            stackNewest.Push(value);
        }
        /* Move elements from stackNewest into stackOldest. This is
        * usually done so that we can do operations on stackOldest. */
        private void shiftStacks()
        {
            if (stackOldest == null)
            {
                while (stackNewest != null)
                {
                    stackOldest.Push(stackNewest.Pop());
                }
            }
        }

        public T peek()
        {
            shiftStacks(); // Ensure stackOldest has the current elements
            return stackOldest.Peek();
        }// retrieve the oldest item.
        public T remove()
        {
            shiftStacks(); // Ensure stackOldest has the current elements
            return stackOldest.Pop(); // pop the oldest item.

        }
    }
}
