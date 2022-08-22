using Optmni.BL.DTOs.ResponseDTOs;
using Optmni.BL.Manager.Interface;
using Optmni.DAL.Model;
using Optmni.DAL.Repository;
using Optomni.Utilities.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Optmni.BL.Manager
{
    public class Lkp_RegionManager : ILkp_RegionManager
    {
        private IBaseRepository<Lkp_Region> _lkp_RegionRepository;

        public Lkp_RegionManager(IBaseRepository<Lkp_Region> lkp_RegionRepository)
        {
            _lkp_RegionRepository = lkp_RegionRepository;
        }

        public async Task<GeneralResponse<List<LookUpResponseDTO>>> GetAll()
        {
            List<LookUpResponseDTO> lookData = (from region in await _lkp_RegionRepository.GetAllAsync()
                    select new LookUpResponseDTO()
                    {
                        Id = region.Id,
                        Name = region.Name
                    }).ToList();

            if (lookData != null)
            {
                return new GeneralResponse<List<LookUpResponseDTO>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = lookData
                };
            }
            else
            {
                return new GeneralResponse<List<LookUpResponseDTO>>
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    MessageKey = "DataNotFound", 
                    Data = null
                };
            }
        }
    }
}
