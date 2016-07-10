using Autofac;
using Autofac.Integration.WebApi;
using CQRS.WebAPI;
using EventFlow;
using EventFlow.Autofac.Extensions;
using EventFlow.EventStores.Files;
using EventFlow.Extensions;
using EventFlow.Owin.Extensions;
using EventFlow.Owin.Middlewares;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace CQRS.WebAPI
{
    public class Startup
    {
        public Startup()
        {
        }

        public void Configuration(IAppBuilder appBuilder)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterApiControllers(typeof(Startup).Assembly).InstancePerRequest();
            containerBuilder.RegisterType<CommandPublishMiddleware>().InstancePerRequest();

            var storePath = Path.Combine(
                Path.GetTempPath(),
                Guid.NewGuid().ToString());

            var container = EventFlowOptions.New
                .UseAutofacContainerBuilder(containerBuilder)
                //.AddEvents(EventFlowTestHelpers.Assembly)
                //.AddCommandHandlers(EventFlowTestHelpers.Assembly)
                .AddOwinMetadataProviders()
                //.AddCommands(new[] { typeof(ThingyPingCommand) })
                .UseFilesEventStore(FilesEventStoreConfiguration.Create(storePath))
                //.RegisterServices(f => f.Register(r => new DirectoryCleaner(storePath), Lifetime.Singleton))
                .CreateContainer(false);

            //container.Resolve<DirectoryCleaner>();

            var config = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(container),
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly,
            };

            config.ConfigureSwagger();
            config.ConfigureWebApi();

            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);
        }
    }
}