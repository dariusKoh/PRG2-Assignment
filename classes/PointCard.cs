using System;
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
    internal class PointCard
    {
        public int Points { get; set; }
        public int PunchCard { get; set; }
        public string Tier { get; set; }

        public PointCard() { }
        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
            Tier = "Ordinary";
        }

        // Checks if a tier upgrade is available, and upgrades the customer's tier if so
        public void CheckTierUpgrade()
        {
            if (Tier == "Ordinary" && Points >= 50)
            {
                Tier = "Silver";
            }

            else if (Tier == "Silver" && Points >= 100)
            {
                Tier = "Gold";
            }
        }

        // Adds points to the customer's account after a purchase
        public void AddPoints(double amount)
        {
            int pointsConverted = Convert.ToInt32(Math.Floor(amount * 0.72));
            Points += pointsConverted;
        }

        // Deducts points from customer when points are used
        public void RedeemPoints(int amount)
        {
            // Checks if customer has enough points
            if (amount > Points)
                Console.WriteLine($"You do not have that many points!\nCurrent Points: {Points}");
            else
                Points -= amount;        
        }

        // Punches card and resets card
        public void Punch()
        {
            if (PunchCard < 10)
                PunchCard++;
            else
                PunchCard = 0;
        }

        public override string ToString()
        {
            return $"Points: {Points}, Punch Card: {PunchCard}, Tier: {Tier}";
        }
    }
}
