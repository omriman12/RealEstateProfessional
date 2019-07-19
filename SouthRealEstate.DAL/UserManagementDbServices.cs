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
    public class UserManagementDbServices : IUserManagementDbServices
    {
        private readonly string m_ConString = null;
        /* static */
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(UserManagementDbServices));

        public UserManagementDbServices(string conString)
        {
            m_ConString = conString;
        }

        public async Task<UmUsers> AuthenticateUserAsync(string userName, string password)
        {
            UmUsers retVal = null;

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<RealestateContext>();
                optionsBuilder.UseMySql(m_ConString);

                using (var context = new RealestateContext(optionsBuilder.Options))
                {
                    retVal = await context.UmUsers.Where(x=> x.Name == userName && x.Password == password)
                        .FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error($"error occurred during authenticate user", ex);
                throw;
            }

            return retVal;
        }
    }
}
