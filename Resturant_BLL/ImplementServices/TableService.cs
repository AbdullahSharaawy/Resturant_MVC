using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_DAL.Entities;
namespace Resturant_BLL.Services
{
    public class TableService : ITableService
    {
        private readonly IRepository<table> _TR;

        public TableService(IRepository<table> tr)
        {
            _TR = tr;
        }

        public table? Create(TableDTO table)
        {
            if(table==null)
                return null;

            table mappedTable = new TableMapper().MapToTable(table);
            mappedTable.CreatedOn = DateTime.UtcNow;
            mappedTable.CreatedBy = "Current User";
            mappedTable.IsDeleted = false; 
            _TR.Create(mappedTable);
            return mappedTable;
        }

        public bool Delete(int id)
        {
            table t=_TR.GetByID(id);
            if(t==null || t.IsDeleted == true)
            {
                return false;    
            }
            t.IsDeleted = true;
            t.DeletedOn = DateTime.UtcNow;
            t.DeletedBy = "Current User"; // This should be replaced with the actual user context
            _TR.Update(t);
            return true;
        }

        public TableDTO? GetById(int id)
        {
           table t=_TR.GetByID(id);
            
           if (t == null || t.IsDeleted==true)
            {
                return null;
            }
           
           TableDTO tableDTO = new TableMapper().MapToTableDTO(t);
           return tableDTO;
        }

        public List<TableDTO> GetList()
        {
            List<TableDTO> tablesDTO=new List<TableDTO>();
            List<table> tables = _TR.GetAll().Where(t=>t.IsDeleted==false).ToList();

            if(tables == null || tables.Count == 0)
            {
                return new List<TableDTO>();
            }

            tablesDTO = new TableMapper().MapToTableDTOList(tables);
            return tablesDTO;
        }

        public table? Update(TableDTO table)
        {
            if(table==null)
            {
                return null; 
            }

            
            table mappedTable = new TableMapper().MapToTable(table);
            mappedTable.ModifiedOn = DateTime.UtcNow;
            mappedTable.ModifiedBy = "Current User";
            mappedTable.IsDeleted = false;
            _TR.Update(mappedTable);
            return mappedTable;
        }
    }
}
