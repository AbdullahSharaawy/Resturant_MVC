
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.ImplementRepository;
using Resturant_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

 using Table = Resturant_DAL.Entities.table;

namespace Resturant_BLL.Services
{
    public class ReservedTableService : IReservedTableService
    {
        private readonly IRepository<ReservedTable> _reservedTableRepo;
        private readonly IRepository<Table> _tableRepo;
        
        public ReservedTableService(IRepository<ReservedTable> reservedRepo, IRepository<Table> tableRepo)
        {
            _reservedTableRepo = reservedRepo;
            _tableRepo = tableRepo;
        }

        public ReservedTable? Create(ReservedTableDTO dto)
        {

            if (dto == null)
                return null;


            ReservedTable mappedReservedTable = new ReservedTableMapper().MapToReservedTable(dto);
            mappedReservedTable.CreatedOn = DateTime.UtcNow;
            mappedReservedTable.CreatedBy = "Current User";
            mappedReservedTable.IsDeleted = false;
            _reservedTableRepo.Create(mappedReservedTable);
            return mappedReservedTable;
        }

        public ReservedTable? Update(ReservedTableDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            // the mapping remove the values that are not in the DTO
            ReservedTable UpdatedReservedTable =_reservedTableRepo.GetByID(dto.ReservedTableID);
            UpdatedReservedTable.TableID = dto.TableID;
            UpdatedReservedTable.ReservationID = dto.ReservationID;
            UpdatedReservedTable.ModifiedBy = "user";
            UpdatedReservedTable.ModifiedOn = DateTime.UtcNow;
            UpdatedReservedTable.DateTime = dto.DateTime;
            UpdatedReservedTable.ModifiedBy = "Current User"; // This should be replaced with the actual user context
           _reservedTableRepo.Update(UpdatedReservedTable);
            return UpdatedReservedTable;
        }

        public bool Delete(int id)
        {
            var entity = _reservedTableRepo.GetByID(id);
            if (entity == null || entity.IsDeleted) return false;

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            entity.DeletedBy = "Current User";

            _reservedTableRepo.Update(entity);
            return true;

        }

        public ReservedTableDTO? GetById(int id)
        {
            var entity = _reservedTableRepo.GetByID(id);
            if (entity == null || entity.IsDeleted) return null;

            return new ReservedTableMapper().MapToReservedTableDTO(entity);
        }

        public List<ReservedTableDTO> GetList()
        {
            var all = _reservedTableRepo.GetAll()
                .Where(r => !r.IsDeleted)
                .ToList();

            return new ReservedTableMapper().MapToReservedTableDTOList(all);
        }

        public void Create(ReservedTable reservedTable)
        {
           _reservedTableRepo.Create(reservedTable);

        }
    }
}
