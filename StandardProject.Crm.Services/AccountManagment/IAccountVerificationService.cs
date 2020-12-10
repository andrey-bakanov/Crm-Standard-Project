using StandardProject.Crm.Model;

namespace StandardProject.Crm.Services.AccountManagment
{

    /// <summary>
    /// perform account verification
    /// </summary>
    public interface IAccountVerificationService
    {
        /// <summary>
        /// Perform account verification
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        VerificationResult Verify(Account account);

        /// <summary>
        /// Perform verification for all account with specified  category code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></retur
        VerificationResult VerifyAll(Account_AccountCategoryCode code);
    }
}