using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Resturant_BLL.DTOModels.ReviewDTOS;
using Resturant_BLL.Mapperly;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;

namespace Resturant_BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _RR;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReviewService(IRepository<Review> rr, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _RR = rr;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Review?> Create(ReadReviewDTO readReview)
        {
            if (readReview == null)
                return null;
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            List<Review> reviews = await _RR.GetAllByFilter(r => r.UserID == user.Id);
            Review review=new Review();
            if(reviews.Count==0)
            {
                review = new ReviewMapper().MapToReview(readReview);
                review=await ReviewMapped(review,user);
                await _RR.Create(review);
            }else
            {
                review =reviews.First();
                review.Description = readReview.Description;
                review.Rate = readReview.Rate;
                review.DateTime = DateTime.Now;
                review.UserID = user.Id;
                review.CreatedBy = user.FirstName + " " + user.LastName;
                review.CreatedOn = DateTime.Now;
                await _RR.Update(review);
            }
            return review;
        }
       private async Task<Review> ReviewMapped(Review review,User user)
        {
            review.DateTime = DateTime.Now;
            review.UserID = user.Id;
            review.CreatedBy = user.FirstName + " " + user.LastName;
            review.CreatedOn = DateTime.Now;
            return review;
        }
        public async Task<bool> Delete(int id)
        {
            Review r = await _RR.GetByID(id);
            if (r == null || r.IsDeleted == true)
            {
                return false;
            }

            r.IsDeleted = true;
            r.DeletedOn = DateTime.UtcNow;
            r.DeletedBy = "Current User";

            await _RR.Update(r);
            return true;
        }

        public async Task<ReviewDTO?> GetById(int id)
        {
            Review r = await _RR.GetByID(id);
            if (r == null || r.IsDeleted == true)
            {
                return null;
            }

            ReviewDTO reviewDTO = new ReviewMapper().MapToReviewDTO(r);
            return reviewDTO;
        }

        public async Task<List<ReviewDTO>> GetList(Expression<Func<Review, bool>> filter)
        {
            List<Review> reviews = await _RR.GetAllByFilter(filter);

            if (reviews == null || reviews.Count == 0)
            {
                return new List<ReviewDTO>();
            }

            return new ReviewMapper().MapToReviewDTOList(reviews);
        }

        

        public async Task<Review?> Update(ReviewDTO review)
        {
            if (review == null)
                return null;

            Review mappedReview = new ReviewMapper().MapToReview(review);
            mappedReview.ModifiedOn = DateTime.UtcNow;
            mappedReview.ModifiedBy = "Current User";
            mappedReview.IsDeleted = false;

            await _RR.Update(mappedReview);
            return mappedReview;
        }

        
    }
}