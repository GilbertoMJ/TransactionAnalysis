using TransactionAnalysis.Domain.Classes;

namespace TransactionAnalysis.Domain.Interfaces
{
    public interface ITransactionAnalyser
    {
        TransactionAnalysisResult AnalyseTransaction(ITransactionAdapter transacao);
    }
}
