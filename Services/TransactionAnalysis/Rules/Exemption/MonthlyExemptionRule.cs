using TransactionAnalysis.Domain.Classes;
using TransactionAnalysis.Domain.Interfaces;

namespace Services.TransactionAnalysis.Rules.Exemption
{
    public class MonthlyExemptionRule : IExemptionRule
    {
        private readonly int _limit;
        private readonly DateTime _limitDate;
        private readonly string _transactionNamePlural;

        public MonthlyExemptionRule(int limit, DateTime limitDate, string transactionNamePlural)
        {
            _limit = limit;
            _limitDate = limitDate;
            _transactionNamePlural = transactionNamePlural;
        }

        public ExemptionRuleAnalysisResult CheckExemption(ITransactionAdapter transaction)
        {
            if (transaction.Date <= _limitDate)
            {
                var transacoesDoMes = transaction.GetTransactionsCurrentMonth();

                if (transacoesDoMes.Count < _limit)
                {
                    return new ExemptionRuleAnalysisResult
                    {
                        Reason = $"Customer used {transacoesDoMes.Count} out of {_limit} {_transactionNamePlural} free this month."
                    };
                }
            }

            return null;
        }
    }
}
