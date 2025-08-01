using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class ReviewService : IReviewService
    {
            private readonly IRepository<Review> _RR;

            public ReviewService(IRepository<Review> rr)
            {
                _RR = rr;
            }

            public Review? Create(ReviewDTO review)
            {
                if (review == null)
                    return null;

                Review mappedReview = new ReviewMapper().MapToReview(review);
                mappedReview.CreatedOn = DateTime.UtcNow;
                mappedReview.CreatedBy = "Current User";
                mappedReview.IsDeleted = false;

                _RR.Create(mappedReview);
                return mappedReview;
            }

            public bool Delete(int id)
            {
                Review r = _RR.GetByID(id);
                if (r == null || r.IsDeleted == true)
                {
                    return false;
                }

                r.IsDeleted = true;
                r.DeletedOn = DateTime.UtcNow;
                r.DeletedBy = "Current User";

                _RR.Update(r);
                return true;
            }
            public ReviewDTO? GetById(int id)
            {
                Review r = _RR.GetByID(id);
                if (r == null || r.IsDeleted == true)
                {
                    return null;
                }

                ReviewDTO reviewDTO = new ReviewMapper().MapToReviewDTO(r);
                return reviewDTO;
            }
            public List<ReviewDTO> GetList()
            {
                List<Review> reviews = _RR.GetAll().Where(r => r.IsDeleted == false).ToList();

                if (reviews == null || reviews.Count == 0)
                {
                    return new List<ReviewDTO>();
                }

                return new ReviewMapper().MapToReviewDTOList(reviews);
            }
            public Review? Update(ReviewDTO review)
            {
                if (review == null)
                    return null;

                Review mappedReview = new ReviewMapper().MapToReview(review);
                mappedReview.ModifiedOn = DateTime.UtcNow;
                mappedReview.ModifiedBy = "Current User";
                mappedReview.IsDeleted = false;

                _RR.Update(mappedReview);
                return mappedReview;
            }
        }
    }