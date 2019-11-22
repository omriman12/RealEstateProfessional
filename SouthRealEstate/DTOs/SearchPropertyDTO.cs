
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthRealEstate.DTOs
{
    public class SearchPropertyDTO
    {
        public string FreeSearch { get; set; }
        public int? CityId { get; set; }
        public int? PropertyType { get; set; }
        public int? SizeMetersFrom { get; set; }
        public int? SizeMetersTo { get; set; }
        public int? BadRoomsCountFrom { get; set; }
        public int? BadRoomsCountTo { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }

    }
}