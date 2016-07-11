using Autofac;
using Autofac.Integration.WebApi;
using CQRS.Domain;
using CQRS.Domain.Applications;
using CQRS.Domain.Models.CargoModel;
using CQRS.Domain.Models.CargoModel.Queries;
using CQRS.Domain.Models.LocationModel;
using CQRS.Domain.Models.LocationModel.Queries;
using CQRS.Domain.Models.VoyageModel;
using CQRS.Domain.Models.VoyageModel.Queries;
using CQRS.Domain.Services;
using CQRS.Queries.InMemory.Cargos;
using CQRS.Queries.InMemory.Cargos.Queries;
using CQRS.Queries.InMemory.Locations;
using CQRS.Queries.InMemory.Locations.Queries;
using CQRS.Queries.InMemory.Voyage;
using CQRS.Queries.InMemory.Voyage.Queries;
using CQRS.WebAPI;
using EventFlow;
using EventFlow.Autofac.Extensions;
using EventFlow.Configuration;
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
                "EventFlowTest");

            var container = EventFlowOptions.New
                .UseAutofacContainerBuilder(containerBuilder)
                .AddOwinMetadataProviders()
                .UseFilesEventStore(FilesEventStoreConfiguration.Create(storePath))
                .RegisterServices(f => f.Register(r => new DirectoryCleaner(storePath), Lifetime.Singleton))
                .AddDefaults(typeof(Specs).Assembly)
                .RegisterServices(sr => {
                    sr.Register<IBookingApplicationService, BookingApplicationService>();
                    sr.Register<IScheduleApplicationService, ScheduleApplicationService>();
                    sr.Register<IUpdateItineraryService, UpdateItineraryService>();
                    sr.Register<IRoutingService, RoutingService>();
                })
                .UseInMemoryReadStoreFor<LocationReadModel>()
                .UseInMemoryReadStoreFor<VoyageReadModel>()
                .UseInMemoryReadStoreFor<CargoReadModel>()
                .AddQueryHandler<GetLocationsQueryHandler, GetLocationsQuery, IReadOnlyCollection<Location>>()
                .AddQueryHandler<GetAllVoyagesQueryHandler, GetAllVoyagesQuery, IReadOnlyCollection<Voyage>>()
                .AddQueryHandler<GetVoyagesQueryHandler, GetVoyagesQuery, IReadOnlyCollection<Voyage>>()
                .AddQueryHandler<GetCargosDependentOnVoyageQueryHandler, GetCargosDependentOnVoyageQuery, IReadOnlyCollection<Cargo>>()
                .AddQueryHandler<GetCargosQueryHandler, GetCargosQuery, IReadOnlyCollection<Cargo>>()
                .CreateContainer(false);

            container.Resolve<DirectoryCleaner>();

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

        public class DirectoryCleaner : IDisposable
        {
            private readonly string _path;

            public DirectoryCleaner(string path)
            {
                _path = path;
            }

            public void Dispose()
            {
                if (Directory.Exists(_path))
                {
                    Console.WriteLine("Deleting directory {0}", _path);
                    Directory.Delete(_path, true);
                }
            }
        }
    }
}