using Services.TransactionAnalysis.Adapters;
using Services.TransactionAnalysis.Analysers;
using Transaction_Analysis.Entities;
using TransactionAnalysis.Entities;

namespace Application.Services
{
    public class TransferService
    {
        public async Task CreateTransfer()
        {
            var value = 30.50m;
            var account = new Account();
            var contact = new Contact();
            var plan = new Plan();

            /* Creates a new adapter for the transfer, if necessary for the project */
            var transferAdapter = new TransferAdapter(
                DateTimeOffset.UtcNow,
                account,
                contact,
                contact.Bank,
                contact.Branch,
                contact.Account,
                value,
                false);

            /* Creates the analyser for transfer */
            var transferAnalyser = new TransferAnalyser(plan);

            /* Goes through all the rules to find any irregularities */
            var analysisResult = transferAnalyser.AnalyseTransaction(transferAdapter);

            /* Throws a exception if there is any irregularity */
            if (analysisResult.BlockedTransaction)
                throw new Exception(string.Join("\r\n", analysisResult.BlockingReasons));
        }
    }
}
