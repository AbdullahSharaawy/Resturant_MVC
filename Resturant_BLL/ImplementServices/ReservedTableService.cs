using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Resturant_BLL.DTOModels.ReservedTablesDTOS;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class ReservedTableService : IReservedTableService
    {
        private readonly IRepository<ReservedTable> _RTR;
        private readonly IRepository<table> _TR;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        public ReservedTableService(IRepository<ReservedTable> reservedRepo, IRepository<table> tableRepo, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager = null)
        {
            _RTR = reservedRepo;
            _TR = tableRepo;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }




        public async Task<bool> Delete(int id)
        {
            ReservedTable t = await _RTR.GetByID(id);
            if (t == null || t.IsDeleted == true)
            {
                return false;
            }
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            t.IsDeleted = true;
            t.DeletedOn = DateTime.UtcNow;
            t.DeletedBy = user.FirstName+" "+user.LastName;
            await _RTR.Update(t);
            return true;
        }
        public async Task<bool> Delete(ReservedTable reservedTable)
        {
            if (reservedTable == null ) return false;
            await _RTR.Delete(reservedTable);
            return true;
        }
        
        public async Task<ReservedTableDTO?> GetById(int id)
        {
            var entity = await _RTR.GetByID(id);
            if (entity == null || entity.IsDeleted) return null;

            return new ReservedTableMapper().MapToReservedTableDTO(entity);
        }

        public async Task<List<ReservedTableDTO>> GetList()
        {
            var all = (await _RTR.GetAll())
                .Where(r => !r.IsDeleted)
                .ToList();

            return new ReservedTableMapper().MapToReservedTableDTOList(all);
        }

        public async Task Create(ReservedTable reservedTable)
        {
            await _RTR.Create(reservedTable);
        }
    }
}