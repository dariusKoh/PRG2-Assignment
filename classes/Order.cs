using S10257799G_PRG2Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment.classes
{
    internal class Order
    {
        public int Id { get; set; }
        public DateTime TimeReceived { get; set; }
        public DateTime? TimeFulfilled { get; set; }
        public List<IceCream> IceCreamList { get; set; }

        public Order() { }
        public Order(int id, DateTime timeReceived)
        {
            Id = id;
            TimeReceived = timeReceived;

            IceCreamList = new List<IceCream>();
        }

        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }

        public void RemoveIceCream(int n)
        {
            IceCream modifyIceCream = IceCreamList[n - 1];
            IceCreamList.RemoveAt(n - 1);
        }
        public void ModifyIceCream(int n) //implement later
        {
            /*
            IceCream modifyIceCream = IceCreamList[n - 1];
            IceCreamList.RemoveAt(n - 1);

            Console.Write("Please enter your ice cream order (option, scoops, flavours, toppings) seperated by (,): ");
            string[] newOrder = Console.ReadLine().Split(",");

            IceCream newIceCream = new IceCream(newOrder[0]);
            */
        }

        public void CalculateTotal(double n)
        {

        }

        public override string ToString()
        {
            string ICListOut = "";
            foreach (IceCream i in IceCreamList)
            {
                ICListOut += i.ToString();
            }

            return $"Id: {Id}, Time Recieved: {TimeReceived}, Time Fulfilled: {TimeFulfilled}\nIce Cream List: {ICListOut}";
        }
    }
}
