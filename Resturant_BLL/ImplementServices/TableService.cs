using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Resturant_DAL.Entities;
namespace Resturant_BLL.Services
{
    public class TableService : ITableService
    {
        private readonly IRepository<table> _TR;
        private readonly IRepository<Branch> _BR;
        public TableService(IRepository<table> tr, IRepository<Branch> bR)
        {
            _TR = tr;
            _BR = bR;
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
            List<table> tables = _TR.GetAll().Where(t => t.IsDeleted == false).ToList();
            
            if (tables == null || tables.Count == 0)
            {
                return new List<TableDTO>();
            }

            tablesDTO = new TableMapper().MapToTableDTOList(tables);
            return tablesDTO;
        }
        public UpdateTableDTO? GetCreateTableDTOInfo()
        {
            UpdateTableDTO createTableDTO = new UpdateTableDTO();
            createTableDTO.Branches = _BR.GetAll()
                .Where(b => b.IsDeleted == false)
                .Select(b => new BranchMapper().MapToBranchDTO(b))
                .ToList();
            return createTableDTO;
        }
        public UpdateTableDTO? GetUpdateTableInfo(int id)
        {
            TableDTO tableDTO=GetById(id);
            if (tableDTO == null)
            {
                return null;
            }
            List<BranchDTO> Branches = _BR.GetAll().Where(b => b.IsDeleted == false).Select(b => new BranchMapper().MapToBranchDTO(b)).ToList();
            if (Branches == null || Branches.Count == 0)
            {
                return null;
            }
            // i can`t apply mapperly here because it will not work with the list of branches
            UpdateTableDTO updateTableDTO =new UpdateTableDTO();
            updateTableDTO.Branches = Branches;
            updateTableDTO.tableDTO = tableDTO;
            return updateTableDTO;
        }

        public table? Update(TableDTO table)
        {
            if(table==null)
            {
                return null; 
            }

            // the mapping remove the values that are not in the DTO
            table UpdatedTable = _TR.GetByID(table.TableID);
            UpdatedTable.TableNumber = table.TableNumber;
            UpdatedTable.Capacity = table.Capacity;
            UpdatedTable.BranchID = table.BranchID;
            UpdatedTable.ModifiedOn = DateTime.UtcNow;
            UpdatedTable.ModifiedBy = "Current User"; // This should be replaced with the actual user context
            _TR.Update(UpdatedTable);
            return UpdatedTable;
        }

    }
}
