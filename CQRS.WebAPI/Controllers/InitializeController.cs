using CQRS.Domain.Models.LocationModel;
using CQRS.Domain.Models.VoyageModel;
using CQRS.Domain.Models.VoyageModel.Commands;
using CQRS.Domain.Models.VoyageModel.ValueObjects;
using CQRS.WebAPI.DTO;
using EventFlow;
using EventFlow.Queries;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CQRS.WebAPI.Controllers
{
    [RoutePrefix("api/initialize")]
    public class InitializeController : ApiController
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public InitializeController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpPost]
        [Route("locations")]
        public async Task<IHttpActionResult> InitilaizeLocations()
        {
            foreach (var item in Locations.GetLocations())
            {
                await new LocationController(_commandBus, _queryProcessor).Post(new CreateLocationDTO { UNCode = item.Id.Value, Name = item.Name });
            }

            return Ok();
        }

        [HttpPost]
        [Route("voyages")]
        public async Task<IHttpActionResult> InitilaizeVoyages()
        {
            var t = Task.WhenAll(Voyages.GetVoyages().Select(CreateVoyageAggregateAsync));

            return Ok();
        }

        public Task CreateVoyageAggregateAsync(Voyage voyage)
        {
            return _commandBus.PublishAsync(new VoyageCreateCommand(voyage.Id, voyage.Schedule), CancellationToken.None);
        }

        public static class Locations
        {
            public static readonly LocationId Hongkong = new LocationId("CNHKG");
            public static readonly LocationId Melbourne = new LocationId("AUMEL");
            public static readonly LocationId Stockholm = new LocationId("SESTO");
            public static readonly LocationId Helsinki = new LocationId("FIHEL");
            public static readonly LocationId Chicago = new LocationId("USCHI");
            public static readonly LocationId Tokyo = new LocationId("JNTKO");
            public static readonly LocationId Hamburg = new LocationId("DEHAM");
            public static readonly LocationId Shanghai = new LocationId("CNSHA");
            public static readonly LocationId Rotterdam = new LocationId("NLRTM");
            public static readonly LocationId Gothenburg = new LocationId("SEGOT");
            public static readonly LocationId Hangzou = new LocationId("CNHGH");
            public static readonly LocationId NewYork = new LocationId("USNYC");
            public static readonly LocationId Dallas = new LocationId("USDAL");

            public static IEnumerable<Location> GetLocations()
            {
                var fieldInfos = typeof(Locations).GetFields(BindingFlags.Public | BindingFlags.Static);
                return fieldInfos.Select(fi => new Location((LocationId)fi.GetValue(null), fi.Name));
            }
        }

        public static class Voyages
        {
            // 0100S: Hongkong - Hangzou - Tokyo - Melbourne - New York (by ship)
            public static VoyageId HongkongToNewYorkId = new VoyageId("0100S");

            public static Schedule HongkongToNewYorkSchedule = new ScheduleBuilder(Locations.Hongkong)
                .Add(Locations.Hangzou, 1.October(2008).At(12, 00), 3.October(2008).At(14, 30))
                .Add(Locations.Tokyo, 3.October(2008).At(21, 00), 6.October(2008).At(06, 15))
                .Add(Locations.Melbourne, 6.October(2008).At(11, 00), 12.October(2008).At(11, 30))
                .Add(Locations.NewYork, 14.October(2008).At(12, 00), 23.October(2008).At(23, 10))
                .Build();

            // 0200T: New York - Chicago - Dallas (by train)
            public static VoyageId NewYorkToDallasId = new VoyageId("0200T");

            public static Schedule NewYorkToDallasSchedule = new ScheduleBuilder(Locations.NewYork)
                .Add(Locations.Chicago, 24.October(2008).At(07, 00), 24.October(2008).At(17, 45))
                .Add(Locations.Dallas, 24.October(2008).At(21, 25), 25.October(2008).At(19, 30))
                .Build();

            // 0300A: Dallas - Hamburg - Stockholm - Helsinki (by airplane)
            public static VoyageId DallasToHelsinkiId = new VoyageId("0300A");

            public static Schedule DallasToHelsinkiSchedule = new ScheduleBuilder(Locations.Dallas)
                .Add(Locations.Hamburg, 29.October(2008).At(03, 30), 31.October(2008).At(14, 00))
                .Add(Locations.Stockholm, 1.November(2008).At(15, 20), 1.November(2008).At(18, 40))
                .Add(Locations.Helsinki, 2.November(2008).At(09, 00), 2.November(2008).At(11, 15))
                .Build();

            // 0301S: Dallas - Helsinki (by ship)
            public static VoyageId DallasToHelsinkiAltId = new VoyageId("0301S");

            public static Schedule DallasToHelsinkiAltSchedule = new ScheduleBuilder(Locations.Dallas)
                .Add(Locations.Helsinki, 29.October(2008).At(03, 00), 5.November(2008).At(15, 45))
                .Build();

            // 0400S: Helsinki - Rotterdam - Shanghai - Hongkong (by ship)
            public static VoyageId HelsinkiToHongkongId = new VoyageId("0400S");

            public static Schedule HelsinkiToHongkongSchedule = new ScheduleBuilder(Locations.Helsinki)
                .Add(Locations.Rotterdam, 4.November(2008).At(05, 50), 6.November(2008).At(14, 10))
                .Add(Locations.Shanghai, 10.November(2008).At(21, 45), 22.November(2008).At(16, 40))
                .Add(Locations.Hongkong, 24.November(2008).At(07, 00), 28.November(2008).At(13, 37))
                .Build();

            public static IEnumerable<Voyage> GetVoyages()
            {
                yield return CreateVoyage(HongkongToNewYorkId, HongkongToNewYorkSchedule);
                yield return CreateVoyage(NewYorkToDallasId, NewYorkToDallasSchedule);
                yield return CreateVoyage(DallasToHelsinkiId, DallasToHelsinkiSchedule);
                yield return CreateVoyage(DallasToHelsinkiAltId, DallasToHelsinkiAltSchedule);
                yield return CreateVoyage(HelsinkiToHongkongId, HelsinkiToHongkongSchedule);
            }

            private static Voyage CreateVoyage(VoyageId voyageId, Schedule schedule)
            {
                return new Voyage(voyageId, schedule);
            }
        }
    }
}