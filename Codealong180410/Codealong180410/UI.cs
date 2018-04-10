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
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("Enter numver for choice ");
                Console.WriteLine("1) Example");
                Console.WriteLine("0) Quit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Example();
                        break;
                    case "0":
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }
                Console.Write("\n<Press any Button To continue>");
                Console.ReadKey();

            }
        }
        public void Example()
        {
            AnimalShelter<Animal> theShelter = new AnimalShelter<Animal>(10);

            theShelter.leave(new Animal(100, "Hati", "Elephant", 3, false));
            theShelter.leave(new Animal(50, "Trump", "Orange ", 2, false));
            theShelter.leave(new Animal(10, "Bob", "Dog", 3, true));
            theShelter.leave(new Animal(2, "Billy", "Cat", 4, true));
            theShelter.leave(new Animal(45, "Helly Nilsson ", "Monkey", 2 , false));

          


            foreach (Animal a in theShelter.Where(x => x.NrOfLegs > 2 || x.Species == "Orange").OrderBy(x=>x.IsTame).OrderBy(x => x.Age).OrderBy(x => x.Name))
                
            {
                Console.WriteLine(a.Name + " is " + a.Age + "years old has" + a.NrOfLegs + "Legs , is a" + a.Species + "is the Tame = " + a.IsTame);
            }

            


        }
    }
}
