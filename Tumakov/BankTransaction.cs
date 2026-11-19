namespace Tumakov
{
    internal class BankTransaction
    {
        public readonly DateTime dateTime;
        public readonly decimal sum;
        public BankTransaction(decimal sum)
        {
            dateTime = DateTime.Now;
            this.sum = sum;
        }
    }
}