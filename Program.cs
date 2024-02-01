//==========================================================
// Student Name : Giam Jun Xian Clive (S10257799G)
// Student Name : Darius Koh Kai Keat (S10255626K)
//==========================================================
using PRG2_Assignment.classes;
using S10257799G_PRG2Assignment;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata;
using System.Transactions;
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
    //Create empty list to store arrays containing each flavour and its value
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
            {   //Check if an order with this id already exists in the list
                if (order.Id == Convert.ToInt32(temp[0]))
                {
                    tempOrder = order;
                    idExists = true;
                    break;
                }
            }
            //Adds the order to the list if it does not exist in the list
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
Queue<Order> goldQueue = new Queue<Order>();
Queue<Order> normalQueue = new Queue<Order>();

getCustomers(customerList);
getOrders(orderList, customerList);
/* Test data for function 2
goldQueue.Enqueue(orderList[0]);
goldQueue.Enqueue(orderList[1]);
*/

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
{   //Iterate through every element in each queue and output the orders
    Console.WriteLine("Current orders in Gold Queue\n-------------------------------");
    foreach(Order item in goldQueue)
    {
        Console.WriteLine($"Id: {item.Id}\nTime received: {item.TimeReceived}\n-------------------------------");
        displayCurrentItems(item);
    }
    Console.WriteLine("Current orders in Regular Queue\n-------------------------------");
    foreach (Order item in normalQueue)
    {
        Console.WriteLine($"Id: {item.Id}\nTime received: {item.TimeReceived}\n-------------------------------");
        displayCurrentItems(item);
    }
}
//ListCurrentOrders(goldQueue, normalQueue);

// Basic 3 : Darius
void RegisterNewCustomer()
{
    while (true)
    {
        try
        {
            Console.Write("Enter customer information in the following format (name;id number;date of birth): ");
            string[] newCustomerInfo = Console.ReadLine().Split(';');

            Customer newCustomer = new Customer(newCustomerInfo[0], Convert.ToInt32(newCustomerInfo[1]), Convert.ToDateTime(newCustomerInfo[2]));
            PointCard newPointCard = new PointCard(0, 0);

            newCustomer.Rewards = newPointCard;

            string writeToFile = $"{newCustomer.Name},{newCustomer.MemberId},{newCustomer.DoB.ToString("MM/dd/yyyy")},{newPointCard.Tier},{newPointCard.Points},{newPointCard.PunchCard}";

            using (StreamWriter sw = new StreamWriter("data/customers.csv", true)) { sw.WriteLine(writeToFile); }
            break;
        }
        catch (Exception ex) { Console.WriteLine("Please enter customer information following the format of: (name;id number;date of birth)."); }
    }
    
}
//RegisterNewCustomer();
//ListAllCustomers();

// Basic 4 : Darius
void CreateCustomerOrder()
{
    ListAllCustomers();
    Customer chosenCustomer = new Customer();
    Order newOrder = new Order(orderList[orderList.Count - 1].Id + 1, DateTime.Now);

    while (true)
    {
        try
        {
            Console.Write("Please select a customer from the list: ");
            int chosenCustomerNum = Convert.ToInt16(Console.ReadLine());

            chosenCustomer = customerList[chosenCustomerNum - 1];
            break;
        }
        catch (Exception e)
        {
            Console.WriteLine("Please enter a valid customer.");
        }
    }
    Console.WriteLine(chosenCustomer);

    newOrder.AddIceCream(newOrder.CreateIceCream());
    while (true)
    {
        Console.Write("Would you like to add another Ice Cream? (Y/N): ");
        string additionalIceCream = Console.ReadLine().ToLower();

        if (additionalIceCream == "y") { newOrder.AddIceCream(newOrder.CreateIceCream()); }
        else { break; }
    }
    chosenCustomer.CurrentOrder = newOrder;

    if (chosenCustomer.Rewards.Tier == "Gold")
    {
        // Queue code goes here
    }
    else
    {
        // Queue code goes here
    }

    Console.WriteLine($"Order for customer {chosenCustomer.Name} with order ID {newOrder.Id} has been placed in the queue.");
}
//CreateCustomerOrder();

// Basic 5 : Clive

