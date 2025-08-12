using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels.ReviewDTOS;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
    public interface IReviewService
    {
        public Task<List<ReviewDTO>> GetList(Expression<Func<Review, bool>> condition);
        public Task<ReviewDTO?> GetById(int id);
        public Task<Review?> Create(ReviewDTO review);
        public Task<Review?> Update(ReviewDTO review);
        public Task<bool> Delete(int id);
    }
}