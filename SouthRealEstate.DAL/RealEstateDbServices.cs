using Microsoft.EntityFrameworkCore;
using SouthRealEstate.DAL.Entities;
using SouthRealEstate.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthRealEstate.DAL
{
    public class RealEstateDbServices : IRealEstateDbServices
    {
        private readonly string m_ConString = null;
        /* static */
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(RealEstateDbServices));

        public RealEstateDbServices(string conString)
        {
            m_ConString = conString;
        }

        public async Task<IEnumerable<Cities>> GetAllCitiesAsync()
        {
            IEnumerable<Cities> retVal = null;

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<realestateContext>();
                optionsBuilder.UseMySQL(m_ConString);

                using (var context = new realestateContext(optionsBuilder.Options))
                {
                    retVal = await context.Cities.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error($"error occurred during get all cities", ex);
                throw;
            }

            return retVal;
        }
        public async Task<IEnumerable<PropertiesResidental>> GetAllResidentalPropertiesAsync()
        {
            IEnumerable<PropertiesResidental> retVal = null;

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<realestateContext>();
                optionsBuilder.UseMySQL(m_ConString);

                using (var context = new realestateContext(optionsBuilder.Options))
                {
                    retVal = await context.PropertiesResidental.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error($"error occurred during get all properties", ex);
                throw;
            }

            return retVal;
        }
    }
}
