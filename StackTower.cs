using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Stack
{
    class StackTower : Stack<int>
    {

        private Stack<int> disks;
        private int index;
        public StackTower(int i)
        {
            disks = new Stack<int>();
            index = i;
        }

        public int Index
        {
            get
            {
                return index;
            }

        }


        public void add(int d)
        {
            if (disks.Count != 0)
            {

                if (disks.Peek() <= d)
                {
                    throw new Exception("Error placing disk " + d);
                }
                else
                {
                    disks.Push(d);
                }

            }
            else
            {
                disks.Push(d);
            }
        }

        private void moveTopTo(StackTower t)
        {
            int top = disks.Pop();
            t.add(top);
            Console.Out.WriteLine("Move disk " + top + " from " + Index +
            " to " + t.Index);
        }

        public void moveDisks(int n, StackTower destination, StackTower buffer)
        {
            if (n > 0)
            {
                moveDisks(n - 1, buffer, destination);
                moveTopTo(destination);
                buffer.moveDisks(n - 1, destination, this);
            }
        }

    }
}
