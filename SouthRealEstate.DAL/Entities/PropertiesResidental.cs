using System;
using System.Collections.Generic;

namespace SouthRealEstate.DAL.Entities
{
    public partial class PropertiesResidental
    {
        public PropertiesResidental()
        {
            PropertiesResidentialImages = new HashSet<PropertiesResidentialImages>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int SizeMeters { get; set; }
        public int BadRoomsCount { get; set; }
        public int BathRoomsCount { get; set; }
        public int Price { get; set; }
        public bool IsFeatured { get; set; }

        public int CityId { get; set; }
        public virtual Cities City { get; set; }
        public virtual ICollection<PropertiesResidentialImages> PropertiesResidentialImages { get; set; }

        public long AgentId { get; set; }
        public virtual Agents Agent { get; set; }
    }
}
