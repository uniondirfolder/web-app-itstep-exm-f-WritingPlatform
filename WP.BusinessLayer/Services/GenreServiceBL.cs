using System.Collections.Generic;
using WP.BusinessLayer.Interfaces;
using WP.BusinessLayer.ObjectValues;
using WP.DataLayer.Entities;
using WP.DataLayer.UnitOfWork;

namespace WP.BusinessLayer.Services
{
    public class GenreServiceBL : ABaseServiceBL, IGenreBL
    {
        public GenreServiceBL(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public void Create(GenreBL genre)
        {
            if (null == genre) return;

            var item = new Genre() { Name = genre.Name };
            Dbcontext.UowRepositoryGenres.Create(item);
            Dbcontext.Save();
        }

        public void DeleteGenre(int id)
        {
            if (0 >= id) return;

            Dbcontext.UowRepositoryGenres.Delete(id);
            Dbcontext.Save();
        }

        public void Dispose()
        {
            Dbcontext.Dispose();
        }

        public GenreBL GetGenre(int id)
        {
            if (0 >= id) return new GenreBL();

            return AutoMapperBL<Genre, GenreBL>.Map(Dbcontext.UowRepositoryGenres.Get, id);
        }

        public IEnumerable<GenreBL> GetGenres()
        {
            return AutoMapperBL<IEnumerable<Genre>, List<GenreBL>>.Map(Dbcontext.UowRepositoryGenres.GetAll);
        }

        public void Update(GenreBL genre)
        {
            var item = AutoMapperBL<GenreBL, Genre>.Map(genre);
            Dbcontext.UowRepositoryGenres.Update(item);
            Dbcontext.Save();
        }
    }
}
