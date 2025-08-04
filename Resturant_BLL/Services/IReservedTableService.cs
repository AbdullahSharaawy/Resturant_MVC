using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.ImplementRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.Services
{
    public interface IReservedTableService
    {
        public Task<ReservedTable?> Create(ReservedTableDTO dto);
        public Task<ReservedTable?> Update(ReservedTableDTO dto);

        public Task<bool> Delete(int id);


        public  Task<ReservedTableDTO?> GetById(int id);


        public  Task<List<ReservedTableDTO>> GetList();


        public  Task Create(ReservedTable reservedTable);
        
    }
}
