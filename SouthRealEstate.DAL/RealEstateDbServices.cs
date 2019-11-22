using Microsoft.EntityFrameworkCore;
using SouthRealEstate.DAL.Entities;
using SouthRealEstate.DAL.Interfaces;
using SouthRealEstate.Model;
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
        public async Task<IEnumerable<PropertiesResidental>> SearchPropertyAsync(SearchProperty searchProperty)
        {
            IEnumerable<PropertiesResidental> retVal = null;

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<RealestateContext>();
                optionsBuilder.UseMySql(m_ConString);

                using (var context = new RealestateContext(optionsBuilder.Options))
                {
                    var searchQueriable = context.PropertiesResidental.Where(x=> true);

                    /* badrooms */
                    if (searchProperty.BadRoomsCountFrom.HasValue)
                    {
                        searchQueriable = searchQueriable.Where(x => x.BadRoomsCount >= searchProperty.BadRoomsCountFrom.Value);
                    }

                    if (searchProperty.BadRoomsCountTo.HasValue)
                    {
                        searchQueriable = searchQueriable.Where(x => x.BadRoomsCount <= searchProperty.BadRoomsCountTo.Value);
                    }

                    /* size */
                    if (searchProperty.SizeMetersFrom.HasValue)
                    {
                        searchQueriable = searchQueriable.Where(x => x.SizeMeters >= searchProperty.SizeMetersFrom.Value);
                    }

                    if (searchProperty.SizeMetersTo.HasValue)
                    {
                        searchQueriable = searchQueriable.Where(x => x.SizeMeters <= searchProperty.SizeMetersTo.Value);
                    }


                    /* price */
                    if (searchProperty.PriceFrom.HasValue)
                    {
                        searchQueriable = searchQueriable.Where(x => x.Price >= searchProperty.PriceFrom.Value);
                    }

                    if (searchProperty.PriceTo.HasValue)
                    {
                        searchQueriable = searchQueriable.Where(x => x.Price <= searchProperty.PriceTo.Value);
                    }

                    /* city */
                    if (searchProperty.CityId.HasValue)
                    {
                        searchQueriable = searchQueriable.Where(x => x.CityId == searchProperty.CityId.Value);
                    }

                    /* free search */
                    if (!string.IsNullOrWhiteSpace(searchProperty.FreeSearch))
                    {
                        searchQueriable = searchQueriable.Where(x => x.Title.Contains(searchProperty.FreeSearch)
                         || x.Description.Contains(searchProperty.FreeSearch)
                         || x.Address.Contains(searchProperty.FreeSearch));
                    }

                    retVal = await searchQueriable
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

        public async Task<IEnumerable<Agents>> GetAllAgentsAsync()
        {
            IEnumerable<Agents> retVal = null;

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<RealestateContext>();
                optionsBuilder.UseMySql(m_ConString);

                using (var context = new RealestateContext(optionsBuilder.Options))
                {
                    retVal = await context.Agents.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error($"error occurred during get all agents", ex);
                throw;
            }

            return retVal;
        }
        public async Task<Agents> AddUpdateAgentsAsync(Agents agent)
        {
            Agents retVal = null;

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<RealestateContext>();
                optionsBuilder.UseMySql(m_ConString);

                using (var context = new RealestateContext(optionsBuilder.Options))
                {
                    var agentDB = await context.Agents.Where(x => x.Name == agent.Name).FirstOrDefaultAsync();
                    if (agentDB == null)
                    {
                        context.Agents.Add(agent);
                        retVal = agent;
                    }
                    else
                    {
                        agentDB.Phone = agent.Phone;
                        agentDB.Email = agent.Email;
                        agentDB.Details = agent.Details;
                        agentDB.ImageName = agent.ImageName;
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error($"error occurred during add update agent", ex);
                throw;
            }

            return retVal;
        }
        public async Task DeleteAgentAsync(int agentId)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<RealestateContext>();
                optionsBuilder.UseMySql(m_ConString);

                using (var context = new RealestateContext(optionsBuilder.Options))
                {
                    context.RemoveRange(context.Agents.Where(x => x.Id == agentId));
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error($"error occurred during delete agent by id:{agentId}", ex);
                throw;
            }
        }

    }
}
