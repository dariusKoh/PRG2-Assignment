using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10257799G_PRG2Assignment
{
    internal class Cup : IceCream
    {
        //Set the value of variables
        public Cup() { }
        //Object constructor
        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base(option, scoops, flavours, toppings) { }

        public override double CalculatePrice()
        {
            double price = (1 * Toppings.Count());
            //Match the price of scoops to the quantity
            switch (Scoops)
            {
                case 1:
                    price += 4;
                    break;
                case 2:
                    price += 5.50;
                    break;
                case 3:
                    price += 6.50;
                    break;
                default:
                    break;
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

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
