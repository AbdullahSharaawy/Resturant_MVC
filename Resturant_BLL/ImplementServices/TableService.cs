using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.Services
{
    public class TableService : ITableService
    {
        private readonly IRepository<Table> _TS;

        public TableService(IRepository<Table> tS)
        {
            _TS = tS;
        }

        public TableDTO Create(TableDTO table)
        {
            
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TableDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<TableDTO> GetList()
        {
            var mapping=new TableMapper();
            List<TableDTO> tablesDTO=new List<TableDTO>();
            List<Table> tables = _TS.GetAll().ToList();
            tablesDTO = mapping.MapToChiefDTOList(tables);
        }

        public bool Update(TableDTO table)
        {
            throw new NotImplementedException();
        }
    }
}
