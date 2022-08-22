using System;

namespace Optmni.DAL.Interfaces
{
    public interface IBaseModel
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        bool IsDeleted { get; set; }
        Guid? CreatedBy { get; set; }
        Guid? UpdatedBy { get; set; }
    }
}
