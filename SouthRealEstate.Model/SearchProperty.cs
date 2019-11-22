using System;

namespace SouthRealEstate.Model
{

    public class SearchProperty
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
