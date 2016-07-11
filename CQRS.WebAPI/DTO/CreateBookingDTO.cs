using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CQRS.WebAPI.DTO
{
    public class CreateBookingDTO
    {
        public string OriginId { get; set; }
        public string DestinationId { get; set; }

        public DateTimeOffset DepartureDate { get; set; }
        public DateTimeOffset ArrivalDate { get; set; }
    }
}