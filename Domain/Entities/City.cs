using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class City:BaseEntity
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }
}
