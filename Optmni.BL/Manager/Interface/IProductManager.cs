using Optmni.BL.DTOs.RequestDTOs;
using Optmni.BL.DTOs.ResponseDTOs;
using Optomni.Utilities.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Optmni.BL.Manager.Interface
{
    public interface IProductManager
    {
        Task<GeneralResponse<List<ProductListResponseDTO>>> GetProducts(int page, int pageSize);
        Task<GeneralResponse<ProductDetailsResponseDTO>> GetProductDetails();
        Task<GeneralResponse<bool>> AddEditProduct(ProductRequestDTO model);
        Task<GeneralResponse<bool>> DeleteProduct(string id);

       



    }
}
