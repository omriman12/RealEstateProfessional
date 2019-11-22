using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SouthRealEstate.DAL.Entities;
using SouthRealEstate.DAL.Interfaces;
using SouthRealEstate.DTOs;
using SouthRealEstate.Interfaces;
using SouthRealEstate.Models;
using Newtonsoft;
using Newtonsoft.Json;
using SouthRealEstate.Model;

namespace SouthRealEstate.Controllers
{
    public class PropertiesController : Controller
    {
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(PropertiesController));

        private readonly IPropertiesLogic m_PropertiesLogic;
        private readonly IHostingEnvironment m_HostingEnvironment;
        public PropertiesController(IHostingEnvironment env, IPropertiesLogic propertiesLogic)
        {
            m_HostingEnvironment = env;
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
                    data.Add("IsFeatured", property.IsFeatured);
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
        public async Task<IActionResult> AddResidentalPropertyAsync()
        {
            ActionResult retVal = null;

            try
            {
                ResidentalPropertyDTO residentalPropertyDTO = JsonConvert.DeserializeObject<ResidentalPropertyDTO>(
                    Request.Form["residentalPropertyDTO"],
                    new JsonSerializerSettings()
                    {
                        DefaultValueHandling = DefaultValueHandling.Populate,
                        NullValueHandling = NullValueHandling.Ignore,
                    });

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
                    IsFeatured = residentalPropertyDTO.IsFeatured,
                };

                //images
                var files = Request.Form.Files;
                s_Logger.Info($"received:{files.Count} files");
                if (files.Count > 0)
                {
                    residentalPropertyEntity.PropertiesResidentialImages = new List<PropertiesResidentialImages>();
                    foreach (IFormFile file in files)
                    {
                        string fileNameRaw = file.FileName;
                        string guid = Guid.NewGuid().ToString("N").Substring(0, 12);
                        string fileNameGuid = string.Format("{0}_{1}", guid, fileNameRaw);
                        string pathToSave = Path.Combine(m_HostingEnvironment.WebRootPath, @"img\uploads", fileNameGuid);
                        using (var fileStream = new FileStream(pathToSave, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        residentalPropertyEntity.PropertiesResidentialImages.Add(new PropertiesResidentialImages()
                        {
                            ImageName = fileNameGuid
                        });
                    }
                }

                PropertiesResidental propertiesResidental = await m_PropertiesLogic.AddResidentalPropertyAsync(residentalPropertyEntity);
                retVal = Ok();
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
                    IsFeatured = residentalPropertyDTO.IsFeatured,
                };

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


        [Route("api/properties/search")]
        [HttpPost]
        public async Task<IActionResult> SearchPropertyAsync(SearchPropertyDTO searchPropertyDTO)
        {
            ActionResult retVal = null;

            try
            {
                //get data
                var searchProperty = new SearchProperty()
                {
                    FreeSearch = searchPropertyDTO.FreeSearch,
                    CityId = searchPropertyDTO.CityId,
                    PropertyType = searchPropertyDTO.PropertyType,
                    SizeMetersFrom = searchPropertyDTO.SizeMetersFrom,
                    SizeMetersTo = searchPropertyDTO.SizeMetersTo,
                    BadRoomsCountFrom = searchPropertyDTO.BadRoomsCountFrom,
                    BadRoomsCountTo = searchPropertyDTO.BadRoomsCountTo,
                    PriceFrom = searchPropertyDTO.PriceFrom,
                    PriceTo = searchPropertyDTO.PriceTo,
                };
                IEnumerable<PropertiesResidental> residentalProperties = await m_PropertiesLogic.SearchPropertyAsync(searchProperty);

                //return dto
                var residentalPropertiesDTO = residentalProperties.Select(x =>
                {
                    var residentalPropertyDTO = new ResidentalPropertyDTO
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
                    };

                    if (x.PropertiesResidentialImages != null && x.PropertiesResidentialImages.Any())
                    {
                        residentalPropertyDTO.PropertyImages = x.PropertiesResidentialImages.Select(i => i.ImageName);
                    }

                    return residentalPropertyDTO;
                });

                retVal = Ok(residentalPropertiesDTO);
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
