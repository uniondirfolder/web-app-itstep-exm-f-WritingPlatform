
using System.Collections.Generic;
using WP.BusinessLayer.ObjectValues;

namespace WP.BusinessLayer.Interfaces
{
    public interface IRatingBL
    {
        void Create(RatingBL rating);
        void Update(RatingBL rating);
        RatingBL GetRating(int id);
        IEnumerable<RatingBL> GetRatings();
        void DeleteRating(int id);
        void Dispose();
    }
}
