using SouthRealEstate.DAL.Entities;
using SouthRealEstate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthRealEstate.Interfaces
{
    public interface IPropertiesLogic
    {
        Task<IEnumerable<Cities>> GetCityEntitiesAsync();
        Task<Cities> AddCityAsync(Cities city);
        Task<IEnumerable<PropertiesResidental>> GetAllFeautredResidentalPropertiesAsync();
        Task<IEnumerable<PropertiesResidental>> GetAllResidentalPropertiesAsync();
        Task<PropertiesResidental> GetResidentalPropertyAsync(long propertyId);
        Task<IEnumerable<PropertiesResidental>> SearchPropertyAsync(SearchProperty searchProperty);
        Task<PropertiesResidental> AddResidentalPropertyAsync(PropertiesResidental propertiesResidental);
        Task<PropertiesResidental> UpdateResidentalPropertyAsync(PropertiesResidental propertiesResidental);
        Task DeleteResidentalPropertyAsync(int propertiesResidentalId);

        Task<IEnumerable<Agents>> GetAllAgentsAsync();
        Task<Agents> AddUpdateAgentsAsync(Agents agent);
        Task DeleteAgentAsync(int agentId);
    }
}
