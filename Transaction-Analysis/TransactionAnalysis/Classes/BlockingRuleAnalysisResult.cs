using TransactionAnalysis.Domain.Enums;

namespace TransactionAnalysis.Domain.Classes
{
    public class BlockingRuleAnalysisResult
    {
        public Severity Severity { get; set; }
        public string Reason { get; set; }
    }
}
