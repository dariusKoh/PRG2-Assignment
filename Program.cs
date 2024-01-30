//==========================================================
// Student Name : Giam Jun Xian Clive (S10257799G)
// Student Name : Darius Koh Kai Keat (S10255626K)
//==========================================================
using PRG2_Assignment.classes;
using S10257799G_PRG2Assignment;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

//Utility methods : Clive

//Read flavours.csv
List<string[]> getFlavours()
{
    List<string[]> output = new List<string[]>();
    using (StreamReader sr = new StreamReader("data/flavours.csv"))
    {
        string? s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] temp = s.Split(",");
            temp[0] = temp[0].ToLower();
            output.Add(temp);
        }
    }
    return output;
}

//Read customers.csv and create customers
void getCustomers(List<Customer> customerList) 
{
    using (StreamReader sr = new StreamReader("data/customers.csv"))
    {
        string? s = sr.ReadLine();
        if (s != null) { string[] heading = s.Split(','); }
        while ((s = sr.ReadLine()) != null)
        {
            string[] temp = s.Split(",");
            PointCard tempCard = new PointCard(Convert.ToInt32(temp[4]), Convert.ToInt32(temp[5]));
            tempCard.CheckTierUpgrade();
            Customer tempCustomer = new Customer(temp[0], Convert.ToInt32(temp[1]), Convert.ToDateTime(temp[2]));
            tempCustomer.Rewards = tempCard;
            customerList.Add(tempCustomer);
        }
    }
}
//Assign an order to a customer based on member id
Customer associateCustomer(string[] info, List<Customer> customerList)
{
    foreach(Customer temp in customerList)
    {
        if (Convert.ToInt32(info[1]) == temp.MemberId)
        {
            return temp;
        }
    }
    return null;
}
//Read orders.csv and create orders
void getOrders(List<Order> orderList, List<Customer> customerList)
{
    List<string[]> flavours = getFlavours();
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
            Customer tempCustomer = associateCustomer(temp, customerList);
            Order tempOrder = new Order();
            bool idExists = false;

            foreach (Order order in orderList)
            {
                if (order.Id == Convert.ToInt32(temp[0]))
                {
                    tempOrder = order;
                    idExists = true;
                    break;
                }
            }

            if (idExists == false)
            {
                tempOrder = new Order(Convert.ToInt32(temp[0]), Convert.ToDateTime(temp[2]));
                tempOrder.TimeFulfilled = Convert.ToDateTime(temp[3]);
                orderList.Add(tempOrder);
                tempCustomer.OrderHistory.Add(tempOrder);
            }

            int orderIndex = tempCustomer.OrderHistory.IndexOf(tempOrder);
            Order customerHistory = tempCustomer.OrderHistory[orderIndex];
            List<Flavour> flavourList = new List<Flavour>();
            List<Topping> toppingList = new List<Topping>();

            for (int i = 8; i < 11; i++)
            {
                if (temp[i] != "")
                {
                    bool premium = false;
                    foreach (var array in flavours)
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
                        customerHistory.IceCreamList.Add(tempIceCream);
                        break;
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
                        customerHistory.IceCreamList.Add(tempIceCream);
                        break;
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
                        customerHistory.IceCreamList.Add(tempIceCream);
                        break;
                    }
                }
            }
        }
    }
}

List<Order> orderList = new List<Order>();
List<Customer> customerList = new List<Customer>();
getCustomers(customerList);
getOrders(orderList, customerList);

// Basic 1 : Darius
void ListAllCustomers()
{
    string[] customers = File.ReadAllLines("data/customers.csv");
    for (int i = 1; i < customers.Length; i++)//foreach (string i in customers)
    {
        if (customers[i] == "Name,MemberId,DOB,MembershipStatus,MembershipPoints,PunchCard")
            continue;
        string[] customerData = customers[i].Split(',');
        Console.WriteLine($"{i,-4}{customerData[0],-15}{customerData[1],-8}{customerData[2],-12}{customerData[3],-10}{customerData[4],-5}{customerData[5]}");
    }
}
//ListAllCustomers();


// Basic 2 : Clive
void ListCurrentOrders(Queue<Order>goldQueue, Queue<Order>normalQueue)
{
    Console.WriteLine("Current orders in Gold Queue");
    foreach(Order item in goldQueue)
    {
        Console.WriteLine(item.ToString() + "\n------------");
    }
    Console.WriteLine("Current orders in Regular Queue");
    foreach (Order item in normalQueue)
    {
        Console.WriteLine(item.ToString() + "\n------------");
    }
}
//ListCurrentOrders(goldQueue, normalQueue);

