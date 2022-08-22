using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Optmni.BL.DTOs.RequestDTOs;
using Optmni.BL.DTOs.ResponseDTOs;
using Optmni.BL.Manager.Interface;
using Optmni.DAL.Model;
using Optmni.DAL.Repository;
using Optmni.DAL.UnitOfWork;
using Optmni.Utilities.Constants;
using Optmni.Utilities.Resources;
using Optomni.Utilities.DataProvider;
using Optomni.Utilities.FileUpload;
using Optomni.Utilities.Model;
using Optomni.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Optmni.BL.Manager
{
    public class ProductManager : IProductManager
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<Lkp_Region> _regionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUpload _fileUpload;
        private readonly string _baseUrl;
        private readonly IHostEnvironment _env;
        private readonly OptmniSettings _settings;
        private readonly IUserDataProvider _userDataProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductManager(IBaseRepository<Product> productRepository,
            IBaseRepository<Lkp_Region> regionRepository,
            IUnitOfWork unitOfWork,
            IFileUpload fileUpload,
            IOptions<OptmniSettings> settings,
            IHostEnvironment env,
            UserManager<ApplicationUser> userManager,
            IUserDataProvider userDataProvider)
        {
            _productRepository = productRepository;
            _regionRepository = regionRepository;
            _unitOfWork = unitOfWork;
            _fileUpload = fileUpload;
            _settings = settings.Value;
            _env = env;
            _userDataProvider = userDataProvider;
            _userManager = userManager;
        }

        // Get Products
        public async Task<GeneralResponse<List<ProductListResponseDTO>>> GetProducts(int pageNumber, int pageSize)

        {
            GeneralResponse<List<ProductListResponseDTO>> result =
                new GeneralResponse<List<ProductListResponseDTO>>();

            pageSize = pageSize == 0 ? _settings.System.Page_Size : pageSize;

            int offset = pageNumber == 0 ? pageNumber : (pageNumber - 1) * pageSize;

            UserInfo user = _userDataProvider.GetUserInfo();

            result.Data = (from product in _productRepository.GetAll()
                           join regionObj in _regionRepository.GetAll() on product.RegionId equals regionObj.Id
                           into reg
                           from region in reg.DefaultIfEmpty()
                           where product.UserId == user.Id
                           && !product.IsDeleted

                           select new ProductListResponseDTO
                           {
                               Id = product.Id.ToString(),
                               Name = product.Name,
                               Description = product.Description,
                               PictureUrl = string.IsNullOrEmpty(product.Picture) ? null :
                                         $"{_baseUrl}/{ OptmniConstants.UploadProductPhotos}/{product.Picture}",

                               Price = product.Price,
                               ProductType = product.ProductType,
                               RegionId = product.RegionId,
                               RegionName = region.Name
                           }).Skip(offset).Take(pageSize).ToList();

            int productsCount = await _productRepository.CountAsync(x => x.IsDeleted == false && x.UserId == user.Id);

            var totalNumberOfPages = Math.Ceiling((decimal)productsCount / pageSize);

            result.Paging = new PagingResponse
            {
                Previous = offset != 0,
                Next = pageNumber < totalNumberOfPages,
                TotalPages = (int)totalNumberOfPages,
                TotalRecords = productsCount
            };

            result.StatusCode = HttpStatusCode.OK;
            return result;
        }


        // Get Product Details 
        public async Task<GeneralResponse<ProductDetailsResponseDTO>> GetProductDetails()
        {
            GeneralResponse<ProductDetailsResponseDTO> result = new GeneralResponse<ProductDetailsResponseDTO>();
            UserInfo user = _userDataProvider.GetUserInfo();

            result.Data = (from product in _productRepository.GetAll()
                           join regionObj in _regionRepository.GetAll() on product.RegionId equals regionObj.Id
                           into reg
                           from region in reg.DefaultIfEmpty()
                           where product.UserId == user.Id
                           && !product.IsDeleted

                           select new ProductDetailsResponseDTO
                           {
                               Id = product.Id.ToString(),
                               Name = product.Name,
                               Description = product.Description,
                               PictureUrl = string.IsNullOrEmpty(product.Picture) ? null :
                                         $"{_baseUrl}/{ OptmniConstants.UploadProductPhotos}/{product.Picture}",

                               Price = product.Price,
                               ProductType = product.ProductType,
                               RegionId = product.RegionId,
                               RegionName = region.Name
                           }).FirstOrDefault();



            result.StatusCode = HttpStatusCode.OK;
            return result;
        }

        // Add Edit Product
        public async Task<GeneralResponse<bool>> AddEditProduct(ProductRequestDTO model)
        {
            try
            {
                UserInfo user = _userDataProvider.GetUserInfo();

                Product productObj = new Product();
                if (!string.IsNullOrEmpty(model.Id)) // edit Mode 
                {
                    productObj = _productRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(model.Id));
                    if (productObj is null)
                    {
                        return new GeneralResponse<bool>
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            MessageKey = OptmniResources.DataNotFound
                        };
                    }
                    productObj.UpdatedAt = DateTime.Now;
                    productObj.UpdatedBy = user.Id;

                }
                else
                {
                    productObj.CreatedAt = DateTime.Now;
                    productObj.CreatedBy = user.Id;
                }

                productObj.Name = model.Name;
                productObj.Description = model.description;
                productObj.RegionId = model.RegionId;
                productObj.Price = model.Price;
                productObj.ProductType = model.ProductType;
                productObj.UserId = user.Id.Value;


                if (string.IsNullOrEmpty(model.Id))
                {
                    _productRepository.Add(productObj);
                }

                await _unitOfWork.SaveChangesAsync();
                return new GeneralResponse<bool>
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
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

        //Delete Product 
        public async Task<GeneralResponse<bool>> DeleteProduct(string id)
        {
            try
            {
                Product product = _productRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(id));
                if (product is null)
                {
                    return new GeneralResponse<bool>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        MessageKey = OptmniResources.DataNotFound
                    };
                }
                product.IsDeleted = true;
                await _unitOfWork.SaveChangesAsync();
                return new GeneralResponse<bool>
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
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

      
    }
}
