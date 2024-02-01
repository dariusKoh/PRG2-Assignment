using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRG2_Assignment.classes;

/*
//==========================================================
// Student Number : S10257799G
// Student Name : Giam Jun Xian Clive
// Partner Name : Darius Koh Kai Keat
//==========================================================
*/

namespace S10257799G_PRG2Assignment
{
    internal class Cone : IceCream
    {
        //Set the value of variables
        public bool Dipped { get; set; }

        public Cone() { }
        //Object constructor
        public Cone(string option, int scoops, bool _dipped, List<Flavour> flavours, List<Topping> toppings) : base(option, scoops, flavours, toppings)
        {
            Dipped = _dipped;
        }
        //Function to read options.csv and get ice cream pricing based on scoops and if the cone is dipped
        public void getPrices()
        {
            using (StreamReader sr = new StreamReader("data/options.csv"))
            {
                string? s = sr.ReadLine();
                if (s != null) { string[] heading = s.Split(","); }
                while ((s = sr.ReadLine()) != null)
                {
                    string[] temp = s.Split(",");
                    Prices.Add(temp);
                }
            }
        }
        public override double CalculatePrice()
        {
            getPrices();
            //Calculate how much is owed for toppings
            double price = (1 * Toppings.Count());
            foreach (string[] array in Prices) 
            {
                //Check if the current array options match the options of the order and set the price accordingly
                if (Option == array[0] && Scoops == Convert.ToInt32(array[1]) && Convert.ToBoolean(array[2]) == Dipped)
                {
                    price += Convert.ToDouble(array[4]);
                    break;
                }
            }
            //Iterate through flavour list and add 2 dollars if the flavour is premium
            foreach (Flavour item in Flavours)
            {
                if (item.Premium == true)
                {
                    price += 2;
                }
            }
            return price;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nDipped: {Dipped}";
        }
    }
}
