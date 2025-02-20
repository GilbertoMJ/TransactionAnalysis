using TransactionAnalysis.Domain.Classes;
using TransactionAnalysis.Domain.Enums;
using TransactionAnalysis.Domain.Interfaces;

namespace Services.TransactionAnalysis.Rules.Blocking
{
    public class LimitCurrentDayRule : IBlockingRule
    {
        private readonly int _limit;
        private readonly string _transactionNamePlural;
        private readonly Severity _severity;

        public LimitCurrentDayRule(int limit, string transactionNamePlural, Severity severity)
        {
            _limit = limit;
            _transactionNamePlural = transactionNamePlural;
            _severity = severity;
        }

        public BlockingRuleAnalysisResult CheckViolation(ITransactionAdapter transaction)
        {
            var quantidadeTransacoesDataAtual = transaction.GetTransactionsCurrentDate().Count;

            if (quantidadeTransacoesDataAtual >= _limit)
            {
                return new BlockingRuleAnalysisResult
                {
                    Severity = _severity,
                    Reason = $"The amount of {_transactionNamePlural} exceeds the daily limit."
                };
            }

            return null;
        }
    }
}
