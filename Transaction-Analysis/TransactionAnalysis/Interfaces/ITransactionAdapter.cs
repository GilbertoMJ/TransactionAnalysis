using TransactionAnalysis.Entities;

namespace TransactionAnalysis.Domain.Interfaces
{
    public interface ITransactionAdapter
    {
        bool Exempt { get; }
        decimal Value { get; }
        DateTime Date { get; }
        Account PayerAccount { get; }
        Object TransactionObject { get; }

        void DefinirIsenta();
        IList<ITransactionAdapter> GetTransactionsByPeriod(TimeSpan periodo);
        IList<ITransactionAdapter> GetTransactionsCurrentDate();
        IList<ITransactionAdapter> GetTransactionsCurrentMonth();
    }
}
