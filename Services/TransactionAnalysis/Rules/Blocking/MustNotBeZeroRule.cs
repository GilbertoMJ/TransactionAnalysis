using TransactionAnalysis.Domain.Classes;
using TransactionAnalysis.Domain.Enums;
using TransactionAnalysis.Domain.Interfaces;

namespace Services.TransactionAnalysis.Rules.Blocking
{
    public class MustNotBeZeroRule : IBlockingRule
    {
        public BlockingRuleAnalysisResult CheckViolation(ITransactionAdapter transaction)
        {
            if (transaction.Value <= 0)
            {
                return new BlockingRuleAnalysisResult
                {
                    Reason = "Valor da transação não pode ser zero.",
                    Severity = Severity.Blocked
                };
            }

            return null;
        }
    }
}
