using Transaction_Analysis.Entities;
using TransactionAnalysis.Domain.Interfaces;
using TransactionAnalysis.Entities;
using TransactionAnalysis.Enums;

namespace Services.TransactionAnalysis.Adapters
{
    public class TransferAdapter : ITransactionAdapter
    {
        private readonly DateTimeOffset _date;
        private readonly Account _account;
        private readonly Contact _favoredContact;
        private readonly string _favoredBankCode;
        private readonly string _favoredBranch;
        private readonly string _favoredAccount;
        private readonly decimal _value;
        private bool _exempt;

        public TransferAdapter(DateTimeOffset date, Account account, Contact favoredContact, string favoredBankCode, string favoredBranch, string favoredAccount, decimal value, bool exempt)
        {
            _date = date;
            _account = account;
            _favoredContact = favoredContact;
            _favoredBankCode = favoredBankCode;
            _favoredBranch = favoredBranch;
            _favoredAccount = favoredAccount;
            _value = value;
            _exempt = exempt;
        }

        public TransferAdapter(Operation operation)
        {
            _date = operation.UTCCreationTime;
            _account = operation.Account;
            _favoredContact = operation.Contact;
            _favoredBankCode = operation.FavoredBank;
            _favoredBranch = operation.FavoredBranch;
            _favoredAccount = operation.FavoredAccount;
            _value = operation.Value;
        }

        public bool Exempt
        {
            get
            {
                return _exempt;
            }
        }

        public decimal Value
        {
            get
            {
                return _value;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date.DateTime;
            }
        }

        public Account PayerAccount
        {
            get
            {
                return _account;
            }
        }

        public object TransactionObject
        {
            get
            {
                return this;
            }
        }

        public void DefinirIsenta()
        {
            _exempt = true;
        }

        public IList<ITransactionAdapter> GetTransactionsByPeriod(TimeSpan period)
        {
            var startDateUTC = DateTime.UtcNow - period;
            var queryTransacoesPeriodo = GetUserTransfersQueryByPeriod(startDateUTC);

            return queryTransacoesPeriodo
                .Select(t => new TransferAdapter(t))
                .ToList<ITransactionAdapter>();
        }

        public IList<ITransactionAdapter> GetTransactionsCurrentDate()
        {
            var date = DateTime.UtcNow.Date;
            var queryTransacoesPeriodo = GetUserTransfersQueryByPeriod(date);

            return queryTransacoesPeriodo
                .Select(t => new TransferAdapter(t))
                .ToList<ITransactionAdapter>();
        }

        public IList<ITransactionAdapter> GetTransactionsCurrentMonth()
        {
            var date = DateTime.UtcNow.Date;
            var queryTransacoesPeriodo = GetUserTransfersQueryByPeriod(date);

            return queryTransacoesPeriodo
                .Select(t => new TransferAdapter(t))
                .ToList<ITransactionAdapter>();
        }

        private IQueryable<Operation> GetUserTransfersQuery()
        {
            //add database context
            var query = _context.Session.Query<Operation>()
                .Where(oc => oc.TipoOperacao == OperationType.Transfer)
                .OrderByDescending(oc => oc.UTCCreationTime)
                .Take(500);

            return query;
        }

        private IQueryable<Operation> GetUserTransfersQueryByPeriod(DateTime startDateUTC)
        {
            //add database context
            return GetUserTransfersQuery(_context)
                .Where(oc => oc.UTCCreationTime > startDateUTC);
        }
    }
}
