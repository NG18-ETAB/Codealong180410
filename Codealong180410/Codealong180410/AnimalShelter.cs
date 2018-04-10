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
        public void leave(T input)
        {
            if (cap-1 > count )
            {
                collection[count++] = input;
                //or     count +=1; instead of count++
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
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
