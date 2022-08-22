using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Optmni.BL.DTOs.RequestDTOs;
using Optmni.BL.DTOs.ResponseDTOs;
using Optmni.BL.Manager.Interface;
using Optmni.Utilities.Constants;
using Optomni.Utilities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Optmni.Api.Controllers.Customer
{
    [Route("api/customer/order")]
    [ApiController]
    [Authorize(Roles = OptmniConstants.CustomerRole)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        /// <summary>
        /// Get list of order
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(DataResponse<List<OrderListResponseDTO>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] int page_size, [FromQuery] int page_number = 1)
        {
            var result = await _orderManager.GetOrders(page_number, page_size);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new DataResponseWithPaging<List<OrderListResponseDTO>>
                {
                    Data = result.Data,
                    Paging = result.Paging
                });
            }
            else
            {
                return BadRequest(new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = result.MessageKey
                    }
                });
            }
        }


        /// <summary>
        /// Add Edit in order
        /// </summary>
        /// <param name="orderRequestDTO"></param>
        /// <returns></returns>
        [HttpPut("add")]
        [ProducesResponseType(typeof(DataResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddEdit(OrderRequestDTO model)
        {
            var result = await _orderManager.AddOrderDetails(model);

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
                        Message = result.MessageKey// _localizer[result.MessageKey].Value
                    }
                });
            }
        }

        /// <summary>
        /// Delete  order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(DataResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _orderManager.DeleteOder(id);

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
                        Message = result.MessageKey//_localizer[result.MessageKey].Value
                    }
                });
            }
        }

    }
}
