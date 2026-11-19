using System.Collections;

namespace Tumakov
{
    internal class BankAccount : IDisposable
    {
        private int accountNumber;
        private decimal balance;
        private BankAccountType accountType;

        private static int lastAccountNumber = 0;
        private Queue transactions;

        public BankAccount()
        {
            AccountNumber();
            transactions = new Queue();
        }

        public BankAccount(decimal balance)
        {
            this.balance = balance;
            AccountNumber();
            transactions = new Queue();
        }

        public BankAccount(BankAccountType type)
        {
            accountType = type;
            AccountNumber();
            transactions = new Queue();
        }

        public BankAccount(BankAccountType type, decimal balance)
        {
            accountType = type;
            this.balance = balance;
            AccountNumber();
            transactions = new Queue();
        }

        public void Dispose()
        {
            string fileName = $"Transactions_{accountNumber}.txt";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                while (transactions.Count > 0)
                {
                    BankTransaction transaction = (BankTransaction)transactions.Dequeue();
                    writer.WriteLine($"Дата и время: {transaction.dateTime}\nСумма: {transaction.sum}");
                }
            }

            GC.SuppressFinalize(this);
        }

        public void AccountNumber()
        {
            lastAccountNumber++;
            accountNumber = lastAccountNumber;
        }
        public int GetAccountNumber()
        {
            return accountNumber;
        }

        public decimal GetBalance()
        {
            return balance;
        }

        public BankAccountType GetAccountType()
        {
            return accountType;
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                balance += amount;
                BankTransaction transaction = new BankTransaction(amount);
                transactions.Enqueue(transaction);
            }
        }

        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && balance >= amount)
            {
                balance -= amount;
                BankTransaction transaction = new BankTransaction(amount);
                transactions.Enqueue(transaction);
                return true;
            }

            return false;
        }

        public bool Transfer(BankAccount source, decimal amount)
        {
            if (source == null || source == this || amount <= 0)
            {
                return false;
            }

            if (source.Withdraw(amount))
            {
                Deposit(amount);
                return true;
            }

            return false;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Номер счёта: {GetAccountNumber()}");
            Console.WriteLine($"Баланс: {GetBalance()}");
            Console.WriteLine($"Тип счёта: {GetAccountType()}");
        }
    }
}