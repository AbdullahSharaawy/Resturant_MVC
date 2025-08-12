using Resturant_BLL.DTOModels.ChifDTOS;
using Resturant_BLL.DTOModels.ReviewDTOS;
using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;
using System.Collections.Generic;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class ReviewMapper
    {

        [MapProperty(nameof(Review.User.ImagePath), nameof(ReviewDTO.ImagePath))]
        public partial ReviewDTO MapToReadReviewDTO(Review review);
        public partial Review MapToReview(ReviewDTO reviewDTO);
        public partial List<ReviewDTO> MapToReviewDTOList(List<Review> reviews);

        internal ReviewDTO MapToReviewDTO(Review r)
        {
            throw new NotImplementedException();
        }
    }
}