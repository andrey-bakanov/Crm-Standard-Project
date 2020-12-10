using Microsoft.Xrm.Sdk;
using System;
using StandardProject.Crm.Services.AccountManagment;
using StandardProject.Crm.Model;

namespace StandardProject.Crm.Plugins.AccountPipeline.Handlers
{
    public sealed class AccountVerificationHandler : PluginEventHandler
    {
        private readonly IAccountVerificationService _accountService;

        public AccountVerificationHandler(IAccountVerificationService service)
        {
            this._accountService = service ?? throw new ArgumentNullException(nameof(service));
        }
        public override void Handle(PluginState state)
        {

            var account = state.GetTarget<Entity>().ToEntity<Account>();

            if (account.AccountCategoryCode == null)
            {
                return;
            }


            var accountImage = state.TryGetPostImage("image").ToEntity<Account>();

            var result = _accountService.Verify(accountImage);
            if (!result.IsValid)
            {
                throw new Exception(String.Format(Messages.InvalidVerification, account.AccountNumber));
            }
        }
    }
}
