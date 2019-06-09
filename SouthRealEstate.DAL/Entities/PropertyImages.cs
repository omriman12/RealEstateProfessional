using System;
using System.Collections.Generic;

namespace SouthRealEstate.DAL.Entities
{
    public partial class PropertyImages
    {
        public int Id { get; set; }
        public long? PropertyId { get; set; }
        public string ImageName { get; set; }
    }
}
