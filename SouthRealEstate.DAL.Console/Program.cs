using SouthRealEstate.DAL.Entities;
using System;
using System.Collections.Generic;

namespace SouthRealEstate.DAL.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = @"server=127.0.0.1;port=3306;user id=user1; password=1qaz2wsx; database=realestate; pooling=true; CharSet=utf8; Allow User Variables=True; Convert Zero Datetime=True; default command timeout=720";
            var realEstateDbServices = new RealEstateDbServices(conString);

            var propertiesResidental = new PropertiesResidental()
            {
                CityId = 1,
                Title = "app 1",
                Address = "modiin 1",
                Description = "great app desc",
                BadRoomsCount = 4,
                BathRoomsCount = 1,
                Price = 1450000,
                SizeMeters = 150,
                PropertiesResidentialImages = new List<PropertiesResidentialImages>()
                {
                    new PropertiesResidentialImages()
                    {
                        ImageName = "feature2"
                    }
                }
            };
            realEstateDbServices.AddUpdateResidentalPropertyAsync(propertiesResidental).Wait();
        }
    }
}
