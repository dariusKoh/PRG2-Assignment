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
    internal class Flavour
    {
        //Retrieve and set value of variables
        public string Type { get; set; }
        public bool Premium { get; set; }

        public Flavour() { }
        //Object constructor
        public Flavour(string _type, bool _premium)
        {
            Type = _type;
            Premium = _premium;
        }

        public override string ToString()
        {
            return $"Type: {Type,-10}Premium: {Premium,-10}";
        }
    }
}
