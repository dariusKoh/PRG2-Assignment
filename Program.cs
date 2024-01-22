//==========================================================
// Student Name : Giam Jun Xian Clive (S10257799G)
// Student Name : Darius Koh Kai Keat (S10255626K)
//==========================================================

// Basic 1 : Darius
using PRG2_Assignment.classes;
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


// Basic 3 : Darius


// Basic 4 : Darius


// Basic 5 : Clive


// Basic 6 : Clive


// Advanced (A) : 


// Advanced (B) : 