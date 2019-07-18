using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SouthRealEstate.DAL.Entities;
using SouthRealEstate.DAL.Interfaces;
using SouthRealEstate.DTOs;
using SouthRealEstate.Interfaces;
using SouthRealEstate.Models;

namespace SouthRealEstate.Controllers
{
    public class PropertiesController : Controller
    {
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(PropertiesController));

        private readonly IPropertiesLogic m_PropertiesLogic;

        public PropertiesController(IPropertiesLogic propertiesLogic)
        {
            m_PropertiesLogic = propertiesLogic;
        }
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SingleProperty()
        {
            return View();
        }

        [Route("api/cities")]
        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            ActionResult retVal = null;

            try
            {
                var cities = await m_PropertiesLogic.GetCityEntitiesAsync();
                retVal = Ok(cities);
            }
            catch (Exception ex)
            {
                s_Logger.Error(ex);
                retVal = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return retVal;
        }

        [Route("api/properties/residental/featured")]
        [HttpGet]
        public async Task<IActionResult> GetAllFeaturedPropertiesAsync()
        {
            ActionResult retVal = null;

            try
            {
                var properties = await m_PropertiesLogic.GetAllFeautredResidentalPropertiesAsync();
                List<FeaturedPropertyDTO> featuredProperties = properties.Select(x => new FeaturedPropertyDTO()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Address = x.Address,
                    CityId = x.CityId,
                    SizeMeters = x.SizeMeters,
                    BadRoomsCount = x.BadRoomsCount,
                    BathRoomsCount = x.BathRoomsCount,
                    Price = x.Price,
                    IsNew = x.IsNew == 1,
                    PropertyImage = x.PropertiesResidentialImages.Any() ? x.PropertiesResidentialImages.First().ImageName : "feature1"
                }).ToList();
                retVal = Ok(featuredProperties);
            }
            catch (Exception ex)
            {
                s_Logger.Error(ex);
                retVal = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return retVal;
        }


        [Route("api/properties/residental/datatable")]
        [HttpGet]
        public async Task<IActionResult> GetAllResidentalPropertiesDataTableAsync()
        {
            ActionResult retVal = null;

            try
            {
                var properties = await m_PropertiesLogic.GetAllResidentalPropertiesAsync();
                JObject json = new JObject();
                JArray dataArray = new JArray();
                byte i = 0;
                foreach (PropertiesResidental property in properties)
                {
                    JObject data = new JObject();
                    data.Add("DT_RowId", "row_" + property.Id);
                    data.Add("Title", property.Title);
                    data.Add("Description", property.Description);
                    data.Add("Address", property.Address);
                    //data.Add("CityId", property.CityId);
                    data.Add("SizeMeters", property.SizeMeters);
                    data.Add("BadRoomsCount", property.BadRoomsCount);
                    data.Add("BathRoomsCount", property.BathRoomsCount);
                    data.Add("Price", property.Price);
                    data.Add("IsFeatured", property.IsFeatured == 1);
                    data.Add("actions", "");
                    dataArray.Add(data);
                    i++;
                }
                json.Add("data", dataArray);
                retVal = Ok(json.ToString());
            }
            catch (Exception ex)
            {
                s_Logger.Error(ex);
                retVal = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return retVal;
        }

        [Route("api/properties/residental")]
        [HttpPost]
        public async Task<IActionResult> AddResidentalPropertyAsync(ResidentalPropertyDTO residentalPropertyDTO)
        {
            ActionResult retVal = null;

            try
            {
                var residentalPropertyEntity = new PropertiesResidental()
                {
                    Id = residentalPropertyDTO.Id,
                    Title = residentalPropertyDTO.Title,
                    Description = residentalPropertyDTO.Description,
                    Address = residentalPropertyDTO.Address,
                    CityId = residentalPropertyDTO.CityId,
                    SizeMeters = residentalPropertyDTO.SizeMeters,
                    BadRoomsCount = residentalPropertyDTO.BadRoomsCount,
                    BathRoomsCount = residentalPropertyDTO.BathRoomsCount,
                    Price = residentalPropertyDTO.Price,
                    IsFeatured = residentalPropertyDTO.IsFeatured ? (byte)1 : (byte)0,
                };

                if (residentalPropertyDTO.PropertyImages != null)
                {
                    residentalPropertyEntity.PropertiesResidentialImages = residentalPropertyDTO.PropertyImages.Select(imageName =>
                        new PropertiesResidentialImages()
                        {
                            ImageName = imageName
                        }).ToList();
                }

                PropertiesResidental propertiesResidental = await m_PropertiesLogic.AddResidentalPropertyAsync(residentalPropertyEntity);
                retVal = Ok(propertiesResidental);
            }
            catch (Exception ex)
            {
                s_Logger.Error(ex);
                retVal = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return retVal;
        }

        [Route("api/properties/residental")]
        [HttpPut]
        public async Task<IActionResult> UpdateResidentalPropertyAsync(ResidentalPropertyDTO residentalPropertyDTO)
        {
            ActionResult retVal = null;

            try
            {
                var residentalPropertyEntity = new PropertiesResidental()
                {
                    Id = residentalPropertyDTO.Id,
                    Title = residentalPropertyDTO.Title,
                    Description = residentalPropertyDTO.Description,
                    Address = residentalPropertyDTO.Address,
                    CityId = residentalPropertyDTO.CityId,
                    SizeMeters = residentalPropertyDTO.SizeMeters,
                    BadRoomsCount = residentalPropertyDTO.BadRoomsCount,
                    BathRoomsCount = residentalPropertyDTO.BathRoomsCount,
                    Price = residentalPropertyDTO.Price,
                    IsFeatured = residentalPropertyDTO.IsFeatured ? (byte)1 : (byte)0,
                };

                if (residentalPropertyDTO.PropertyImages != null)
                {
                    residentalPropertyEntity.PropertiesResidentialImages = residentalPropertyDTO.PropertyImages.Select(imageName =>
                        new PropertiesResidentialImages()
                        {
                            ImageName = imageName
                        }).ToList();
                }

                PropertiesResidental propertiesResidental = await m_PropertiesLogic.UpdateResidentalPropertyAsync(residentalPropertyEntity);
                retVal = Ok(propertiesResidental);
            }
            catch (Exception ex)
            {
                s_Logger.Error(ex);
                retVal = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return retVal;
        }


        [Route("api/properties/residental/{propertyId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteResidentalPropertyAsync(int propertyId)
        {
            ActionResult retVal = null;

            try
            {
                await m_PropertiesLogic.DeleteResidentalPropertyAsync(propertyId);
                retVal = Ok(true);
            }
            catch (Exception ex)
            {
                s_Logger.Error(ex);
                retVal = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return retVal;
        }
    }
}
