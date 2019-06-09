using System;

namespace SouthRealEstate.DAL.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = @"server=127.0.0.1;port=3306;user id=user1; password=1qaz2wsx; database=realestate; pooling=true; CharSet=utf8; Allow User Variables=True; Convert Zero Datetime=True; default command timeout=720";
            var realEstateDbServices = new RealEstateDbServices("");
            realEstateDbServices.GetAllCitiesAsync().Wait();
        }
    }
}
