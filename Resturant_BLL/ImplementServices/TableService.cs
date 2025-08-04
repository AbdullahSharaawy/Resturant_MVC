using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

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

        public async Task<table?> Create(TableDTO table)
        {
            if (table == null)
                return null;

            table mappedTable = new TableMapper().MapToTable(table);
            mappedTable.CreatedOn = DateTime.UtcNow;
            mappedTable.CreatedBy = "Current User";
            mappedTable.IsDeleted = false;
            await _TR.Create(mappedTable);
            return mappedTable;
        }

        public async Task<bool> Delete(int id)
        {
            table t = await _TR.GetByID(id);
            if (t == null || t.IsDeleted == true)
            {
                return false;
            }
            t.IsDeleted = true;
            t.DeletedOn = DateTime.UtcNow;
            t.DeletedBy = "Current User";
            await _TR.Update(t);
            return true;
        }

        public async Task<TableDTO?> GetById(int id)
        {
            table t = await _TR.GetByID(id);

            if (t == null || t.IsDeleted == true)
            {
                return null;
            }

            TableDTO tableDTO = new TableMapper().MapToTableDTO(t);
            return tableDTO;
        }

        public async Task<List<TableDTO>> GetList()
        {
            List<table> tables = (await _TR.GetAll()).Where(t => t.IsDeleted == false).ToList();

            if (tables == null || tables.Count == 0)
            {
                return new List<TableDTO>();
            }

            List<TableDTO> tablesDTO = new TableMapper().MapToTableDTOList(tables);
            return tablesDTO;
        }

        public async Task<UpdateTableDTO?> GetCreateTableDTOInfo()
        {
            UpdateTableDTO createTableDTO = new UpdateTableDTO();
            createTableDTO.Branches = (await _BR.GetAll())
                .Where(b => b.IsDeleted == false)
                .Select(b => new BranchMapper().MapToBranchDTO(b))
                .ToList();
            return createTableDTO;
        }

        public async Task<UpdateTableDTO?> GetUpdateTableInfo(int id)
        {
            TableDTO tableDTO = await GetById(id);
            if (tableDTO == null)
            {
                return null;
            }
            List<BranchDTO> Branches = (await _BR.GetAll())
                .Where(b => b.IsDeleted == false)
                .Select(b => new BranchMapper().MapToBranchDTO(b))
                .ToList();
            if (Branches == null || Branches.Count == 0)
            {
                return null;
            }
            UpdateTableDTO updateTableDTO = new UpdateTableDTO();
            updateTableDTO.Branches = Branches;
            updateTableDTO.tableDTO = tableDTO;
            return updateTableDTO;
        }

        public async Task<table?> Update(TableDTO table)
        {
            if (table == null)
            {
                return null;
            }

            table UpdatedTable = await _TR.GetByID(table.TableID);
            UpdatedTable.TableNumber = table.TableNumber;
            UpdatedTable.Capacity = table.Capacity;
            UpdatedTable.BranchID = table.BranchID;
            UpdatedTable.ModifiedOn = DateTime.UtcNow;
            UpdatedTable.ModifiedBy = "Current User";
            await _TR.Update(UpdatedTable);
            return UpdatedTable;
        }
    }
}