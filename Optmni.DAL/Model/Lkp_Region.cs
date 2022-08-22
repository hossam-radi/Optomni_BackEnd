using System.ComponentModel.DataAnnotations;

namespace Optmni.DAL.Model
{
    public class Lkp_Region: BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
