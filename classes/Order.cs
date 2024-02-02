using S10257799G_PRG2Assignment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
//==========================================================
// Student Number : S10255626K
// Student Name : Darius Koh Kai Keat
// Partner Name : Giam Jun Xian Clive
//==========================================================
*/

namespace PRG2_Assignment.classes
{
    internal class Order : IComparable<Order>
    {
        public int Id { get; set; }
        public DateTime TimeReceived { get; set; }
        public DateTime? TimeFulfilled { get; set; }
        public List<IceCream> IceCreamList { get; set; }

        public Order() { }
        public Order(int id, DateTime timeReceived)
        {
            Id = id;
            TimeReceived = timeReceived;

            IceCreamList = new List<IceCream>();
        }

        // Capitalises the first letters of a string
        public string CapitaliseFirstLetters(string s)
        {
            string[] stringList = s.Split(' '); // Splits the string up if there are multiple words to capitalise

            for (int i = 0; i < stringList.Count(); i++)
            {
                // Creates an array of char, as we are unable to change the first character of a string like in python
                char[] letters = stringList[i].ToCharArray(); 
                letters[0] = char.ToUpper(letters[0]); // Changes the first letter of the string to UpperCase

                stringList[i] = new string(letters);
            }

            return string.Join(" ", stringList); // Joins the stringlist together, adding a space between each word if necessary
        }

