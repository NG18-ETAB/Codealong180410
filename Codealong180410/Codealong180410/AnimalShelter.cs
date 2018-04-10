using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codealong180410
{
    public class AnimalShelter<T> : IEnumerable<T> where T : Animal
    {

        T[] collection;
        int cap, count;
        public AnimalShelter(int capacity)
        {
            cap = capacity;
            count = 0;
            collection = new T[capacity];
        }

        public void Leave(T input)
        {
            if(cap > count)
            {
                collection[count] = input;
                count += 1;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i=0; i<count; i++)
            {
                yield return collection[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
