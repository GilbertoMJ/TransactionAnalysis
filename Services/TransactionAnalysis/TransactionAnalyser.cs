using TransactionAnalysis.Domain.Classes;
using TransactionAnalysis.Domain.Interfaces;

namespace Services.TransactionAnalysis
{
    public class TransactionAnalyser : ITransactionAnalyser
    {
        private readonly List<IBlockingRule> _blockingRules;
        private readonly List<IExemptionRule> _exemptionRules;

        public TransactionAnalyser(List<IBlockingRule> blockingRules, List<IExemptionRule> exemptionRules)
        {
            _blockingRules = blockingRules;
            _exemptionRules = exemptionRules;
        }

        public TransactionAnalysisResult AnalyseTransaction(ITransactionAdapter transacao)
        {
            var result = new TransactionAnalysisResult();

            foreach (var rule in _exemptionRules)
            {
                var resultadoAnaliseRegra = rule.CheckExemption(transacao);
                result.AddExemptionRuleAnalysis(resultadoAnaliseRegra);

                if (result != null && result.ExemptTransaction)
                {
                    Console.WriteLine($"Transaction exempted by rule {rule.GetType().Name}");
                    transacao.DefinirIsenta();
                }
            }

            foreach (var rule in _blockingRules)
            {
                var resultadoAnaliseRegra = rule.CheckViolation(transacao);
                result.AddBlockingRuleAnalysis(resultadoAnaliseRegra);

                if (result != null)
                    if (result.BlockedTransaction)
                        Console.WriteLine($"Transaction blocked by rule {rule.GetType().Name}");
                    else if (result.ExemptTransaction)
                        Console.WriteLine($"Transaction considered suspicious by rule {rule.GetType().Name}");
            }

            return result;
        }
    }
}
