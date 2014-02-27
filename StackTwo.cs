using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Stack
{

    #region 3.1 Solution two
    /* StackData is a simple class that holds a set of data about each
   / * stack. It does not hold the actual items in the stack. */
    public class StackData
    {
        public int start;
        public int pointer;
        public int size;
        public int capacity;
        public StackData(int _start, int _capacity)
        {
            start = _start;
            pointer = _start - 1;
            capacity = _capacity;
            size = 0;
        }

        public bool IsWithinStack(int index, int total_size)
        {
            /* Note: if stack wraps, the head (right side) wraps around
           / * to the left. */
            if (start <= index && index < start + capacity)
            {
                // non-wrapping, or "head" (right side) of wrapping case
                return true;
            }
            else if ((start + capacity) > total_size && index < (start + capacity) % total_size)
            {
                // tail (left side) of wrapping case
                return true;
            }
            return false;
        }
    }


    public class StackTwo
    {
        static int number_of_stacks = 3;

        public  int NumberOfStacks
        {
            get { return number_of_stacks; }
           // set { StackTwo.number_of_stacks = value; }
        }
         static int default_size = 1;
         static int total_size = default_size * number_of_stacks;
         StackData[] stacks = { new StackData(0, default_size), new StackData(default_size, default_size), new StackData(default_size * 2, default_size) };
         int[] buffer = new int[total_size];
        // there are three stacks 
        
  
        public  int NumberOfElements()
        {
            return stacks[0].size + stacks[1].size + stacks[2].size;
        }

        private int NextElement(int index)
        {
            if (index + 1 == total_size) return 0;
            else return index + 1;
        }

        private  static int PreviousElement(int index)
        {
            if (index == 0) return total_size - 1;
            else return index - 1;
        }

        private  void Shift(int stackNum)
        {
             StackData stack = stacks[stackNum];
            // increase the capacity 
            if (stack.size >= stack.capacity)
            {
                int nextStack = (stackNum + 1) % number_of_stacks;
                Shift(nextStack); // make some room
                stack.capacity++;
            }

            // Shift elements in reverse order

            for (int i = (stack.start + stack.capacity - 1) % total_size; stack.IsWithinStack(i, total_size); i = PreviousElement(i))
            {
                buffer[i] = buffer[PreviousElement(i)];
            }

            buffer[stack.start] = 0;
            stack.start = NextElement(stack.start); // move stack start
            stack.pointer = NextElement(stack.pointer); // move pointer
            stack.capacity--; // return capacity to original
        }

        /* Expand stack by shifting over other stacks */
        private void Expand(int stackNum)
        {
            Shift((stackNum + 1) % number_of_stacks);
            stacks[stackNum].capacity++;
        }
        public void Push(int stackNum, int value)
        {
            StackData stack = stacks[stackNum];
            /* Check that we have space */
            if (stack.size >= stack.capacity)
            {
                if (NumberOfElements() >= total_size)
                { // Totally full
                    throw new Exception("Out of space.");
                }
                else
                { // just need to shift things around
                    Expand(stackNum);
                }
            }
                /* Find the index of the top element in the array + 1,
                /* and increment the stack pointer */
                stack.size++;
                stack.pointer = NextElement(stack.pointer);
                buffer[stack.pointer] = value;
            
        }
      
                   
    
        public int Pop(int stackNum)
        {
            StackData stack = stacks[stackNum];
            if (stack.size == 0)
            {
                throw new Exception("Trying to pop an empty stack.");
            }
            int value = buffer[stack.pointer];
            buffer[stack.pointer] = 0;
            stack.pointer = PreviousElement(stack.pointer);
            stack.size--;
            return value;
        }

        public int Peek(int stackNum)
        {
            StackData stack = stacks[stackNum];
            return buffer[stack.pointer];
        }

        public bool IsEmpty(int stackNum)
        {
            StackData stack = stacks[stackNum];
            return stack.size == 0;
        }

        
    }
}
    #endregion 
