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
        private T[] collection;
        private int capacity, count=0;


        public AnimalShelter(int capacity)
        {
            this.capacity = capacity;
            collection = new T[capacity];
        }

        public T Add(T input)
        {
            if (count + 1 > capacity) return null;

            collection[count++] = input;
            return input;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                //"if collection is not null"-logic, add here
                yield return collection[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
