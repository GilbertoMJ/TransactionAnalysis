using TransactionAnalysis.Domain.Classes;

namespace TransactionAnalysis.Domain.Interfaces
{
    public interface IBlockingRule
    {
        BlockingRuleAnalysisResult CheckViolation(ITransactionAdapter transaction);
    }
}
