//==========================================================
// Student Name : Giam Jun Xian Clive (S10257799G)
// Student Name : Darius Koh Kai Keat (S10255626K)
//==========================================================

// Basic 1 : Darius
using PRG2_Assignment.classes;
using S10257799G_PRG2Assignment;
using System.Data;
using System.Reflection;

void ListAllCustomers()
{
    string[] customers = File.ReadAllLines("data/customers.csv");
    foreach (string i in customers)
    {
        if (i == "Name,MemberId,DOB,MembershipStatus,MembershipPoints,PunchCard")
            continue;
        Console.WriteLine(i);
    }
}
ListAllCustomers();

// Basic 2 : Clive
void ListAllOrders()
{

    List<string[]> flavours = getFlavours();
    List<Order> orderList = new List<Order>();
    using (StreamReader sr = new StreamReader("data/orders.csv"))
    {
        string? s = sr.ReadLine();
        if (s != null) 
        {
            string[] heading = s.Split(",");
        }
        
        while ((s = sr.ReadLine()) != null)
        {
            string[] temp = s.Split(",");
            orderList.Add(new Order(Convert.ToInt32(temp[0]), Convert.ToDateTime(temp[2])));
            List<Flavour> flavourList = new List<Flavour>();
            List<Topping>toppingList = new List<Topping>();

            for (int i = 8; i<11; i++)
            {
                if (temp[i] != "")
                {
                    bool premium = false;
                    foreach(var array in flavours)
                    {
                        if (array[0] == temp[i].ToLower() && array[1] == "2") 
                        { 
                            premium = true;
                            break;
                        }
                    }
                    Flavour tempFlavour = new Flavour(temp[i], premium);
                    flavourList.Add(tempFlavour);
                }
            }

            for (int i = 11; i < 15; i++)
            {
                if (temp[i] != "")
                {
                    Topping tempTopping = new Topping(temp[i]);
                    toppingList.Add(tempTopping);
                }
            }
            
            if (temp[4] == "Cup")
            {
                IceCream tempIceCream = new Cup(temp[4], Convert.ToInt32(temp[5]), flavourList, toppingList);
                foreach (Order order in orderList)
                {
                    if (order.Id == Convert.ToInt32(temp[0]))
                    {
                        order.AddIceCream(tempIceCream);
                    }
                }
            }
            else if (temp[4] == "Cone")
            {
                IceCream tempIceCream = new Cone(temp[4], Convert.ToInt32(temp[5]), Convert.ToBoolean(temp[6]), flavourList, toppingList);
                foreach (Order order in orderList)
                {
                    if (order.Id == Convert.ToInt32(temp[0]))
                    {
                        order.AddIceCream(tempIceCream);
                    }
                }
            }
            else if (temp[4] == "Waffle")
            {
                IceCream tempIceCream = new Waffle(temp[4], Convert.ToInt32(temp[5]), temp[7], flavourList, toppingList);
                foreach (Order order in orderList)
                {
                    if (order.Id == Convert.ToInt32(temp[0]))
                    {
                        order.AddIceCream(tempIceCream);
                    }
                }
            }
        }
    }
    foreach (Order order in orderList)
    {
        Console.WriteLine(order.ToString() + "\n------------");
    }
}

List<string[]> getFlavours()
{
    List<string[]> output = new List<string[]>();
    using (StreamReader sr = new StreamReader("data/flavours.csv"))
    {
        string? s = sr.ReadLine();
        while((s = sr.ReadLine()) != null)
        {
            string[] temp = s.Split(",");
            temp[0] = temp[0].ToLower();
            output.Add(temp);
        }
    }
    return output;
}

ListAllOrders();
// Basic 3 : Darius


// Basic 4 : Darius


// Basic 5 : Clive


// Basic 6 : Clive


// Advanced (A) : 


// Advanced (B) : 