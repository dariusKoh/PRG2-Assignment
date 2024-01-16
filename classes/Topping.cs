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


namespace PRG2_Assignment.classes
{
    internal class Topping
    {
        //Set value of variable
        public string Type { get; set; }

        public Topping() { }
        //Object constructor
        public Topping(string _type)
        {
            Type = _type;
        }

        public override string ToString()
        {
            return $"Type: {Type}";
        }
    }
}
