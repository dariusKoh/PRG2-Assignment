using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
//==========================================================
// Student Number : S10257799G
// Student Name : Giam Jun Xian Clive
// Partner Name : Darius Koh Kai Keat
//==========================================================
*/

namespace S10257799G_PRG2Assignment
{
    abstract class IceCream
    {
        //Set values of variables
        public string Option { get; set; }
        public int Scoops { get; set; }
        //Lists to store objects of other classes
        public List<Flavour> Flavours { get; set; }
        public List<Topping> Toppings { get; set; }

        public IceCream() { }
        
        public IceCream(string _option, int _scoops, List<Flavour> _flavours, List<Topping> _toppings)
        {
            Option = _option;
            Scoops = _scoops;
            Flavours = _flavours;
            Toppings = _toppings;
        }

        public abstract double CalculatePrice();
        public override string ToString()
        {
            string allFlavours = "";
            string allToppings = "";
            //Iterate through every item in the lists and add them for output in ToString()
            foreach(Flavour item in Flavours)
            {
                allFlavours += " " + item.ToString();
            }

            foreach(Topping item in Toppings)
            {
                allToppings += " " + item.ToString();
            }
            return $"Option: {Option,-10}Scoops: {Scoops,-10}Flavours:{allFlavours}Toppings:{allToppings}";
        }

    }
}
