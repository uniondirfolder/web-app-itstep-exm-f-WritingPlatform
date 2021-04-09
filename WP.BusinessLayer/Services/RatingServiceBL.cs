using System.Collections.Generic;
using WP.BusinessLayer.Interfaces;
using WP.BusinessLayer.ObjectValues;
using WP.DataLayer.Entities;
using WP.DataLayer.UnitOfWork;

namespace WP.BusinessLayer.Services
{
    public class RatingServiceBL : ABaseServiceBL, IRatingBL
    {
        public RatingServiceBL(IUnitOfWork unitOfWork) : base(unitOfWork){}

        public void Create(RatingBL rating)
        {
            if (null == rating) return;

            var item = new Rating() { Rank = rating.Rank, UserId = rating.UserId, WorkId = rating.WorkId };
            Dbcontext.UowRepositoryRatings.Create(item);
            Dbcontext.Save();
        }

        public void DeleteRating(int id)
        {
            if (0 >= id) return;

            Dbcontext.UowRepositoryRatings.Delete(id);
            Dbcontext.Save();
        }

        public void Dispose()
        {
            Dbcontext.Dispose();
        }

        public RatingBL GetRating(int id)
        {
            if (0 >= id) return new RatingBL();

            return AutoMapperBL<Rating, RatingBL>.Map(Dbcontext.UowRepositoryRatings.Get, id);
        }

        public IEnumerable<RatingBL> GetRatings()
        {
            return AutoMapperBL<IEnumerable<Rating>, List<RatingBL>>.Map(Dbcontext.UowRepositoryRatings.GetAll);
        }

        public void Update(RatingBL rating)
        {
            if (null == rating) return;

            var item = AutoMapperBL<RatingBL, Rating>.Map(rating);
            Dbcontext.UowRepositoryRatings.Update(item);
            Dbcontext.Save();
        }
    }
}
