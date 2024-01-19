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
    internal class Waffle : IceCream
    {
        public string WaffleFlavour { get; set; }

        string[] flavourIndex = ["Red Velvet", "Charcoal", "Pandan"];
        public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string _waffleflavour) : base(option, scoops, flavours, toppings)
        {
            WaffleFlavour = _waffleflavour;
        }

        public override double CalculatePrice()
        {
            double price = (1 * Toppings.Count());
            //Match the price of scoops to the quantity
            switch (Scoops)
            {
                case 1:
                    price += 7;
                    break;
                case 2:
                    price += 8.50;
                    break;
                case 3:
                    price += 9.50;
                    break;
                default:
                    break;
            }

            if (flavourIndex.Contains(WaffleFlavour))
            {
                price += 3;
            }

            foreach (Flavour item in Flavours)
            {
                if (item.Premium == true)
                {
                    price += 2;
                }
            }

            return price;
        }



    }
}
