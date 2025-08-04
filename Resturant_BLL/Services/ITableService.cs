using System.Collections.Generic;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
    public interface ITableService
    {
        public Task<List<TableDTO>> GetList();
        public Task<TableDTO?> GetById(int id);
        public Task<table?> Create(TableDTO table);
        public Task<table?> Update(TableDTO table);
        public Task<bool> Delete(int id);
        public Task<UpdateTableDTO?> GetUpdateTableInfo(int id);
        public Task<UpdateTableDTO?> GetCreateTableDTOInfo();
    }
}