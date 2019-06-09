using System;
using System.Collections.Generic;

namespace SouthRealEstate.DAL.Entities
{
    public partial class Cities
    {
        public Cities()
        {
            PropertiesResidental = new HashSet<PropertiesResidental>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PropertiesResidental> PropertiesResidental { get; set; }
    }
}
