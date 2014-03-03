using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Stack
{
    #region 3.4
    /*
In the classic problem of the Towers of Hanoi, you have 3 towers and N disks of
different sizes which can slide onto any tower. The puzzle starts with disks sorted
in ascending order of size from top to bottom (i.e., each disk sits on top of an even
larger one). You have the following constraints:
(T) Only one disk can be moved at a time.
(2) A disk is slid off the top of one tower onto the next rod.
(3) A disk can only be placed on top of a larger disk.
Write a program to move the disks from the first tower to the last using Stacks.
*/
    
//public static void main(String[] args) {
// int n = 3;
// Towerf] towers = new Tower[n];
// for (int i = 0; i < 3; i++) {
// towers[i] = new Tower(i);
// }
//for (int i = n - 1; i >= 0; i--) {
// towers[0].add(i);
// }
// towers[0].moveDisks(n, towers[2], towers[l]);
// }


  public  class StackTower : Stack<int>
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



        #endregion

    }
}
