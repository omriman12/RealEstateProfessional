using System;
using System.Collections.Generic;

namespace SouthRealEstate.DAL.Entities
{
    public partial class UmUsers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public byte Role { get; set; }
    }
}
