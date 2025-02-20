using TransactionAnalysis.Domain.Classes;

namespace TransactionAnalysis.Domain.Interfaces
{
    public interface IExemptionRule
    {
        ExemptionRuleAnalysisResult CheckExemption(ITransactionAdapter transaction);
    }
}
