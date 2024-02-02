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

        public string CapitaliseFirstLetters(string s)
        {
            string[] stringList = s.Split(' ');

            for (int i = 0; i < stringList.Count(); i++)
            {
                char[] letters = stringList[i].ToCharArray();
                letters[0] = char.ToUpper(letters[0]);

                stringList[i] = new string(letters);
            }

            return string.Join(" ", stringList);
        }

        public IceCream CreateIceCream()
        {
            List<string> options = new List<string> {"cup", "cone", "waffle"};
            List<string> wFList = new List<string> { "original", "red velvet", "charcoal", "pandan" };
            List<string> flavours = new List<string> { "vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt" };
            List<string> toppings = new List<string> { "sprinkles", "mochi", "sago", "oreos" };
            List<Flavour> flList = new List<Flavour>();
            List<Topping> tList = new List<Topping>();
            List<object> iceCreamData = new List<object>();

            while (true)
            {
                Console.Write("Please enter the option for your ice cream: ");
                string option = Console.ReadLine().ToLower();
                if (options.Contains(option))
                {
                    iceCreamData.Add(option);

                    if (option.ToLower() == "cone")
                    {
                        Console.Write("Do you want your cone to be a Chocolate-dipped cone? (y/n): ");
                        string dip = Console.ReadLine().ToLower();

                        if (new[] { "y", "n" }.Contains(dip))
                        {
                            if (dip == "y")
                                iceCreamData.Add(true);
                            else
                                iceCreamData.Add(false);
                            break;
                        }
                        else
                            Console.WriteLine("Please enter either 'y' or 'n'.");
                    }

                    else if (option == "waffle")
                    {
                        Console.Write("Which flavour of waffle would you like? (1. Original, 2. Red Velvet, 3. Charcoal, 4. Pandan): ");
                        try
                        {
                            int fOption = Convert.ToInt32(Console.ReadLine());

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

            while (true)
            {
                Console.Write("Please enter the number of scoops: ");
                string sOption = Console.ReadLine();

                if (new[] { "1", "2", "3" }.Contains(sOption))
                {
                    iceCreamData.Add(Convert.ToInt32(sOption));
                    break;
                }
                else
                    Console.WriteLine("Please enter a number between 1-3");
            }

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

            count = 0;
            int scoops = 0;
            if ((string)iceCreamData[0] == "cup")
                scoops = Convert.ToInt16(iceCreamData[1]);
            else
                scoops = Convert.ToInt16(iceCreamData[2]);

            while (count < scoops)
            {
                string flavourString = flavours[0];
                foreach (string s in flavours)
                    flavourString = flavourString + $", {s}";

                Console.Write("Please enter an ice cream flavour: ");
                string fOption = Console.ReadLine().ToLower();

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
                    Console.WriteLine("Please enter a valid flavour.");
            }

            iceCreamData[0] = CapitaliseFirstLetters((string)iceCreamData[0]);

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

        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }

        public void RemoveIceCream(int n)
        {
            IceCream modifyIceCream = IceCreamList[n - 1];
            IceCreamList.RemoveAt(n - 1);
        }
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
