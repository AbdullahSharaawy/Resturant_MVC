using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;
using System.Collections.Generic;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class ReviewMapper
    {
        public partial ReviewDTO MapToReviewDTO(Review review);
        public partial Review MapToReview(ReviewDTO reviewDTO);
        public partial List<ReviewDTO> MapToReviewDTOList(List<Review> reviews);
    }
}