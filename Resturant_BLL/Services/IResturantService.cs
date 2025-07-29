using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
    public interface IResturantService
    {
        public List<ResturantDTO> GetList();
        public ResturantDTO GetById(int id);
        public ResturantDTO Create(ResturantDTO restaurant);
        public bool Update(ResturantDTO restaurant);
        public bool Delete(int id);
    }
}
