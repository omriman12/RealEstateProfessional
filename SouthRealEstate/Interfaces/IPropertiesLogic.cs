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
        Task<IEnumerable<PropertiesResidental>> GetAllFeautredResidentalPropertiesAsync();
        Task<IEnumerable<PropertiesResidental>> GetAllResidentalPropertiesAsync();
        Task<PropertiesResidental> AddResidentalPropertyAsync(PropertiesResidental propertiesResidental);
        Task<PropertiesResidental> UpdateResidentalPropertyAsync(PropertiesResidental propertiesResidental);
        Task DeleteResidentalPropertyAsync(int propertiesResidentalId);
    }
}
