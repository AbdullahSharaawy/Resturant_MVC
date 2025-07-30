
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

 using Table = Resturant_DAL.Entities.table;

namespace Resturant_BLL.Services
{
    public class ReservedTableService : IReservedTableService
    {
        private readonly IRepository<ReservedTable> _reservedRepo;
        private readonly IRepository<Table> _tableRepo;

        public ReservedTableService(IRepository<ReservedTable> reservedRepo, IRepository<Table> tableRepo)
        {
            _reservedRepo = reservedRepo;
            _tableRepo = tableRepo;
        }

        public ReservedTable? Create(ReservedTableDTO dto)
        {
            if (dto == null) return null;

            // ✅ التحقق من السعة
            table Table = _tableRepo.GetByID(dto.TableID);
            if (Table == null || dto.Capacity > Table.Capacity)
            {
                throw new Exception($"Table {dto.TableID} does not have enough capacity.");
            }

            var entity = new ReservedTableMapper().MapToReservedTable(dto);
            entity.CreatedOn = DateTime.UtcNow;
            entity.CreatedBy = "Current User";
            entity.IsDeleted = false;

            _reservedRepo.Create(entity);
            return entity;
        }

        public ReservedTable? Update(ReservedTableDTO dto)
        {
            if (dto == null) return null;

            table Table = _tableRepo.GetByID(dto.TableID);
            if (Table == null || dto.Capacity > Table.Capacity)
            {
                throw new Exception($"Table {dto.TableID} does not have enough capacity.");
            }

            var entity = new ReservedTableMapper().MapToReservedTable(dto);
            entity.ModifiedOn = DateTime.UtcNow;
            entity.ModifiedBy = "Current User";

            _reservedRepo.Update(entity);
            return entity;
        }

        public bool Delete(int id)
        {
            var entity = _reservedRepo.GetByID(id);
            if (entity == null || entity.IsDeleted) return false;

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            entity.DeletedBy = "Current User";

            _reservedRepo.Update(entity);
            return true;
        }

        public ReservedTableDTO? GetById(int id)
        {
            var entity = _reservedRepo.GetByID(id);
            if (entity == null || entity.IsDeleted) return null;

            return new ReservedTableMapper().MapToReservedTableDTO(entity);
        }

        public List<ReservedTableDTO> GetList()
        {
            var all = _reservedRepo.GetAll()
                .Where(r => !r.IsDeleted)
                .ToList();

            return new ReservedTableMapper().MapToReservedTableDTOList(all);
        }
    }
}
