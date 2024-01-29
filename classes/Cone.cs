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
            //Check if the cone is dipped and adjust the price if so
            foreach (string[] array in Prices) 
            { 
                if (Option == array[0] && Scoops == Convert.ToInt32(array[1]) && Convert.ToBoolean(array[2]) == Dipped)
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
            return base.ToString() + $" Dipped: {Dipped}";
        }
    }
}
