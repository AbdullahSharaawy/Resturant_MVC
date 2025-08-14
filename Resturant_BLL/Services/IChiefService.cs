using Resturant_BLL.DTOModels.ChifDTOS;
using Resturant_BLL.DTOModels.ReviewDTOS;
using Resturant_DAL.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Resturant_BLL.Services
{
    public interface IChiefService
    {
        public Task<List<ChiefDTO>> GetList(Expression<Func<Chief, bool>> condition);
        public Task<List<ChiefDTO>> GetList();
        public Task<ChiefDTO?> GetById(int id);
        public Task<Chief?> Create(ChiefDTO chief);
        public Task<Chief?> Update(ChiefDTO chief);
        public Task<bool> Delete(int id);
        public Task<ManageChiefDTO?> GetUpdateChiefInfo(int id);
        public Task<ManageChiefDTO?> GetCreateChiefInfo();
    }
}