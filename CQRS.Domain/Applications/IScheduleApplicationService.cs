using CQRS.Domain.Models.VoyageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Applications
{
    public interface IScheduleApplicationService
    {
        Task DelayScheduleAsync(VoyageId voyageId, TimeSpan delay, CancellationToken cancellationToken);
    }
}