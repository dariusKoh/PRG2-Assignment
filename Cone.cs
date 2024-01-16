using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10257799G_PRG2Assignment
{
    class Cone:IceCream
    {
        //Set the value of variables
        public bool Dipped { get; set; }

        public Cone() { }
        //Object constructor
        public Cone(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, bool _dipped) : base(option, scoops, flavours, toppings) 
        { 
            Dipped = _dipped;
        }

        public override double CalculatePrice()
        {
            //Calculate how much is owed for toppings
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
            //Check if the cone is dipped and adjust the price if so
            switch (Dipped)
            {
                case true:
                    price += 2;
                    break;
                default:
                    break;
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


    }
}
