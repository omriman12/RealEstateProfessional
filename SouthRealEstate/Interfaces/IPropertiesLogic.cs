using SouthRealEstate.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthRealEstate.Interfaces
{
    public interface IPropertiesLogic
    {
        Task<IEnumerable<Cities>> GetCityEntitiesAsync();
        Task<IEnumerable<PropertiesResidental>> GetAllResidentalPropertiesAsync();
    }
}
