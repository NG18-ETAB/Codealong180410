using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codealong180410
{
    public class Animal
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int NrOfLegs { get; set; }
        public bool IsTame { get; set; }

        public Animal(int age, string name, string species, int nrOfLegs, bool isTame)
        {
            Age = age;
            Name = name;
            Species = species;
            NrOfLegs = nrOfLegs;
            IsTame = isTame;
        }
    }
}
