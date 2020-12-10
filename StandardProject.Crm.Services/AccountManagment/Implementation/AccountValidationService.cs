using StandardProject.Crm.Logging;
using StandardProject.Crm.Model;
using StandardProject.Crm.Repository;
using System;

namespace StandardProject.Crm.Services.AccountManagment.Implementation
{
    public sealed class AccountVerificationService : IAccountVerificationService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IApplicationLogger _logger;

        public AccountVerificationService(IAccountRepository accountRepository, IApplicationLogger logger)
        {
            this._accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            this._logger = logger;
        }

        /// <summary>
        /// Perform account verification
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public VerificationResult Verify(Account account)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Perform verification for all account with specified  category code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public VerificationResult VerifyAll(Account_AccountCategoryCode code)
        {
            throw new NotImplementedException();
        }
    }
}
