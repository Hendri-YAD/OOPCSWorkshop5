using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPCSWorkshop5
{
    class Program
    {
        static void Main(string[] args)
        {



        }
    }

    public class BankAccount
    {
        //
        //attributes
        //

        private string acctNum;
        private Customer acctName;
        protected double balance;

        //
        //Constructor
        //

        public BankAccount(string num, Customer name, double balance)
        {
            acctNum = num;
            acctName = name;
            this.balance = balance;
        }

        public BankAccount() : this("000-000-000", new Customer(), 0)
        {
        }

        //
        //Properties
        //

        public string AccountNumber
        {
            get { return AccountNumber; }
        }

        public Customer AccountName
        {
            get { return acctName; }
            set { acctName = value; }
        }

        public double Balance
        {
            get { return balance; }
        }

        //
        // Methods
        //

        public virtual bool Withdraw(double amount)
        {
            if (balance < amount)
            {
                Console.Error.WriteLine("Withdraw for {0} is unsuccessful", AccountName);
                return false;
            }

            else
            {
                balance -= amount;
                return true;
            }
        }

        public void Deposit(double amount)
        {
            balance += amount;
        }

        public bool TransferTo(double amount, BankAccount another)
        {
            if (Withdraw(amount))
            {
                another.Deposit(amount);
                return true;
            }
            else
            {
                Console.Error.WriteLine("TransferTo for {0} is unsuccessful", AccountName);
                return false;
            }
        }

        public virtual double CalculateInterest()
        {
            return Balance * 0.01;
        }

        public void CreditInterest()
        {
            Deposit(CalculateInterest());
        }

        public override string ToString()        
        {
            string m = String.Format("[Account number = {0}, account holder = {1}, account's balance = {2}]", acctNum, acctName.Show(), balance);
            return m;
        }
    }

    public class SavingsAccount : BankAccount
    {
        private static double interestRate = 1;
        public SavingsAccount (string number, Customer holder, double bal) : base(number, holder, bal)
        {
        }

        public override double CalculateInterest()
        {
            return Balance * interestRate / 100;
        }

        public override bool Withdraw(double amount)
        {
            if (amount < balance)
                return base.Withdraw(amount);
            else
            {
                Console.WriteLine("Unable to withdraw");
                return false;
            }
        }

        public override string ToString()
        {
            string m = String.Format("[SavingsAccount:accountNumber={0},accountHolder={1},balance={2}]",
                AccountNumber, AccountName.Show(), Balance);
            return (m);
        }

    }

    public class CurrentAccount : BankAccount
    {
        private static double interestRate = 0.25;

        public CurrentAccount(string number, Customer holder, double bal) : base(number, holder, bal)
        {
        }

        public override double CalculateInterest()
        {
            return Balance * interestRate / 100;
        }

        public override bool Withdraw(double amount)
        {
            if (amount < Balance)
                return base.Withdraw(amount);
            else
            {
                Console.WriteLine("Cannot withdraw");
                return false;
            }
        }

        public override string ToString()       
        {
            string m = String.Format("[Current account number = {0}, account holder = {1}, account's balance = {2}]", AccountNumber, AccountName.Show(), Balance);
            return m;
        }

    }

    public class OverdraftAccount : BankAccount
    {
        private static double interestRate = 0.25;
        private static double overdraftInterest = 6;

        public OverdraftAccount(string num, Customer holder, double bal) : base(num, holder, bal)
        {
        }

        public override bool Withdraw(double amount)
        {
            balance -= amount;
            return true;
        }

        public override double CalculateInterest()
        {
            return (balance > 0) ? (balance * interestRate / 100) : (balance * overdraftInterest / 100);
        }

        public override string ToString()
        {
            string m = String.Format("[Overdraft account number = {0}," +
                " account holder = {1}, account's balance = {2}]", AccountNumber, AccountName.Show(), Balance);
            return m;
        }
    }

    public class Customer
    {
        // Attributes
        private string name;
        private string address;
        private string passport;
        private int age;

        // Constructor
        public Customer(string name, string address, string passport, int age)
        {
            this.name = name;
            this.address = address;
            this.passport = passport;
            this.age = age;
        }

        public Customer(string name)
            : this(name, "ThisAddress", "ThisPassport", 0)
        {
        }

        public Customer()
            : this("ThisName", "ThisAddress", "ThisPassport", 0)
        {
        }

        // Properties
        public string Name
        {
            get
            {
                return name;
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }
        public string Passport
        {
            get
            {
                return passport;
            }
            set
            {
                passport = value;
            }
        }
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        // Methods

        public void GrowOld()
        {
            age = age + 1;
        }

        public override string ToString()
        {
            string m = String.Format("[Customer:name={0},address={1},passport={2},age={3}]",
                Name, Address, Passport, Age);
            return (m);
        }
    }

    public class BankBranch
    {
        private string name;
        private string manager;
        private ArrayList accounts;

        public string Name
        {
            get { return name; }
        }       

        public string Manager
        {
            get { return manager; }
        }

        public BankBranch(string n, string m)
        {
            name = n;
            manager = m;
            accounts = new ArrayList();
        }

        public void AddAccount (BankAccount a)
        {
            accounts.Add(a);
        }

        public void PrintAccounts()
        {
            for (int i = 0; i < accounts.Count; i++)
                Console.WriteLine(accounts[i]);
        }

        public void PrintCustomers()
        {
            ArrayList cust = new ArrayList();
            for (int i = 0; i < accounts.Count; i++)
            {
                BankAccount a = (BankAccount) accounts[i];
                Customer t = a.AccountName;
                int c = cust.IndexOf(t);
                if (c < 0)
                    cust.Add(t);

            }

        }


    }


}
