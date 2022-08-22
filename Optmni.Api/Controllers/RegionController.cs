using Microsoft.AspNetCore.Mvc;
using Optmni.BL.DTOs.ResponseDTOs;
using Optmni.BL.Manager.Interface;
using Optomni.Utilities.Model;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Optmni.Api.Controllers
{
    [Route("api/lookup/region")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly ILkp_RegionManager _regionManager;

        public RegionController(ILkp_RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// Get All region
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(DataResponse<List<LookUpResponseDTO>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _regionManager.GetAll();

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = result.MessageKey //_localizer[result.MessageKey].Value
                    }
                });
            }
        }
    }
}
