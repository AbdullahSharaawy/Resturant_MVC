using Resturant_BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.Services
{
    public interface IChiefService
    {
        public List<ChiefDTO> GetList();
        public ChiefDTO GetById(int id);
        public ChiefDTO Create(ChiefDTO chief);
        public bool Update(ChiefDTO chief);
        public bool Delete(int id);
    }
}
