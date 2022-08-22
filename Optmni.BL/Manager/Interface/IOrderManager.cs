using Optmni.BL.DTOs.RequestDTOs;
using Optmni.BL.DTOs.ResponseDTOs;
using Optomni.Utilities.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Optmni.BL.Manager.Interface
{
    public interface IOrderManager
    {
        Task<GeneralResponse<List<OrderListResponseDTO>>> GetOrders(int page, int pageSize);
        Task<GeneralResponse<bool>> AddOrderDetails(OrderRequestDTO model);
        Task<GeneralResponse<bool>> DeleteOder(string orderId);
        Task<GeneralResponse<OrderListResponseDTO>> GetOrderDetails(Guid orderId);


      

    }
}
