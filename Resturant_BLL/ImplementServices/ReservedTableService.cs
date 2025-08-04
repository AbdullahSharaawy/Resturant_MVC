using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class ReservedTableService : IReservedTableService
    {
        private readonly IRepository<ReservedTable> _reservedTableRepo;
        private readonly IRepository<table> _tableRepo;

        public ReservedTableService(IRepository<ReservedTable> reservedRepo, IRepository<table> tableRepo)
        {
            _reservedTableRepo = reservedRepo;
            _tableRepo = tableRepo;
        }

        public async Task<ReservedTable?> Create(ReservedTableDTO dto)
        {
            if (dto == null)
                return null;

            ReservedTable mappedReservedTable = new ReservedTableMapper().MapToReservedTable(dto);
            mappedReservedTable.CreatedOn = DateTime.UtcNow;
            mappedReservedTable.CreatedBy = "Current User";
            mappedReservedTable.IsDeleted = false;
            await _reservedTableRepo.Create(mappedReservedTable);
            return mappedReservedTable;
        }

        public async Task<ReservedTable?> Update(ReservedTableDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            ReservedTable UpdatedReservedTable = await _reservedTableRepo.GetByID(dto.ReservedTableID);
            UpdatedReservedTable.TableID = dto.TableID;
            UpdatedReservedTable.ReservationID = dto.ReservationID;
            UpdatedReservedTable.ModifiedBy = "user";
            UpdatedReservedTable.ModifiedOn = DateTime.UtcNow;
            UpdatedReservedTable.DateTime = dto.DateTime;
            UpdatedReservedTable.ModifiedBy = "Current User";
            await _reservedTableRepo.Update(UpdatedReservedTable);
            return UpdatedReservedTable;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _reservedTableRepo.GetByID(id);
            if (entity == null || entity.IsDeleted) return false;

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            entity.DeletedBy = "Current User";

            await _reservedTableRepo.Update(entity);
            return true;
        }

        public async Task<ReservedTableDTO?> GetById(int id)
        {
            var entity = await _reservedTableRepo.GetByID(id);
            if (entity == null || entity.IsDeleted) return null;

            return new ReservedTableMapper().MapToReservedTableDTO(entity);
        }

        public async Task<List<ReservedTableDTO>> GetList()
        {
            var all = (await _reservedTableRepo.GetAll())
                .Where(r => !r.IsDeleted)
                .ToList();

            return new ReservedTableMapper().MapToReservedTableDTOList(all);
        }

        public async Task Create(ReservedTable reservedTable)
        {
            await _reservedTableRepo.Create(reservedTable);
        }
    }
}