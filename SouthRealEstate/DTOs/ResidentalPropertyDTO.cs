using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthRealEstate.DTOs
{
    public class ResidentalPropertyDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int SizeMeters { get; set; }
        public int BadRoomsCount { get; set; }
        public int BathRoomsCount { get; set; }
        public int Price { get; set; }
        public bool IsNew { get; set; }
        public bool IsFeatured { get; set; }
        public KeyValuePair<string, string> AdditionalFeatures { get; set; }
        public IEnumerable<string> PropertyImages { get; set; }

    }
}