void ListAllOrders(List<Customer> customerList)
{
    Console.WriteLine("Customers" + "\n------------");
    ListAllCustomers();
    int option = 0;
    //input validation for option
    while (true)
    {
        Console.Write("Enter the customer number: ");
        try
        {
            option = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException) 
        {
            Console.WriteLine($"Invalid input format, please enter an integer within 1 and {customerList.Count()} inclusive.");
            continue; 
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.ToString()}");
            continue;
        }
        if (option <= 0 || option > customerList.Count())
        {
            Console.WriteLine($"Input out of range, please enter an integer within 1 and {customerList.Count()} inclusive.");
            continue;
        }
        else { break; } 
    }
    //Subtract 1 from option as starting index is 0
    option -= 1;
    //Set customer to display orders
    Customer selectCustomer = customerList[option];
    //Retrieve the current order of the customer and store as a object
    Order currentOrder = selectCustomer.CurrentOrder;
    //Retrieve the order history of the customer and store as a list
    List<Order> orderHistory = selectCustomer.OrderHistory;

    //Code to run if a current order exists
    if (currentOrder != null)
    {
        //Display id of order and time received
        Console.WriteLine($"---------Current Order---------\nId: {currentOrder.Id}\nTime received: {currentOrder.TimeReceived}\n-------------------------------");
        //Iterate through every ice cream in the order
        foreach (IceCream iceCream in currentOrder.IceCreamList)
        {
            string allFlavours = "";
            string allToppings = "";
            //Append all flavours to a string
            foreach (Flavour item in iceCream.Flavours)
            {
                allFlavours += $"\n{item.ToString()}";
            }
            //Append all toppings to a string
            foreach (Topping item in iceCream.Toppings)
            {
                allToppings += $"\n{item.ToString()}";
            }
            //Construct the base output string
            string baseOut = $"Option: {iceCream.Option}\nScoops: {iceCream.Scoops}\n---------\nFlavours\n---------{allFlavours}\n---------\nToppings\n---------{allToppings}\n---------";

            if (iceCream.Option == "Cup")
            {
                Console.WriteLine(baseOut);
            }
            //Add dipped field if ice cream is a cone
            else if (iceCream.Option == "Cone")
            {
                Cone temp = (Cone)iceCream;
                baseOut += $"\nDipped: {temp.Dipped}\n---------";
                Console.WriteLine(baseOut);
            }
            //Add waffle flavour field if ice cream is a waffle
            else if (iceCream.Option == "Waffle")
            {
                Waffle temp = (Waffle)iceCream;
                baseOut += $"\nWaffle flavour: {temp.WaffleFlavour}\n---------";
                Console.WriteLine(baseOut);
            }

        }
    }
    //Code to run if there is no current order
    else
    {
        Console.WriteLine("---------Current Order---------\nThis customer does not have a current order.");
    }
    //Display order history
    Console.WriteLine("---------Order History---------");
    foreach(Order order in orderHistory)
    {
        //Display id of order and time received
        Console.WriteLine($"Id: {order.Id}\nTime received: {order.TimeReceived}\n-------------------------------");
        //Iterate through every ice cream in the order
        foreach (IceCream iceCream in order.IceCreamList)
        {
            string allFlavours = "";
            string allToppings = "";
            //Append all flavours to a string
            foreach (Flavour item in iceCream.Flavours)
            {
                allFlavours += $"\n{item.ToString()}";
            }
            //Append all flavours to a string
            foreach (Topping item in iceCream.Toppings)
            {
                allToppings += $"\n{item.ToString()}";
            }
            //Construct the base output string
            string baseOut = $"Option: {iceCream.Option}\nScoops: {iceCream.Scoops}\n---------\nFlavours\n---------{allFlavours}\n---------\nToppings\n---------{allToppings}\n---------";

            if (iceCream.Option == "Cup")
            {
                Console.WriteLine(baseOut);
            }
            //Add the dipped field if the ice cream is a cone
            else if (iceCream.Option == "Cone")
            {
                Cone temp = (Cone)iceCream;
                baseOut += $"\nDipped: {temp.Dipped}\n---------";
                Console.WriteLine(baseOut);
            }
            //Add the waffle flavour field if the ice cream is a waffle
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

void ModifyOrder(List<Customer> customerList)
{
    //Display all customers
    ListAllCustomers();
    int customerOption = 0;
    //input validation for customer option
    while (true)
    {
        Console.Write("Enter the index of the customer whose order you want to modify: ");
        try
        {
            customerOption = Convert.ToInt32(Console.ReadLine());
        }
        //Check if input is in the correct format and output error message if it isnt, then restart loop
        catch (FormatException)
        {
            Console.WriteLine($"Invalid input format, please enter an integer within 1 and {customerList.Count()} inclusive.");
            continue;
        }
        //Catch any unexpected exceptions and restart loop
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.ToString()}");
            continue;
        }
        //Check if input is within range and output error message if it isnt, then restart loop
        if (customerOption <= 0 || customerOption > customerList.Count()) 
        {
            Console.WriteLine($"Input out of range, please enter an integer within 1 and {customerList.Count()} inclusive.");
            continue;
        }
        else { break; }
    }
    //Subtract 1 from customer option as index starts from 0
    customerOption -= 1;
    //Set customer to display orders
    Customer selectedCustomer = customerList[customerOption];
    //Retrieve the customer's current order and store as an object
    Order currentOrder = selectedCustomer.CurrentOrder;

    /*If function 4 incomplete at time of testing, use these to test the function
      Order currentOrder = selectedCustomer.OrderHistory[1];
      selectedCustomer.CurrentOrder = currentOrder;
    */

    //Code to run if there is a current order
    if (currentOrder != null)
    { 
        displayCurrentItems(currentOrder);

        int action = 0;
        //input validation for action
        while (true)
        {
            Console.WriteLine("Options\n---------\n[1] Modify an existing ice cream\n[2] Add new ice cream to order\n[3] Delete an existing ice cream");
            Console.Write("Enter the option you want to execute: ");
            try
            {
                action = Convert.ToInt32(Console.ReadLine());
            }
            //Check if input in correct format, output error and restart loop if not
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format, please enter an integer within 1 and 3 inclusive.");
                continue;
            }
            //Catch unexpected exception, restart loop
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.ToString()}");
                continue;
            }
            //Check if input within range, output error and restart loop if not
            if (action < 1 || action > 3)
            {
                Console.WriteLine("Input out of range, please enter an integer within 1 and 3 inclusive.");
                continue;
            }
            else { break; }
        }
        
        switch (action)
        {
            //Modify ice cream
            case 1:
                {
                    int editOption = 0;
                    while (true)
                    {
                        Console.Write("Enter the index of the ice cream you want to modify: ");
                        try
                        {
                            editOption = Convert.ToInt32(Console.ReadLine());
                        }
                        //Check if input in correct format, output error and restart loop if not
                        catch (FormatException)
                        {
                            Console.WriteLine($"Invalid input format, please enter an integer within 1 and {currentOrder.IceCreamList.Count()} inclusive.");
                            continue;
                        }
                        //Catch unexpected exception, restart loop
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception: {e.ToString()}");
                            continue;
                        }
                        //Check if input within range, output error and restart loop if not
                        if (editOption <= 0 || editOption > currentOrder.IceCreamList.Count())
                        {
                            Console.WriteLine($"Input out of range, please enter an integer within 1 and {currentOrder.IceCreamList.Count()} inclusive.");
                            continue;
                        }
                        else { break; }
                    }
                    currentOrder.ModifyIceCream(editOption);
                    break;
                }
            //Add new ice cream
            case 2: 
                { 
                    currentOrder.AddIceCream(currentOrder.CreateIceCream());
                    break;
                }
            //Delete ice cream
            case 3: 
                {
                    int length = currentOrder.IceCreamList.Count();
                    if (length == 1)
                    {
                        Console.WriteLine("An order must contain at least 1 ice cream, unable to delete.");
                        break;
                    }
                    else
                    {
                        int removeOption = 0;
                        while (true)
                        {
                            Console.Write("Enter the index of the ice cream you want to remove: ");
                            try
                            {
                                removeOption = Convert.ToInt32(Console.ReadLine());
                            }
                            //Check if input in correct format, output error and restart loop if not
                            catch (FormatException)
                            {
                                Console.WriteLine($"Invalid input format, please enter an integer within 1 and {currentOrder.IceCreamList.Count()} inclusive.");
                                continue;
                            }
                            //Catch unexpected exception, restart loop
                            catch (Exception e)
                            {
                                Console.WriteLine($"Exception: {e.ToString()}");
                                continue;
                            }
                            //Check if input within range, output error and restart loop if not
                            if (removeOption <= 0 || removeOption > currentOrder.IceCreamList.Count())
                            {
                                Console.WriteLine($"Input out of range, please enter an integer within 1 and {currentOrder.IceCreamList.Count()} inclusive.");
                                continue;
                            }
                            else { break; }
                        }
                        currentOrder.RemoveIceCream(removeOption);
                        break;
                    }
                }
        }
        displayCurrentItems(currentOrder);
    }
    //Code to run if it there is no current order
    else
    {
        Console.WriteLine("This customer does not have a current order.");
    }
}