        public IceCream CreateIceCream()
        {
            // Lists for data validation
            List<string> options = new List<string> {"cup", "cone", "waffle"};
            List<string> wFList = new List<string> { "original", "red velvet", "charcoal", "pandan" };
            List<string> flavours = new List<string> { "vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt" };
            List<string> toppings = new List<string> { "sprinkles", "mochi", "sago", "oreos" };

            // Lists and variables to store user data
            List<Flavour> flList = new List<Flavour>();
            List<Topping> tList = new List<Topping>();
            List<object> iceCreamData = new List<object>();
            int scoops = 0;

            // Gets user's ice cream option between cup/cone/waffle
            while (true)
            {
                Console.Write("Please enter the option for your ice cream: ");
                string option = Console.ReadLine().ToLower();

                // Checks if the option selected is available
                if (options.Contains(option))
                {
                    iceCreamData.Add(option);

                    // Prompts user if they want their cone dipped
                    if (option.ToLower() == "cone")
                    {
                        Console.Write("Do you want your cone to be a Chocolate-dipped cone? (Y/N): ");
                        string dip = Console.ReadLine().ToLower();

                        // Creates an array with all available options and checks if the user input is in the array
                        if (new[] { "y", "n" }.Contains(dip))
                        {
                            if (dip == "y")
                                iceCreamData.Add(true);
                            else
                                iceCreamData.Add(false);
                            break;
                        }
                        else
                            Console.WriteLine("Please enter either 'Y' or 'N'.");
                    }

                    // Gets waffle flavour
                    else if (option == "waffle")
                    {
                        Console.Write("Which flavour of waffle would you like? (1. Original, 2. Red Velvet, 3. Charcoal, 4. Pandan): ");
                        try
                        {
                            int fOption = Convert.ToInt32(Console.ReadLine());

                            // Creates an array with all available options and checks if the user input is in the array
                            if (new[] { 1, 2, 3, 4 }.Contains(fOption))
                            {
                                iceCreamData.Add(wFList[fOption - 1]);
                                break;
                            }
                            else
                                Console.WriteLine("Please enter a value between 1-4.");
                        }
                        catch (Exception e) { Console.WriteLine("Please enter a value between 1-4."); }
                    }
                    else { break; }
                }
                else { Console.WriteLine("Please give a valid option between 'Cup', 'Cone' or 'Waffle'."); }
            }

            // Gets and validates number of scoops
            while (true)
            {
                Console.Write("Please enter the number of scoops: ");
                string sOption = Console.ReadLine();

                // Creates an array with all available options and checks if the user input is in the array
                if (new[] { "1", "2", "3" }.Contains(sOption)) 
                {
                    iceCreamData.Add(Convert.ToInt32(sOption));
                    break;
                }
                else
                    Console.WriteLine("Please enter a number between 1-3");
            }

            // Gets a maximum of 4 toppings before exiting the loop
            int count = 0;
            while (count < 4)
            {
                Console.Write("Please enter a topping to add ('N' to cancel): ");
                string tOption = Console.ReadLine().ToLower();

                if (tOption == "n")
                    break;
                else if (toppings.Contains(tOption))
                {
                    tList.Add(new Topping(CapitaliseFirstLetters(tOption)));
                    count++;
                }
                else
                    Console.WriteLine("Please enter a valid topping between 'Sprinkles', 'Mochi', 'Sago' and 'Oreos'.");
            }

            // Gets number of scoops according to type
            if ((string)iceCreamData[0] == "cup")
                scoops = Convert.ToInt16(iceCreamData[1]);
            else
                scoops = Convert.ToInt16(iceCreamData[2]);

            // Gets 3 ice cream flavours according to number of scoops
            count = 0;
            while (count < scoops)
            {
                // Used to output all available flavours if user keys in an unavailable flavour
                string flavourString = flavours[0];
                foreach (string s in flavours)
                    flavourString = flavourString + $", {s}";

                Console.Write("Please enter an ice cream flavour: ");
                string fOption = Console.ReadLine().ToLower();

                // Checks if the flavour is valid
                if (flavours.Contains(fOption))
                {
                    fOption = CapitaliseFirstLetters(fOption);
                    if (flavours.IndexOf(fOption) > 2)
                        flList.Add(new Flavour(fOption, true));    
                    else
                        flList.Add(new Flavour(fOption, false));
                    count++;
                }
                else
                    Console.WriteLine("Please enter a valid flavour. \n" +
                        $"Available flavours: {flavourString.Substring(0,flavourString.Length - 2)}");
            }

            iceCreamData[0] = CapitaliseFirstLetters((string)iceCreamData[0]); // Formats all data to have first letter capitalised

            // Creates respective object and returns the ice cream with all data filled
            if ((string)iceCreamData[0] == "Cup")
            {
                IceCream newIceCream = new Cup(Convert.ToString(iceCreamData[0]), Convert.ToInt16(iceCreamData[1]), flList, tList);
                return newIceCream;
            }
            else if ((string)iceCreamData[0] == "Cone")
            {
                IceCream newIceCream = new Cone(Convert.ToString(iceCreamData[0]), Convert.ToInt16(iceCreamData[2]), Convert.ToBoolean(iceCreamData[1]), flList, tList);
                return newIceCream;
            }
            else
            {
                IceCream newIceCream = new Waffle(Convert.ToString(iceCreamData[0]), Convert.ToInt16(iceCreamData[2]), CapitaliseFirstLetters(Convert.ToString(iceCreamData[1])), flList, tList);
                return newIceCream;
            }
        }

        // adds ice cream to list
        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }

        // deletes ice cream from list
        public void RemoveIceCream(int n)
        {
            IceCream modifyIceCream = IceCreamList[n - 1];
            IceCreamList.RemoveAt(n - 1);
        }

        // Calls create ice cream to get user input to modify their ice cream.
        public void ModifyIceCream(int n)
        {
            IceCreamList[n - 1] = CreateIceCream();
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (IceCream i in IceCreamList)
            {
                total += i.CalculatePrice();
            }
            return total;
        }

        public int CompareTo(Order next)
        {
            return (Convert.ToDateTime(this.TimeFulfilled)).CompareTo(Convert.ToDateTime(next.TimeFulfilled));
        }

        public override string ToString()
        {
            string ICListOut = "";
            IceCream temp;
            foreach (IceCream i in IceCreamList)
            {
                string type = i.Option;
                if (type == "Cup") 
                { 
                    temp = (Cup)i; 
                }
                else if(type == "Cone") 
                { 
                    temp = (Cone)i; 
                }
                else 
                { 
                    temp =  (Waffle)i; 
                }
                ICListOut += "\n" + temp.ToString();
            }

            return $"Id: {Id}, Time Recieved: {TimeReceived}, Time Fulfilled: {TimeFulfilled}\nIce Cream List{ICListOut}";
        }
    }
}