using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SouthRealEstate.DAL.Entities;
using SouthRealEstate.DTOs;
using SouthRealEstate.Interfaces;
using SouthRealEstate.Models;

namespace SouthRealEstate.Controllers
{
    public class AgentsController : Controller
    {

        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(AgentsController));

        private readonly IPropertiesLogic m_PropertiesLogic;
        private readonly IHostingEnvironment m_HostingEnvironment;
        public AgentsController(IHostingEnvironment env, IPropertiesLogic propertiesLogic)
        {
            m_HostingEnvironment = env;
            m_PropertiesLogic = propertiesLogic;
        }


        [Route("api/agents/datatable")]
        [HttpGet]
        public async Task<IActionResult> GetAllAgentsDataTAbleAsync()
        {
            ActionResult retVal = null;

            try
            {
                var agents = await m_PropertiesLogic.GetAllAgentsAsync();
                JObject json = new JObject();
                JArray dataArray = new JArray();
                byte i = 0;
                foreach (Agents agent in agents)
                {
                    JObject data = new JObject();
                    data.Add("DT_RowId", "row_" + agent.Id);
                    data.Add("Name", agent.Name);
                    data.Add("Phone", agent.Phone);
                    data.Add("Email", agent.Email);
                    data.Add("Details", agent.Details);
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


        [Route("api/agents")]
        [HttpGet]
        public async Task<IActionResult> GetAllAgentsAsync()
        {
            ActionResult retVal = null;

            try
            {
                var agents = await m_PropertiesLogic.GetAllAgentsAsync();
                List<AgentDTO> agentsDTO = agents.Select(x => new AgentDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Phone = x.Phone,
                    Details = x.Details,
                    ImageName = x.ImageName
                }).ToList();
                retVal = Ok(agentsDTO);
            }
            catch (Exception ex)
            {
                s_Logger.Error(ex);
                retVal = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return retVal;
        }

        [Route("api/agents")]
        [HttpPost]
        public async Task<IActionResult> AddAgentAsync()
        {
            ActionResult retVal = null;

            try
            {
                AgentDTO agentDTO = JsonConvert.DeserializeObject<AgentDTO>(
                    Request.Form["agentDTO"],
                    new JsonSerializerSettings()
                    {
                        DefaultValueHandling = DefaultValueHandling.Populate,
                        NullValueHandling = NullValueHandling.Ignore,
                    });

                var agent = new Agents()
                {
                    Id = agentDTO.Id,
                    Name = agentDTO.Name,
                    Email = agentDTO.Email,
                    Phone = agentDTO.Phone,
                    Details = agentDTO.Details,
                };

                //images
                var files = Request.Form.Files;
                s_Logger.Info($"received:{files.Count} files");
                if (files.Count > 0)
                {
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

                        agent.ImageName = fileNameGuid;
                    }
                }

                await m_PropertiesLogic.AddUpdateAgentsAsync(agent);
                retVal = Ok();
            }
            catch (Exception ex)
            {
                s_Logger.Error(ex);
                retVal = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return retVal;
        }

        [Route("api/agents")]
        [HttpPut]
        public async Task<IActionResult> UpdateAgentAsync(AgentDTO agentDTO)
        {
            ActionResult retVal = null;

            try
            {
                var agent = new Agents()
                {
                    Id = agentDTO.Id,
                    Name = agentDTO.Name,
                    Email = agentDTO.Email,
                    Phone = agentDTO.Phone,
                    Details = agentDTO.Details,
                };

                Agents updatedAgent = await m_PropertiesLogic.AddUpdateAgentsAsync(agent);
                retVal = Ok(updatedAgent);
            }
            catch (Exception ex)
            {
                s_Logger.Error(ex);
                retVal = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return retVal;
        }


        [Route("api/agents/{agentId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAgentsAsync(int agentId)
        {
            ActionResult retVal = null;

            try
            {
                await m_PropertiesLogic.DeleteAgentAsync(agentId);
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
