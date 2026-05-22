using ConsoleApp1.Data;
using ConsoleApp1.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new BankDbContext();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("       National Bank - Management           ");
                Console.WriteLine("============================================");
                Console.WriteLine("1) Add a new Customer");
                Console.WriteLine("2) Open a new Account for a Customer");
                Console.WriteLine("3) Update Account Status (Active / Closed)");
                Console.WriteLine("4) Remove an Account from a Customer");
                Console.WriteLine("5) List all Customers (with accounts)");
                Console.WriteLine("0) Exit");
                Console.WriteLine("--------------------------------------------");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddCustomer(db); break;
                    case "2": OpenAccount(db); break;
                    case "3": UpdateAccountStatus(db); break;
                    case "4": RemoveAccount(db); break;
                    case "5": ListCustomers(db); break;
                    case "0": exit = true; break;
                    default:
                        Console.WriteLine("Invalid input! Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AddCustomer(BankDbContext db)
        {
            Console.Clear();
            Console.WriteLine("--- Add New Customer ---");
            Console.Write("Full Name: "); string name = Console.ReadLine();
            Console.Write("National ID: "); string nId = Console.ReadLine();
            Console.Write("Date of Birth (yyyy-mm-dd): "); DateTime dob = DateTime.Parse(Console.ReadLine());
            Console.Write("Email: "); string email = Console.ReadLine();
            Console.Write("Phone: "); string phone = Console.ReadLine();
            Console.Write("Address: "); string addr = Console.ReadLine();
            Console.WriteLine("Customer Type: 1) Individual, 2) Business");
            int type = int.Parse(Console.ReadLine());

            var customer = new Customer { FullName = name, NationalId = nId, DateOfBirth = dob, Email = email, PhoneNumber = phone, Address = addr, CustomerType = (CustomerType)type };
            db.Customers.Add(customer);
            db.SaveChanges();

            Console.WriteLine($"\nCustomer created successfully. ID = {customer.Id}");
            Console.WriteLine("Press any key to return..."); Console.ReadKey();
        }

        static void OpenAccount(BankDbContext db)
        {
            Console.Clear();
            Console.WriteLine("--- Open New Account ---");
            Console.Write("Account Number: "); string accNum = Console.ReadLine();
            Console.WriteLine("Account Type: 1) Savings, 2) Current, 3) Business");
            int accType = int.Parse(Console.ReadLine());
            Console.Write("Branch Code (e.g. CAI-01): "); string bCode = Console.ReadLine();
            Console.Write("Customer ID: "); int cId = int.Parse(Console.ReadLine());
            Console.WriteLine("Ownership Role: 1) Primary, 2) CoHolder");
            int role = int.Parse(Console.ReadLine());

            if (!db.Branches.Any(b => b.Code == bCode) || !db.Customers.Any(c => c.Id == cId))
            {
                Console.WriteLine("Error: Branch or Customer not found!");
            }
            else
            {
                var account = new Account { AccountNumber = accNum, AccountType = (AccountType)accType, BranchCode = bCode, OpeningDate = DateTime.Now, CurrentBalance = 0 };
                db.Accounts.Add(account);
                db.CustomerAccounts.Add(new CustomerAccount { AccountNumber = accNum, CustomerId = cId, OwnershipType = (OwnershipType)role, OwnershipStartDate = DateTime.Now, AccountStatus = AccountStatus.Active });
                db.SaveChanges();
                Console.WriteLine("Account opened successfully!");
            }
            Console.WriteLine("Press any key..."); Console.ReadKey();
        }

        static void UpdateAccountStatus(BankDbContext db)
        {
            Console.Clear();
            Console.WriteLine("--- Update Account Status ---");
            Console.Write("Account Number: "); string accNum = Console.ReadLine();
            Console.Write("Customer ID: "); int cId = int.Parse(Console.ReadLine());

            var link = db.CustomerAccounts.FirstOrDefault(ca => ca.AccountNumber == accNum && ca.CustomerId == cId);
            if (link != null)
            {
                link.AccountStatus = (link.AccountStatus == AccountStatus.Active) ? AccountStatus.Closed : AccountStatus.Active;
                db.SaveChanges();
                Console.WriteLine($"Status updated to: {link.AccountStatus}");
            }
            else Console.WriteLine("Record not found!");
            Console.ReadKey();
        }

        static void RemoveAccount(BankDbContext db)
        {
            Console.Clear();
            Console.WriteLine("--- Remove Account from Customer ---");
            Console.Write("Account Number: "); string accNum = Console.ReadLine();
            Console.Write("Customer ID: "); int cId = int.Parse(Console.ReadLine());

            var link = db.CustomerAccounts.FirstOrDefault(ca => ca.AccountNumber == accNum && ca.CustomerId == cId);
            if (link != null)
            {
                db.CustomerAccounts.Remove(link);
                if (!db.CustomerAccounts.Any(ca => ca.AccountNumber == accNum && ca.CustomerId != cId))
                {
                    var acc = db.Accounts.Find(accNum);
                    if (acc != null) db.Accounts.Remove(acc);
                }
                db.SaveChanges();
                Console.WriteLine("Removed successfully!");
            }
            else Console.WriteLine("Not found!");
            Console.ReadKey();
        }

        static void ListCustomers(BankDbContext db)
        {
            Console.Clear();
            Console.WriteLine("--- All Customers and their Accounts ---");
            var list = db.Customers.Include(c => c.CustomerAccounts).ThenInclude(ca => ca.Account).ThenInclude(a => a.Branch).ToList();

            foreach (var c in list)
            {
                Console.WriteLine($"#{c.Id} {c.FullName} ({c.CustomerType})");
                foreach (var ca in c.CustomerAccounts)
                {
                    Console.WriteLine($"   - {ca.AccountNumber} [{ca.Account.AccountType}] Bal: {ca.Account.CurrentBalance:N2} @ {ca.Account.Branch.Name} Status: {ca.AccountStatus}");
                }
                if (!c.CustomerAccounts.Any()) Console.WriteLine("   (No accounts)");
            }
            Console.WriteLine("\nPress any key..."); Console.ReadKey();
        }
    }
}