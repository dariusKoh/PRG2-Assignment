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
    internal class Customer
    {
        public string Name { get; set; }
        public int MemberId { get; set; }
        public DateTime DoB { get; set; }
        public Order CurrentOrder { get; set; }
        public List<Order> OrderHistory { get; set; }
        public PointCard Rewards { get; set; }

        public Customer() { }
        public Customer(string name, int memberId, DateTime dob)
        {
            Name = name;
            MemberId = memberId;
            DoB = dob;

            OrderHistory = new List<Order>();
        }

        public Order MakeOrder()
        {
            string orderId = Convert.ToString(MemberId) + Convert.ToString(OrderHistory.Count);

            Order newOrder = new Order(Convert.ToInt32(orderId) ,DateTime.Now);
            return newOrder;
        }

        public bool IsBirthday()
        {
            if (DoB == DateTime.Today)
                return true;
            return false;
        }

        public override string ToString()
        {
            /*
            string orderHistoryOut = "";
            foreach (Order i in OrderHistory)
            {
                orderHistoryOut += $"\n\n{i.ToString()}";
            }*/

            return $"Name: {Name}, Member Id: {MemberId}, DoB: {DoB}, Current Order: {CurrentOrder}\nRewards: {Rewards}\n";
        }
    }
}
