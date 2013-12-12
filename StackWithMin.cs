using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Stack
{
    // How would you design a stack which, in addition to push and pop, also has a
    // function min which returns the minimum element? Push, pop and min should all
    // operate in 0(1) time.
    class StackWithMin : Stack<NodeWithMin>
    {
        public void push(int value)
        {
            int newMin = Math.Min(value, min());
            base.Push(new NodeWithMin(value, newMin));
        }

        public int min()
        {
            if (this.Count == 0)
            {
                return int.MaxValue; // Error value
            }
            else
            {
                return Peek().min;
            }
        }


    }

    class NodeWithMin
    {
        public int value;
        public int min;
        public NodeWithMin(int v, int min)
        {
            value = v;
            this.min = min;
        }
    }

    // solution two 
    // this solution has a issue when the min number was poped. 

    class StackWithMinTwo :Stack<int> 
    {
       Stack<int> minStack;
       public StackWithMinTwo()
       {
           minStack = new Stack<int>();
       }

       public void push(int value)
       {
           
               if (value < Min())
               {
                   minStack.Push(value);
               }
           
           base.Push(value); 
       }
        // will exist a issue when after min number was poped 
        public int pop()
        {
            int value = base.Pop();
            if( value == Min())
            {
                  minStack.Pop();
            }
            return value; 
        }        
     


       public int Min()
       {
           if (minStack == null)
           {
               return int.MaxValue;
           }
           else {

               return minStack.Peek();    
           }
       }


       }
    
       
    
    }