// Basic 3 : Darius
void RegisterNewCustomer()
{
    Console.Write("Enter customer information in the following format (name;id number;date of birth): ");
    string[] newCustomerInfo = Console.ReadLine().Split(';');

    Customer newCustomer = new Customer(newCustomerInfo[0], Convert.ToInt32(newCustomerInfo[1]), Convert.ToDateTime(newCustomerInfo[2]));
    PointCard newPointCard = new PointCard(0, 0);

    newCustomer.Rewards = newPointCard;

    string writeToFile = $"{newCustomer.Name},{newCustomer.MemberId},{newCustomer.DoB.ToString("MM/dd/yyyy")},{newPointCard.Tier},{newPointCard.Points},{newPointCard.PunchCard}";


    using (StreamWriter sw = new StreamWriter("data/customers.csv", true))
    {
        sw.WriteLine(writeToFile);
    }
}
//RegisterNewCustomer();
//ListAllCustomers();

// Basic 4 : Darius


// Basic 5 : Clive

void ListAllOrders(List<Customer> customerList)
{
    Console.WriteLine("Customers" + "\n------------");
    ListAllCustomers();
    int option = 0;
    while (true)
    {
        Console.Write("Enter the customer number: ");
        try
        {
            option = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException) 
        { 
            Console.WriteLine("Invalid input, must be an integer.");  
            continue; 
        }
        if (option <= 0 || option > customerList.Count())
        {
            Console.WriteLine($"Input out of range, please enter an integer within 1 and {customerList.Count()}.");
            continue;
        }
        else { break; } 
    }
    option -= 1;
    Console.WriteLine(option);
    Customer selectCustomer = customerList[option];
    //Order currentOrder = selectCustomer.CurrentOrder;
    Order currentOrder = selectCustomer.OrderHistory[0];
    List<Order> orderHistory = selectCustomer.OrderHistory;

    Console.WriteLine($"---------Current Order---------\nId: {currentOrder.Id}\nTime received: {currentOrder.TimeReceived}\n-------------------------------");

    foreach(IceCream iceCream in currentOrder.IceCreamList)
    {
        string allFlavours = "";
        string allToppings = "";
        foreach(Flavour item in iceCream.Flavours)
        {
            allFlavours += $"\n{item.ToString()}";
        }
        foreach (Topping item in iceCream.Toppings)
        {
            allToppings += $"\n{item.ToString()}";
        }

        string baseOut = $"Option: {iceCream.Option}\nScoops: {iceCream.Scoops}\n---------\nFlavours\n---------{allFlavours}\n---------\nToppings\n---------{allToppings}\n---------";

        if (iceCream.Option == "Cup")
        {
            Console.WriteLine(baseOut);
        }
        else if (iceCream.Option == "Cone")
        {
            Cone temp = (Cone)iceCream;
            baseOut += $"\nDipped: {temp.Dipped}\n---------";
            Console.WriteLine(baseOut);
        }
        else if (iceCream.Option == "Waffle")
        {
            Waffle temp = (Waffle)iceCream;
            baseOut += $"\nWaffle flavour: {temp.WaffleFlavour}\n---------";
            Console.WriteLine(baseOut);
        }

    }
    Console.WriteLine("---------Order History---------");
    Console.WriteLine(orderHistory.Count);
    foreach(Order order in orderHistory)
    {
        foreach (IceCream iceCream in order.IceCreamList)
        {
            string allFlavours = "";
            string allToppings = "";
            foreach (Flavour item in iceCream.Flavours)
            {
                allFlavours += $"\n{item.ToString()}";
            }
            foreach (Topping item in iceCream.Toppings)
            {
                allToppings += $"\n{item.ToString()}";
            }

            string baseOut = $"Option: {iceCream.Option}\nScoops: {iceCream.Scoops}\n---------\nFlavours\n---------{allFlavours}\n---------\nToppings\n---------{allToppings}\n---------";

            if (iceCream.Option == "Cup")
            {
                Console.WriteLine(baseOut);
            }
            else if (iceCream.Option == "Cone")
            {
                Cone temp = (Cone)iceCream;
                baseOut += $"\nDipped: {temp.Dipped}\n---------";
                Console.WriteLine(baseOut);
            }
            else if (iceCream.Option == "Waffle")
            {
                Waffle temp = (Waffle)iceCream;
                baseOut += $"\nWaffle flavour: {temp.WaffleFlavour}\n---------";
                Console.WriteLine(baseOut);
            }

        }
    }
}
//ListAllOrders(customerList);
// Basic 6 : Clive


// Advanced (A) : 


// Advanced (B) : 