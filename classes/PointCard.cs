﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public void AddPoints(int amount)
        {
            int pointsConverted = Convert.ToInt32(Math.Floor(amount * 0.72));
            Points += pointsConverted;
        }

        public void RedeemPoints(int amount)
        {
            if (Tier == "Ordinary")
            {
                Console.WriteLine("Only silver and gold members can redeem their points.");
            }

            else
            {
                Points -= amount;
            }
        }

        public void Punch(int points)
        {
            if (PunchCard < 10)
            {
                PunchCard++;
            }
            else
            {
                PunchCard = 0;
            }
        }

        public override string ToString()
        {
            return $"Points: {Points}, Punch Card: {PunchCard}, Tier: {Tier}";
        }
    }
}