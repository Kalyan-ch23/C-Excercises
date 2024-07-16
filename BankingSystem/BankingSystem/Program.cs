using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class BankingSystem
    {
        public static List<Account> accounts = new List<Account>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nBanking System");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Check Balance");
                Console.WriteLine("5. Display All Accounts");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        createAccount();
                        break;
                    case "2":
                        DepositMoney();
                        break;
                    case "3":
                        WithdrawMoney();
                        break;
                    case "4":
                        CheckBalance();
                        break;
                    case "5":
                        DisplayAllAccounts();
                        break;
                    case "6":
                        Console.WriteLine("Exiting the banking system. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

        }
        public static void createAccount()
        {
            string accountType = "";
            while (true)
            {
                Console.Write("Enter account type (Savings/Current): ");
                accountType = Console.ReadLine().Trim();
                if (accountType.Equals("savings", StringComparison.OrdinalIgnoreCase) ||
                    accountType.Equals("current", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid account type. Please enter 'Savings' or 'Current'.");
                }
            }
            string accountNumber;
            while (true)
            {
                Console.Write("Enter account number (10 digits): ");
                accountNumber = Console.ReadLine().Trim();
                if (accountNumber.Length == 10 && long.TryParse(accountNumber, out _))
                {
                    if (accounts.Exists(acc => acc.AccountNumber == accountNumber))
                    {
                        Console.WriteLine("Account with this number already exists.");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid account number. It must be exactly 10 digits.");
                }
            }
            Console.Write("Enter account holder name: ");
            string accountHolderName = Console.ReadLine();
            Console.Write("Enter initial balance: ");
            if (!double.TryParse(Console.ReadLine(), out double initialBalance) || initialBalance < 0)
            {
                Console.WriteLine("Invalid initial balance.");
                return;
            }
            if (accounts.Exists(acc => acc.AccountNumber == accountNumber))
            {
                Console.WriteLine("Account with this number already exists.");
                return;
            }
            Account account;
            if (accountType.ToLower() == "savings")
            {
                account = new SavingsAccount(accountNumber, accountHolderName, initialBalance);
            }
            else if (accountType.ToLower() == "current")
            {
                account = new CurrentAccount(accountNumber, accountHolderName, initialBalance);
            }
            else
            {
                Console.WriteLine("Invalid account type.");
                return;
            }
            accounts.Add(account);
            Console.WriteLine("Account created successfully!");
        }
        public static void DepositMoney()
        {
            Console.Write("Enter account number :");
            string accountNumber = Console.ReadLine();
            Account account = accounts.Find(acc => acc.AccountNumber == accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }
            Console.Write("Enter the amount to deposit: ");
            if (!double.TryParse(Console.ReadLine(), out double amount) || amount <= 0)
            {
                Console.WriteLine("Invalid deposit amount.");
                return;
            }
            account.Deposit(amount);
        }
        public static void WithdrawMoney()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            Account account = accounts.Find(acc => acc.AccountNumber == accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }
            Console.Write("Enter the amount to withdraw: ");
            if (!double.TryParse(Console.ReadLine(), out double amount) || amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount.");
                return;
            }
            account.Withdraw(amount);
        }
        public static void CheckBalance()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            Account account = accounts.Find(acc => acc.AccountNumber == accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }
            account.CheckBalance();
        }
        public static void DisplayAllAccounts()
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("No accounts to display.");
                return;
            }

            foreach (var account in accounts)
            {
                account.CheckBalance();
            }
        }
    }
    public class Account
    {
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string accountNumer, string name, double intialBalance)
        {
            AccountNumber = accountNumer;
            Name = name;
            Balance = intialBalance;
        }
        public virtual void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }
            Balance
                += amount;
            Console.WriteLine($"Deposit of{amount} successful. New balance is {Balance}");
        }
        public virtual void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }
            if (amount > Balance)
            {
                throw new ArgumentException("insufficient balance");
            }
            Balance -= amount;
            Console.WriteLine($"Withdrawal of {amount} successful. New balance is {Balance}");
        }
        public virtual void CheckBalance()
        {
            Console.WriteLine($"Account Number: {AccountNumber}, Account Holder Name: {Name}, Balance: {Balance}");
        }
    }
    public class SavingsAccount : Account
    {
        public SavingsAccount(string accnumber, string name, double initialBalance) : base(accnumber, name, initialBalance) { }

    }
    public class CurrentAccount : Account
    {
        public CurrentAccount(string accnumber, string name, double initialBalance) : base(accnumber, name, initialBalance) { }
    }
}
