using Optmni.BL.DTOs.ResponseDTOs;
using Optomni.Utilities.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Optmni.BL.Manager.Interface
{
    public interface ILkp_RegionManager
    {
        Task<GeneralResponse<List<LookUpResponseDTO>>> GetAll();
    }
}
