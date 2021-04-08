using System.Collections.Generic;
using WP.BusinessLayer.Interfaces;
using WP.BusinessLayer.ObjectValues;
using WP.DataLayer.Entities;
using WP.DataLayer.UnitOfWork;

namespace WP.BusinessLayer.Services
{
    public class GenreServiceBL : IGenreBL
    {
        private IUnitOfWork _db;

        public GenreServiceBL(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }
        public void Create(GenreBL genre)
        {
            var item = new Genre() { Name = genre.Name };
            _db.UowRepositoryGenres.Create(item);
            _db.Save();
        }

        public void DeleteGenre(int id)
        {
            _db.UowRepositoryGenres.Delete(id);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public GenreBL GetGenre(int id)
        {
            return AutoMapperBL<Genre, GenreBL>.Map(_db.UowRepositoryGenres.Get, id);
        }

        public IEnumerable<GenreBL> GetGenres()
        {
            return AutoMapperBL<IEnumerable<Genre>, List<GenreBL>>.Map(_db.UowRepositoryGenres.GetAll);
        }

        public void Update(GenreBL genre)
        {
            var item = AutoMapperBL<GenreBL, Genre>.Map(genre);
            _db.UowRepositoryGenres.Update(item);
            _db.Save();
        }
    }
}
