using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codealong180410
{
    public class UI
    {
        public void MainMenu()
        {
            bool keepRunning = true;

            Console.WriteLine("Welcome!");
            while (keepRunning)
            {
                Console.WriteLine("\nEnter number for choice");
                Console.WriteLine("1: Example");
                Console.WriteLine("0: Quit");

                string input = Console.ReadKey().KeyChar.ToString();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("1: Example");
                        Example();
                        break;

                    //case "":
                    //    break;

                    //case "":
                    //    break;

                    case "0":
                    case "Q":
                    case "q":
                        keepRunning = false;
                        break;

                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }
            }
        }

        public void Example()
        {
            AnimalShelter<Animal> theShelter = new AnimalShelter<Animal>(10)
            {
                new Animal(10, "Bob", "Dog", 3, true),
                new Animal(2, "Billy", "Cat", 4, true),
                new Animal(45, "Herr Nilsson", "Monkey", 2, false),
                new Animal(100, "Hati", "Elephant", 4, false),
                new Animal(50, "Trump", "Orange", 2, false)
            };

            foreach (var a in theShelter)
            {
                Console.WriteLine($"{a.Name} is {a.Age} years old and has {a.NrOfLegs} legs, is a {a.Species} and is {(!a.IsTame ? "not " : "")}tame");
            }

            Console.WriteLine("\nIs Tame:");

            foreach (var a in theShelter.Where(x => x.IsTame == true))
            {
                Console.WriteLine($"{a.Name} is {a.Age} years old and has {a.NrOfLegs} legs, is a {a.Species} and is {(!a.IsTame ? "not " : "")}tame");
            }

            Console.WriteLine("\nIs >30yo or leg > 3:");

            foreach (var a in theShelter.Where(x => x.Age > 30 || x.NrOfLegs > 3))
            {
                Console.WriteLine($"{a.Name} is {a.Age} years old and has {a.NrOfLegs} legs, is a {a.Species} and is {(!a.IsTame ? "not " : "")}tame");
            }

            Console.WriteLine("\nOrdered:");

            foreach (var a in theShelter.OrderBy(x => x.Age).OrderBy(x => x.NrOfLegs).OrderBy(x => x.Name == "Trump" ? "Dump" : "0"))
            {
                Console.WriteLine($"{a.Name} is {a.Age} years old and has {a.NrOfLegs} legs, is a {a.Species} and is {(!a.IsTame ? "not " : "")}tame");
            }

            Console.WriteLine("\nSame but with function instead of lambda:");

            int keySelectorFuncLegs(Animal x) => x.NrOfLegs; // creats a function pointer of type Func<Animal,int> for the OrderBy that will order by the returning int.

            //Funcs below. Note the funcs don't have () cuz you send the "pointer" to a func, not running it yourself.
            foreach (var a in theShelter.OrderBy(KeySelectorFuncAge).OrderBy(keySelectorFuncLegs).OrderBy(KeySelectorFuncPutDrumpfLast))
            {
                Console.WriteLine($"{a.Name} is {a.Age} years old and has {a.NrOfLegs} legs, is a {a.Species} and is {(!a.IsTame ? "not " : "")}tame");
            }

            Console.ReadKey();
            Console.WriteLine("\nLINQ:");

            var filterdShelter = from a in theShelter
                                 where a.Age > 30
                                 orderby a.Name
                                 select a;


            foreach (Animal a in filterdShelter)
            {
                Console.WriteLine($"{a.Name} is {a.Age} years old and has {a.NrOfLegs} legs, is a {a.Species} and is {(!a.IsTame ? "not " : "")}tame");
            }

            Console.WriteLine("\nLINQ Join:");

            // Just a list of pops to use inner join on animals in theShelter
            var presidents = new List<string>()
            {
                "Billy",
                "Trump",
                "Herr Nilsson",
                "Obama"
            };

            //Take shelter and (inner) join all the list of presidents, matching on Animal.Name (if not found in either list, remove the entry)
            //Also create new Animals, copying from the original Animal but with a new Name derived from the list of presidents

            //Old, with an anonymous new type, in the select-clause, which can't be cast to Animal
            //var joinedShelter = from a in theShelter
            //                    join p in presidents on a.Name equals p
            //                    select new { Name = "Prez " + p, a.Age, a.NrOfLegs, a.Species, a.IsTame };

            //New, with a known type, in the select-clause, which can be cast back to Animal
            var joinedShelter = from a in theShelter
                                join p in presidents on a.Name equals p
                                select new Animal(a.Age, "Prez " + p, a.Species, a.NrOfLegs, a.IsTame);

            //comparison with lambda but with old style, anonymous type
            var jnSh = theShelter.Join(presidents, a => a.Name, p => p, (a, p) => new { Name = "Prez " + p, a.Age, a.NrOfLegs, a.Species, a.IsTame });

            //forcing type to Animal
            foreach (Animal a in joinedShelter)
            {
                Console.WriteLine($"{a.Name} is {a.Age} years old and has {a.NrOfLegs} legs, is a {a.Species} and is {(!a.IsTame ? "not " : "")}tame");
            }

            Console.ReadKey();

            Console.WriteLine("\nBig Test:");

            foreach (Animal a in theShelter)
            {
                Console.WriteLine($"{a.Name} is {a.Age} years old and has {a.NrOfLegs} legs, is a {a.Species} and is {(!a.IsTame ? "not " : "")}tame");
            }
            Console.WriteLine();

            IEnumerable<Animal> query = null;
            Animal result = null;
            try
            {
                int i = 5;
                result = theShelter.OrderBy(a => a.Age).First(a => a.Age > i);
                query = theShelter.OrderBy(a => a.Age).Where((a) => { return a.Age > i; });
                Console.WriteLine($"query{i}:{query.First().Name} is {query.First().Age} years old and has {query.First().NrOfLegs} legs, is a {query.First().Species} and is {(!query.First().IsTame ? "not " : "")}tame");
                i = 44;
                Console.WriteLine($"query{i}:{query.First().Name} is {query.First().Age} years old and has {query.First().NrOfLegs} legs, is a {query.First().Species} and is {(!query.First().IsTame ? "not " : "")}tame");
                i = 46;
                Console.WriteLine($"query{i}:{query.First().Name} is {query.First().Age} years old and has {query.First().NrOfLegs} legs, is a {query.First().Species} and is {(!query.First().IsTame ? "not " : "")}tame");
                i = 51;
                Console.WriteLine($"query{i}:{query.First().Name} is {query.First().Age} years old and has {query.First().NrOfLegs} legs, is a {query.First().Species} and is {(!query.First().IsTame ? "not " : "")}tame");

            }
            catch (Exception)
            {
                Console.WriteLine($"Error");
            }

            Console.WriteLine($"result:{result.Name} is {result.Age} years old and has {result.NrOfLegs} legs, is a {result.Species} and is {(!result.IsTame ? "not " : "")}tame");
            Console.WriteLine($"query:{query.First().Name} is {query.First().Age} years old and has {query.First().NrOfLegs} legs, is a {query.First().Species} and is {(!query.First().IsTame ? "not " : "")}tame");

            Console.WriteLine("\nReverse:");

            foreach (Animal a in theShelter.OrderByDescending(x => x.Name).ThenBy(x => x.NrOfLegs)) // not thenby descending because reasons
            {
                Console.WriteLine($"{a.Name} is {a.Age} years old and has {a.NrOfLegs} legs, is a {a.Species} and is {(!a.IsTame ? "not " : "")}tame");
            }
        }

        // Method using lambda. Method not used in code, just a test. Takes 2 int and returns the result of "is X bigger than Y". Note the lack of {} and a return statment, cuz lambda creates this from any single line code.
        public bool Laaaambert(int x, int y) => x > y;

        // Method using lambda
        public int KeySelectorFuncAge(Animal x) => x.Age;

        //Special method where every animal returns "0" except the baffon "Trump" that returns "Dump". As "0" preceeds "Dump", an OrderBy would sort Trump last.
        public string KeySelectorFuncPutDrumpfLast(Animal x)
        {
            if (x.Name == "Trump") return "Dump";
            return "0";
        }
    }
}
