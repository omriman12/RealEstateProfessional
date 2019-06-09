using SouthRealEstate.DAL.Entities;
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
    }
}
