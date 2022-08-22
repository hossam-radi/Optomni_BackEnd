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

namespace Optmni.Api.Controllers.Growers
{
    [Route("api/grower/product")]
    [ApiController]
    [Authorize(Roles = OptmniConstants.GrowersRole)]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager
            )
        {
            _productManager = productManager;
        }


        /// <summary>
        /// Get list of Product
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(DataResponse<List<ProductListResponseDTO>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] int page_size, [FromQuery] int page_number = 1)
        {
            var result = await _productManager.GetProducts(page_number, page_size);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new DataResponseWithPaging<List<ProductListResponseDTO>>
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
        /// Add Edit in Product
        /// </summary>
        /// <param name="ProductRequestDTO"></param>
        /// <returns></returns>
        [HttpPut("add_edit")]
        [ProducesResponseType(typeof(DataResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddEdit(ProductRequestDTO model)
        {
            var result = await _productManager.AddEditProduct(model);

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
        /// Delete  product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(DataResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete( string id)
        {
            var result = await _productManager.DeleteProduct(id);

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
