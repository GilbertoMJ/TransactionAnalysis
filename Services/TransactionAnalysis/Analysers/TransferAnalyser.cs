using Services.TransactionAnalysis.Rules.Blocking;
using Services.TransactionAnalysis.Rules.Exemption;
using TransactionAnalysis.Domain.Enums;
using TransactionAnalysis.Domain.Interfaces;
using TransactionAnalysis.Entities;

namespace Services.TransactionAnalysis.Analysers
{
    public class TransferAnalyser : TransactionAnalyser
    {
        public static readonly string _transactionName = "transfer";
        public static readonly string _transactionNamePlural = "transfers";
        public static readonly TimeSpan _startWorkTime = new TimeSpan(7, 0, 0);
        public static readonly TimeSpan _endWorkTime = new TimeSpan(17, 0, 0);

        public TransferAnalyser(Plan clientPlan) : base(ConfigureBlockingRules(clientPlan), ConfigureExemptionRules(clientPlan)) { }

        private static List<IBlockingRule> ConfigureBlockingRules(Plan clientPlan)
        {
            return new List<IBlockingRule>
            {
                new BalanceRule(clientPlan.TransferFee),
                new LimitCurrentDayRule(clientPlan.CurrentDayLimit, _transactionNamePlural, Severity.Blocked),
                new MustNotBeZeroRule()
            };
        }

        private static List<IExemptionRule> ConfigureExemptionRules(Plan clientPlan)
        {
            switch (clientPlan.Name)
            {
                case "EspecialPlan":
                    return new List<IExemptionRule>
                    {
                        new MonthlyExemptionRule(4, clientPlan.UTCCreationTime.AddMonths(3), _transactionNamePlural)
                    };
                default:
                    return new List<IExemptionRule>();
            }
        }
    }
}
