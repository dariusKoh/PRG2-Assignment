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
    internal class Cup : IceCream
    {
        //Set the value of variables
        public Cup() { }
        //Object constructor
        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base(option, scoops, flavours, toppings) { }

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
            //Match the price of scoops to the quantity
            foreach (string[] array in Prices)
            {
                if (Option == array[0] && Scoops == Convert.ToInt32(array[1]))
                {
                    price += Convert.ToDouble(array[4]);
                    break;
                }
            }
            //Check the number of premium scoops and add it to the price
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
            return base.ToString();
        }
    }
}
