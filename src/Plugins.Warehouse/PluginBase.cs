using Microsoft.Xrm.Sdk;
using System;

namespace Plugins.Warehouse
{
    public interface ILocalPluginContext
    {
        IPluginExecutionContext PluginExecutionContext { get; }
        IOrganizationServiceFactory OrgSvcFactory { get; }
        ITracingService TracingService { get; }
    }

    public abstract class PluginBase : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var pluginExecutionContext = GetRequiredService<IPluginExecutionContext>(serviceProvider);
            var organizationServiceFactory = GetRequiredService<IOrganizationServiceFactory>(serviceProvider);
            var tracingService = GetRequiredService<ITracingService>(serviceProvider);

            ExecuteDataversePlugin(new LocalPluginContext(
                pluginExecutionContext,
                organizationServiceFactory,
                tracingService));
        }

        protected abstract void ExecuteDataversePlugin(ILocalPluginContext localPluginContext);

        private static T GetRequiredService<T>(IServiceProvider serviceProvider) where T : class
        {
            var service = serviceProvider.GetService(typeof(T)) as T;
            if (service == null)
            {
                throw new InvalidPluginExecutionException(
                    $"The plug-in service provider did not supply {typeof(T).Name}.");
            }

            return service;
        }

        private sealed class LocalPluginContext : ILocalPluginContext
        {
            public LocalPluginContext(
                IPluginExecutionContext pluginExecutionContext,
                IOrganizationServiceFactory orgSvcFactory,
                ITracingService tracingService)
            {
                PluginExecutionContext = pluginExecutionContext;
                OrgSvcFactory = orgSvcFactory;
                TracingService = tracingService;
            }

            public IPluginExecutionContext PluginExecutionContext { get; }
            public IOrganizationServiceFactory OrgSvcFactory { get; }
            public ITracingService TracingService { get; }
        }
    }
}
