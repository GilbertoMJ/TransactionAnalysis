using TransactionAnalysis.Domain.Classes;
using TransactionAnalysis.Domain.Enums;
using TransactionAnalysis.Domain.Interfaces;

namespace Services.TransactionAnalysis.Rules.Blocking
{
    public class BalanceRule : IBlockingRule
    {
        public readonly decimal _fee;

        public BalanceRule(decimal fee)
        {
            _fee = fee;
        }

        public BlockingRuleAnalysisResult CheckViolation(ITransactionAdapter transaction)
        {
            var balance = transaction.PayerAccount.Balance;
            var value = !transaction.Exempt ? Math.Abs(transaction.Value) + Math.Abs(_fee) : transaction.Value;

            if (value > balance)
            {
                return new BlockingRuleAnalysisResult
                {
                    Severity = Severity.Blocked,
                    Reason = "Insufficient balance to carry out the transaction."
                };
            }

            return null;
        }
    }
}
