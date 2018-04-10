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


            foreach (var a in filterdShelter)
            {
                Console.WriteLine($"{a.Name} is {a.Age} years old and has {a.NrOfLegs} legs, is a {a.Species} and is {(!a.IsTame ? "not " : "")}tame");
            }

            Console.WriteLine("\nLINQ Join:");

            var presidents = new List<string>()
            {
                "Billy",
                "Trump",
                "Herr Nilsson",
                "Obama"
            };
            var joinedShelter = from a in theShelter
                                join p in presidents on a.Name equals p
                                select new { Name = "Prez " + p, a.Age, a.NrOfLegs, a.Species, a.IsTame };


            foreach (var a in joinedShelter)
            {
                Console.WriteLine($"{a.Name} is {a.Age} years old and has {a.NrOfLegs} legs, is a {a.Species} and is {(!a.IsTame ? "not " : "")}tame");
            }
        }

        // Method using lambda. Not used in code but more of a test. Takes 2 int and returns the result of "is X bigger than Y". Note the lack of {} and a return statment, cuz lambda creates this from any single line code.
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
