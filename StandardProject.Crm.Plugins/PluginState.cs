using Microsoft.Xrm.Sdk;
using System;

namespace StandardProject.Crm.Plugins
{
    // <summary>
    /// Plugin state, methods for getting services and images
    /// </summary>
    public class PluginState
    {
        /// <summary>
        /// Organization Service
        /// </summary>
        private IOrganizationService _service;

        /// <summary>
        /// Tracing service
        /// </summary>
        private ITracingService _trace;


        /// <summary>
        /// Plugin Context
        /// </summary>
        public IPluginExecutionContext Context { get; }

        /// <summary>
        /// Plugin Context
        /// </summary>
        public IPluginExecutionContext ParentContext { get; }

        /// <summary>
        /// Write Service
        /// </summary>
        public ITracingService TraceService =>
            _trace ?? (_trace = (ITracingService)Provider.GetService(typeof(ITracingService)));

        /// <summary>
        /// Service Provider
        /// </summary>
        public IServiceProvider Provider { get; }

        /// <summary>
        /// Organization service
        /// </summary>
        public IOrganizationService Service
        {
            get
            {
                if (_service != null)
                {
                    return _service;
                }

                var factory =
                    (IOrganizationServiceFactory)Provider.GetService(typeof(IOrganizationServiceFactory));

                _service = factory.CreateOrganizationService(Context.UserId);

                return _service;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="provider">IServiceProvider</param>
        public PluginState(IServiceProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));

            Context = (IPluginExecutionContext)Provider.GetService(typeof(IPluginExecutionContext));
            ParentContext = Context.ParentContext;
        }

        /// <summary>
        /// Get Post Image
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Post Image Entity if image is not null</returns>
        public Entity GetPostImage(string name)
        {
            var image = TryGetPostImage(name);
            if (image == null)
            {
                throw new ApplicationException("Waits for Post image with name " + name);
            }

            return image;
        }

        /// <summary>
        /// Try get Post image
        /// </summary>
        /// <param name="name">Image alias</param>
        /// <returns>Post image Entity if Context contains image alias,
        /// otherwise - null</returns>
        public Entity TryGetPostImage(string name)
        {
            if (Context.PostEntityImages.Contains(name))
            {
                return Context.PostEntityImages[name];
            }

            return null;
        }

        /// <summary>
        /// Get Pre Image
        /// </summary>
        /// <param name="name">Image alias</param>
        /// <returns>Pre Image Entity if image is not null</returns>
        public Entity GetPreImage(string name)
        {
            var image = TryGetPreImage(name);

            if (image == null)
            {
                throw new ApplicationException("Waits for Pre image with name " + name);
            }

            return image;
        }

        /// <summary>
        /// Try get Pre Image
        /// </summary>
        /// <param name="name">Image alias</param>
        /// <returns>Pre image Entity if Context contains image alias,
        /// otherwise - null</returns>
        public Entity TryGetPreImage(string name)
        {
            if (Context.PreEntityImages.Contains(name))
            {
                return Context.PreEntityImages[name];
            }

            return null;
        }

        /// <summary>
        /// Get Target
        /// </summary>
        /// <returns>Target Entity</returns>
        public T GetTarget<T>() where T : class
        {
            if (Context.InputParameters.Contains("Target"))
            {
                return (T)Context.InputParameters["Target"];
            }

            return default(T);
        }


        /// <summary>
        /// Get Target
        /// </summary>
        /// <returns>Target Entity</returns>
        public T GetTargetEntity<T>() where T : Entity
        {
            return GetTarget<Entity>()?.ToEntity<T>();
        }

        /// <summary>
        /// Check is update message
        /// </summary>
        /// <returns>If is it update message - true, else - false.</returns>
        public bool IsUpdate()
        {
            return Context.MessageName.ToUpper() == "UPDATE";
        }

        /// <summary>
        /// Check is create message
        /// </summary>
        /// <returns>If is it update message - true, else - false.</returns>
        public bool IsCreate()
        {
            return Context.MessageName.ToUpper() == "CREATE";
        }

        /// <summary>
        /// Get input parameter by name
        /// </summary>
        /// <typeparam name="T">Parameter type</typeparam>
        /// <param name="parameterName">Input parameter name</param>
        /// <returns>Input parameter or default of <typeparam name="T">T</typeparam></returns>
        public T GetFromInput<T>(string parameterName)
        {
            if (Context.InputParameters.Contains(parameterName))
            {
                return (T)Context.InputParameters[parameterName];
            }

            return default(T);
        }

        /// <summary>
        /// Get output parameter by name
        /// </summary>
        /// <typeparam name="T">Parameter type</typeparam>
        /// <param name="parameterName">Input parameter name</param>
        /// <returns>Output parameter or default of <typeparam name="T">T</typeparam></returns>
        public T GetFromOutput<T>(string parameterName)
        {
            if (Context.OutputParameters.Contains(parameterName))
            {
                return (T)Context.OutputParameters[parameterName];
            }

            return default(T);
        }

        /// <summary>
        /// Update output value or add output value by key in param <paramref name="parameterName"/>
        /// </summary>
        /// <param name="parameterName">Output parameter name</param>
        /// <param name="value">Output parameter value</param>
        public void SetOutput(string parameterName, object value)
        {
            if (Context.OutputParameters.ContainsKey(parameterName))
            {
                Context.OutputParameters[parameterName] = value;
            }
            else
            {
                Context.OutputParameters.Add(parameterName, value);
            }
        }
    }
}
