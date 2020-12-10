using Microsoft.Xrm.Sdk;
using System;
using System.ServiceModel;

namespace StandardProject.Crm.Plugins
{
    /// <summary>
    /// Base class for Plugins
    /// </summary>
    public abstract class BasePlugin : IPlugin
    {
        /// <summary>
        /// Simple configuration (unsecured)
        /// </summary>
        public readonly string UnsecuredConfiguration;

        /// <summary>
        /// Secure configuration
        /// </summary>
        public readonly string SecuredConfiguration;


        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="unsecuredConfiguration">Unsecured configuration.</param>
        /// <param name="securedConfiguration">Secure configuration.</param>
        protected BasePlugin(string unsecuredConfiguration, string securedConfiguration)
        {
            UnsecuredConfiguration = unsecuredConfiguration;
            SecuredConfiguration = securedConfiguration;
        }

        /// <summary>
        /// Base constructor
        /// </summary>
        protected BasePlugin()
        {
        }

        /// <summary>
        /// Querying the CRM Environment
        /// </summary>
        /// <param name="serviceProvider"></param>
        public void Execute(IServiceProvider serviceProvider)
        {
            var state = CreatePluginState(serviceProvider);

            try
            {
                ExecuteInternal(state);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                state.TraceService.Trace(ex.ToString());

                throw new InvalidPluginExecutionException(ex.Message);
            }
            catch (Exception ex)
            {
                state.TraceService.Trace(ex.ToString());

                throw new InvalidPluginExecutionException(ex.Message);
            }
        }


         /// <summary>
         /// Perform componentRegisration
         /// </summary>
         /// <param name="store"></param>
        public abstract void RegisterHandlers(PluginHandlerRegistrator store, PluginState state);

        /// <summary>
        /// Plugin logic
        /// </summary>
        /// <param name="state">Plugin state</param>
        public virtual void ExecuteInternal(PluginState state)
        {
            PluginHandlerRegistrator store = new PluginHandlerRegistrator();
            this.RegisterHandlers(store, state);

            foreach (var cmd in store.CreateHandlers())
            {
                cmd.Handle(state);
            }
        }

        /// <summary>
        /// Create plugin state
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public virtual PluginState CreatePluginState(IServiceProvider serviceProvider)
        {
            return new PluginState(serviceProvider);
        }
    }
}
