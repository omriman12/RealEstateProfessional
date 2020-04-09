using System;
using System.Collections.Generic;

namespace SouthRealEstate.DAL.Entities
{
    public partial class Agents
    {
        public Agents()
        {
            PropertiesResidental = new HashSet<PropertiesResidental>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Details { get; set; }
        public string ImageName { get; set; }

        public virtual ICollection<PropertiesResidental> PropertiesResidental { get; set; }
    }
}