//Function to display current order
void displayCurrentItems(Order currentOrder)
{   
    
    List<IceCream> itemList = currentOrder.IceCreamList;
    for (int i = 0; i < itemList.Count(); i++)
    {
        IceCream iceCream = itemList[i];
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

        string baseOut = $"[{i + 1}]\nOption: {iceCream.Option}\nScoops: {iceCream.Scoops}\n---------\nFlavours\n---------{allFlavours}\n---------\nToppings\n---------{allToppings}\n---------";

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
//ModifyOrder(customerList);

//Interface - Clive + Darius
void DisplayInterface()
{
    Console.WriteLine("Options\n---------\n[1] List all customers\n[2] List all current orders\n[3] Register a new customer\n[4] Create a customer's order\n[5] Display order details\n[6] Modify order details\n[7] Process order\n[8] Display financial breakdown\n[0] Exit");
    int option = 0;
    while (true)
    {
        Console.Write("Enter your option: ");
        try
        {
            option = Convert.ToInt32(Console.ReadLine());
        }
        //Check if input in correct format, output error and restart loop if not
        catch (FormatException)
        {
            Console.WriteLine("Invalid input format, please enter an integer within 0 and 6 inclusive.");
            continue;
        }
        catch (Exception e)
        { 
            Console.WriteLine($"Exception: {e.ToString()}");
            continue;
        }
        if (option < 0 || option > 8)
        {
            Console.WriteLine("Input out of range, please enter an integer within 0 and 6 inclusive.");
            continue;
        }
        else { break; }
    }

    switch (option)
    {
        case 0:
            {
                Console.WriteLine("Exiting program.");
                break;
            }
        case 1:
            {
                ListAllCustomers();
                break;
            }
        case 2:
            {
                ListCurrentOrders(goldQueue, normalQueue);
                break;
            }
        case 3:
            {
                RegisterNewCustomer();
                break;
            }
        case 4:
            {
                CreateCustomerOrder();
                break;
            }
        case 5:
            {
                ListAllOrders(customerList);
                break;
            }
        case 6:
            {
                ModifyOrder(customerList);
                break;
            }
        case 7:
            {
                break;
            }
        case 8:
            {
                DisplayBreakDown();
                break;
            }
        default:
            {
                Console.WriteLine("Default case, something went wrong.");
                break;
            }
    }
    if (option != 0)
    {
        DisplayInterface();
    }
}

DisplayInterface();
// Advanced (A) : 


// Advanced (B) Clive: 
void DisplayBreakDown()
{
    int year = 0;
    //input validation for year
    while (true)
    {
        //Create a datetime object to store the current date for comparison
        DateTime currentDate = DateTime.Now;
        Console.Write("Enter the year: ");
        try
        {
            year = Convert.ToInt32(Console.ReadLine());
        }
        //Check if input is in correct format, output error and restart loop if not
        catch (FormatException)
        {
            Console.WriteLine($"Invalid input format, please input an integer (maximum: {currentDate.Year}).");
            continue;
        }
        //Catch unexpected error, restart loop
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.ToString()}");
            continue;
        }
        //Check if the year being queried is not more than the current year
        if (year > currentDate.Year)
        {
            Console.WriteLine($"Input out of range, please input an integer (maxiumum: {currentDate.Year}).");
        }
        else { break; }
    }
    //Create a list to store all orders for a specific year
    List<Order> yearOrder = new List<Order>();

    foreach (Order order in orderList) 
    {
        //Store the time of completion of the order
        DateTime fulfill = Convert.ToDateTime(order.TimeFulfilled);
        //Check if the order was completed in the year being queried, add to list if it is
        if (fulfill.Year == year)
        {
            yearOrder.Add(order);
        }
    }
    //Sort list by date
    yearOrder.Sort();

    int month = 0;
    //Create list to store month names
    List<string> months = new List<string>();
    //Create list to store total amount for each month
    List<double> monthTotals = new List<double>();

    //Generate 12 elements in monthTotals and all month names in months
    for (int i = 0; i<12; i++)
    {
        monthTotals.Add(0);
        //Create temporary datetime object 
        DateTime tempDate = new DateTime(2024, i+1, 1);
        //Store the month name as a string using the tostring method of datetime objects, specifying abbreviated month
        string monthName = tempDate.ToString("MMM");
        months.Add(monthName);
    }

    foreach (Order order in yearOrder)
    {
        //Store the month of the currentorder as an integer
        int orderMonth = Convert.ToDateTime(order.TimeFulfilled).Month;
        //Set the month ro order month if it is not equal to any month
        if (month == 0)
        {
            month = (orderMonth);
        }
        //Change the month if it is not equal to the current order's month
        else if (orderMonth != month)
        {
            month = orderMonth;
        }
        //Total the amount for all ice creams in the current order and add to the total of the corresponding month
        foreach(IceCream iceCream in order.IceCreamList)
        {
            monthTotals[month-1] += iceCream.CalculatePrice();
        }
    }

    //Print out total amount by month
    for (int i = 0; i<monthTotals.Count;i++) 
    {
        Console.WriteLine($"{months[i]} {year}{":",-5}${monthTotals[i]:0.00}");
    }
    //Print out total amount for a year
    Console.WriteLine($"{"Total",-13}${monthTotals.Sum():0.00}");
}
