using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLevel
{
    /// <summary>
    /// A queue structure which places the "highest priority" items at the front.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> : List<T>
    {
        Comparison<T> prioritySort;
        bool elementAdded = false;
        bool sortFunctionSet = false;
        public PriorityQueue()
        {

        }
        public PriorityQueue(Comparison<T> prioritysort)
        {
            prioritySort = prioritysort;
            sortFunctionSet = true;
        }
        public T Dequeue()
        {
            T val = Top();
            RemoveAt(0);
            return val;
        }
        public void Enqueue(T val)
        {
            Add(val);
            elementAdded = true;
        }
        public T Top()
        {
            if (elementAdded && sortFunctionSet)
            {
                Sort(prioritySort);
                elementAdded = false;
            }
            return this[0];
        }
    }
}