using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CQRS.WebAPI.DTO
{
    public class CreateLocationDTO
    {
        public string UNCode { get; set; }
        public string Name { get; set; }
    }
}