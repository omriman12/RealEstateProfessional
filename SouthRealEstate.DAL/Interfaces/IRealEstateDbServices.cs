using SouthRealEstate.DAL.Entities;
using SouthRealEstate.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SouthRealEstate.DAL.Interfaces
{
    public interface IRealEstateDbServices
    {
        Task<IEnumerable<Cities>> GetAllCitiesAsync();
        Task<IEnumerable<PropertiesResidental>> GetAllResidentalPropertiesAsync();
        Task<IEnumerable<PropertiesResidental>> SearchPropertyAsync(SearchProperty searchProperty);
        Task<PropertiesResidental> AddUpdateResidentalPropertyAsync(PropertiesResidental propertiesResidental);
        Task DeleteResidentalPropertyAsync(int propertiesResidentalId);

        Task<IEnumerable<Agents>> GetAllAgentsAsync();
        Task<Agents> AddUpdateAgentsAsync(Agents agent);
        Task DeleteAgentAsync(int agentId);
    }
}
