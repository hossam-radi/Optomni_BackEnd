using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Optmni.BL.DTOs.RequestDTOs;
using Optmni.BL.DTOs.ResponseDTOs;
using Optmni.Utilities.Constants;
using Optomni.BL.Manager.Interface;
using Optomni.Utilities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Optmni.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISecurityManager _securityManager;
        public AuthController(ISecurityManager securityManager)
        {
            _securityManager = securityManager;
        }

        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(DataResponse<LoginResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            var result = await _securityManager.Login(model);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new DataResponse<LoginResponseDTO>
                {
                    Data = result.Data
                });
            }
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = result.MessageKey//_localizer[result.MessageKey].Value
                    }
                });
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

        [HttpGet("info")]
        [Authorize(Roles = "Customer,Grower")]
        public async Task<IActionResult> GetUserInfo()
        {
            return Ok(_securityManager.GetUserInfo());
        }
    }
}
