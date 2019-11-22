using SouthRealEstate.DAL.Entities;
using SouthRealEstate.DAL.Interfaces;
using SouthRealEstate.Interfaces;
using SouthRealEstate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthRealEstate.Logic
{
    public class PropertiesLogic : IPropertiesLogic
    {
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(PropertiesLogic));

        private readonly IRealEstateDbServices m_RealEstateDbServices;

        public PropertiesLogic(IRealEstateDbServices realEstateDbServices)
        {
            m_RealEstateDbServices = realEstateDbServices;
        }

        public async Task<IEnumerable<Cities>> GetCityEntitiesAsync()
        {
            
            try
            {
                return await m_RealEstateDbServices.GetAllCitiesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IEnumerable<PropertiesResidental>> GetAllFeautredResidentalPropertiesAsync()
        {

            try
            {
                var properties =  await m_RealEstateDbServices.GetAllResidentalPropertiesAsync();
                return properties.Where(x => x.IsFeatured);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<PropertiesResidental>> GetAllResidentalPropertiesAsync()
        {

            try
            {
                return await m_RealEstateDbServices.GetAllResidentalPropertiesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<PropertiesResidental>> SearchPropertyAsync(SearchProperty searchProperty)
        {
            try
            {
                return await m_RealEstateDbServices.SearchPropertyAsync(searchProperty);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<PropertiesResidental> AddResidentalPropertyAsync(PropertiesResidental propertiesResidental)
        {
            PropertiesResidental retVal;

            try
            {
                retVal = await m_RealEstateDbServices.AddUpdateResidentalPropertyAsync(propertiesResidental);
            }
            catch (Exception)
            {
                throw;
            }

            return retVal;
        }

        public async Task<PropertiesResidental> UpdateResidentalPropertyAsync(PropertiesResidental propertiesResidental)
        {
            PropertiesResidental retVal;

            try
            {
                retVal = await m_RealEstateDbServices.AddUpdateResidentalPropertyAsync(propertiesResidental);
            }
            catch (Exception)
            {
                throw;
            }

            return retVal;
        }

        public async Task DeleteResidentalPropertyAsync(int propertiesResidentalId)
        {
            try
            {
                await m_RealEstateDbServices.DeleteResidentalPropertyAsync(propertiesResidentalId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Agents>> GetAllAgentsAsync()
        {
            try
            {
                return await m_RealEstateDbServices.GetAllAgentsAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Agents> AddUpdateAgentsAsync(Agents agent)
        {

            try
            {
                return await m_RealEstateDbServices.AddUpdateAgentsAsync(agent);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAgentAsync(int agentId)
        {
            try
            {
                await m_RealEstateDbServices.DeleteAgentAsync(agentId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
