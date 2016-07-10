using CQRS.Domain.Models.VoyageModel.Events;
using CQRS.Domain.Models.VoyageModel.ValueObjects;
using EventFlow.Aggregates;
using EventFlow.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.VoyageModel
{
    public class VoyageAggregate : AggregateRoot<VoyageAggregate, VoyageId>
    {
        private readonly VoyageState _state = new VoyageState();

        public VoyageAggregate(VoyageId id) : base(id)
        {
            Register(_state);
        }

        public Schedule Schedule => _state.Schedule;

        public void Create(Schedule schedule)
        {
            Specs.AggregateIsNew.ThrowDomainErrorIfNotStatisfied(this);

            Emit(new VoyageCreatedEvent(schedule));
        }

        public void Delay(TimeSpan delay)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);

            if (delay == TimeSpan.Zero) return;

            var delayedSchedule = Schedule.Delay(delay);

            Emit(new VoyageScheduleUpdatedEvent(delayedSchedule));
        }
    }
}