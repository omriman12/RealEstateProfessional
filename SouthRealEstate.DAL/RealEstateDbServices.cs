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
                var optionsBuilder = new DbContextOptionsBuilder<RealestateContext>();
                optionsBuilder.UseMySql(m_ConString);

                using (var context = new RealestateContext(optionsBuilder.Options))
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
                var optionsBuilder = new DbContextOptionsBuilder<RealestateContext>();
                optionsBuilder.UseMySql(m_ConString);

                using (var context = new RealestateContext(optionsBuilder.Options))
                {
                    retVal = await context.PropertiesResidental
                        .Include(x => x.PropertiesResidentialImages)
                        .Include(x => x.City)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error($"error occurred during get all properties", ex);
                throw;
            }

            return retVal;
        }

        public async Task<PropertiesResidental> AddUpdateResidentalPropertyAsync(PropertiesResidental propertiesResidental)
        {
            PropertiesResidental retVal = null;

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<RealestateContext>();
                optionsBuilder.UseMySql(m_ConString);

                using (var context = new RealestateContext(optionsBuilder.Options))
                {
                    var propertyDB = await context.PropertiesResidental.Where(x => x.Id == propertiesResidental.Id).FirstOrDefaultAsync();
                    if (propertyDB == null)
                    {
                        context.PropertiesResidental.Add(propertiesResidental);
                        retVal = propertiesResidental;
                    }
                    else
                    {
                        propertyDB.Title = propertiesResidental.Title;
                        propertyDB.Description = propertiesResidental.Description;
                        propertyDB.Address = propertiesResidental.Address;
                        propertyDB.CityId = propertiesResidental.CityId;
                        propertyDB.SizeMeters = propertiesResidental.SizeMeters;
                        propertyDB.BadRoomsCount = propertiesResidental.BadRoomsCount;
                        propertyDB.BathRoomsCount = propertiesResidental.BathRoomsCount;
                        propertyDB.Price = propertiesResidental.Price;
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error($"error occurred during add residental property", ex);
                throw;
            }

            return retVal;
        }
        public async Task DeleteResidentalPropertyAsync(int propertiesResidentalId)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<RealestateContext>();
                optionsBuilder.UseMySql(m_ConString);

                using (var context = new RealestateContext(optionsBuilder.Options))
                {
                    context.RemoveRange(context.PropertiesResidental.Where(x=>x.Id == propertiesResidentalId));
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error($"error occurred during delete residental property", ex);
                throw;
            }
        }
    }
}
