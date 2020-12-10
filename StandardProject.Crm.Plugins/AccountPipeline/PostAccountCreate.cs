using StandardProject.Crm.Logging;
using StandardProject.Crm.Logging.Implementation;
using StandardProject.Crm.Plugins.AccountPipeline.Handlers;
using StandardProject.Crm.Repository;
using StandardProject.Crm.Repository.Implementation;
using StandardProject.Crm.Services.AccountManagment;
using StandardProject.Crm.Services.AccountManagment.Implementation;

namespace StandardProject.Crm.Plugins.AccountPipeline
{
    public sealed class PostAccountCreate : BasePlugin
    {
        public override void RegisterHandlers(PluginHandlerRegistrator store, PluginState state)
        {
            //Выполняется настройка необходимых зависимостей, возможно IoC
            IAccountRepository accountRepository = new AccountRepository(state.Service);
            IApplicationLogger logger = new TracingLogger(state.TraceService);

            IAccountVerificationService svc = new AccountVerificationService(accountRepository, logger);

            store.Register(new AccountVerificationHandler(svc));
        }
    }
}
