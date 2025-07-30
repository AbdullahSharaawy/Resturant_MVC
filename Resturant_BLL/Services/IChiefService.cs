using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;
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
        public ChiefDTO? GetById(int id);
        public Chief? Create(ChiefDTO chief);
        public Chief? Update(ChiefDTO chief);
        public bool Delete(int id);
    }
}
