using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;

namespace Resturant_BLL.Services
{
    public interface ITableService
    {
        public List<TableDTO> GetList();
        public TableDTO GetById(int id);
        public TableDTO Create(TableDTO table);
        public bool Update(TableDTO table);
        public bool Delete(int id);
    }
}
