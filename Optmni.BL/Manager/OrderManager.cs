using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Optmni.BL.DTOs.RequestDTOs;
using Optmni.BL.DTOs.ResponseDTOs;
using Optmni.BL.Manager.Interface;
using Optmni.DAL.Model;
using Optmni.DAL.Repository;
using Optmni.DAL.UnitOfWork;
using Optmni.Utilities.Resources;
using Optomni.Utilities.DataProvider;
using Optomni.Utilities.FileUpload;
using Optomni.Utilities.Model;
using Optomni.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Optmni.BL.Manager
{
    public class OrderManager : IOrderManager
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<OrderDetails> _orderDetailsRepository;
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<ApplicationUser> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUpload _fileUpload;
        private readonly string _baseUrl;
        private readonly IHostEnvironment _env;
        private readonly OptmniSettings _settings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserDataProvider _userDataProvider;

        public OrderManager(IBaseRepository<Order> orderRepository,
            IBaseRepository<OrderDetails> orderDetailsRepository,
            IBaseRepository<Product> productRepository,
            IBaseRepository<ApplicationUser> userRepository,
            IUnitOfWork unitOfWork,
            IFileUpload fileUpload,
            IOptions<OptmniSettings> settings,
            IHostEnvironment env,
            UserManager<ApplicationUser> userManager,
            IUserDataProvider userDataProvider)
        {
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _fileUpload = fileUpload;
            _settings = settings.Value;
            _baseUrl = _settings.System.Admin_Url;
            _env = env;
            _userManager = userManager;
            _userDataProvider = userDataProvider;
        }

        public async Task<GeneralResponse<List<OrderListResponseDTO>>> GetOrders(int pageNumber, int pageSize)
        {
            GeneralResponse<List<OrderListResponseDTO>> result =
                new GeneralResponse<List<OrderListResponseDTO>>();

            pageSize = pageSize == 0 ? _settings.System.Page_Size : pageSize;

            int offset = pageNumber == 0 ? pageNumber : (pageNumber - 1) * pageSize;

            string GrowerId = _userDataProvider.GetUserInfo().Id.ToString();
            string CustomerId = _userDataProvider.GetUserInfo().Id.ToString();

             UserInfo LogedInUser = _userDataProvider.GetUserInfo();

            result.Data = (from order in  _orderRepository.GetAll()
                           join grower in  _userRepository.GetAll()  on order.GrowerId equals grower.Id
                           join customer in  _userRepository.GetAll() on order.CustomerId equals customer.Id
                           where order.IsDeleted == false
                           && (string.IsNullOrEmpty(GrowerId) ||order.GrowerId == GrowerId)
                           && (string.IsNullOrEmpty(CustomerId) || order.CustomerId == CustomerId)
                           orderby order.CreatedAt descending
                           select new OrderListResponseDTO
                           {
                               Id = order.Id.ToString(),
                               Code = order.Code,
                               CustomerId = order.CustomerId,
                               CustomerName = customer.FullName,
                               growerId = order.GrowerId,
                               GrowerName = grower.FullName,
                               OrderStatus = order.OrderStatus,
                               OrderDetails = (from orderDetails in _orderDetailsRepository.GetAll()
                                               join product in _productRepository.GetAll() on orderDetails.ProductId equals product.Id
                                              where orderDetails.OrderId == order.Id
                                              select new  OrderDetailsResponseDTO
                                              {
                                                  Id = orderDetails.Id.ToString(),
                                                  ProductId = orderDetails.ProductId.ToString(), 
                                                  ProductName = product.Name,
                                                  ProductUnit = orderDetails.Unit,
                                                  Quantity = orderDetails.Quantity,
                                                  TotalPrice = orderDetails.TotalPrice,
                                   
                               }).ToList()
                           }).Skip(offset).Take(pageSize).ToList();

            int agentsCount = await _orderRepository.CountAsync(x => x.IsDeleted == false);

            var totalNumberOfPages = Math.Ceiling((decimal)agentsCount / pageSize);

            result.Paging = new PagingResponse
            {
                Previous = offset != 0,
                Next = pageNumber < totalNumberOfPages,
                TotalPages = (int)totalNumberOfPages,
                TotalRecords = agentsCount
            };

            result.StatusCode = HttpStatusCode.OK;
            return result;
        }

        public async Task<GeneralResponse<OrderListResponseDTO>> GetOrderDetails(Guid orderId)
        {
            GeneralResponse<OrderListResponseDTO> result = new GeneralResponse<OrderListResponseDTO>();
            UserInfo user = _userDataProvider.GetUserInfo();

            result.Data = (from order in _orderRepository.GetAll()
                                          join grower in _userRepository.GetAll() on order.GrowerId equals grower.Id
                                          join customer in _userRepository.GetAll() on order.CustomerId equals customer.Id
                                          where order.IsDeleted == false
                                          && order.Id == orderId
                                          orderby order.CreatedAt descending
                                          select new OrderListResponseDTO
                                          {
                                              Id = order.Id.ToString(),
                                              Code = order.Code,
                                              CustomerId = order.CustomerId,
                                              CustomerName = customer.FullName,
                                              growerId = order.GrowerId,
                                              GrowerName = grower.FullName,
                                              OrderStatus = order.OrderStatus,
                                              OrderDetails = (from orderDetails in _orderDetailsRepository.GetAll()
                                                              join product in _productRepository.GetAll() on orderDetails.ProductId equals product.Id
                                                              where orderDetails.OrderId == order.Id
                                                              select new OrderDetailsResponseDTO
                                                              {
                                                                  Id = orderDetails.Id.ToString(),
                                                                  ProductId = orderDetails.ProductId.ToString(),
                                                                  ProductName = product.Name,
                                                                  ProductUnit = orderDetails.Unit,
                                                                  Quantity = orderDetails.Quantity,
                                                                  TotalPrice = orderDetails.TotalPrice,

                                                              }).ToList()
                                          }).FirstOrDefault();

            result.StatusCode = HttpStatusCode.OK;
            return result;
        }

        public async Task<GeneralResponse<bool>> AddOrderDetails(OrderRequestDTO model)
        {
            try
            {
                UserInfo user = _userDataProvider.GetUserInfo();
                Order order = new Order()
                {
                    CustomerId = user.Id.ToString() ,
                    
                    
                };

             
                await _unitOfWork.SaveChangesAsync();
                return new GeneralResponse<bool>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<bool>
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    MessageKey = OptmniResources.InternalServerError,
                    Data = false
                };
            }
        }

        public Task<GeneralResponse<bool>> DeleteOder(string orderId)
        {
            throw new NotImplementedException();
        }
    }
}
