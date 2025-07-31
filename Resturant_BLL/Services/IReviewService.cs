using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;

namespace Resturant_BLL.Services
{
    public interface IReviewService
    {
        public List<ReviewDTO> GetList();
        public ReviewDTO? GetById(int id);
        public Review? Create(ReviewDTO review);
        public Review? Update(ReviewDTO review);
        public bool Delete(int id);

    }
}
