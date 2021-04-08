
using System;
using WP.DataLayer.Entities;
using WP.DataLayer.Repository;

namespace WP.DataLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ModelDb _db;
        private bool _disposed = false;

        Repository<User> _usersCabinets;
        Repository<Comment> _commentsLogs;
        Repository<Genre> _genres;
        Repository<Work> _works;
        Repository<Rating> _ratings;

        public UnitOfWork(string connectionString)
        {
            _db = new ModelDb(connectionString);
        }


        public Repository<User> UowRepositoryUsers 
        {
            get
            {
                if (this._usersCabinets == null)
                    _usersCabinets = new Repository<User>(_db);
                return _usersCabinets;
            }
        }

        public Repository<Comment> UowRepositoryComments
        {
            get
            {
                if (_commentsLogs == null)
                    _commentsLogs = new Repository<Comment>(_db);
                return _commentsLogs;
            }
        }
        public Repository<Genre> UowRepositoryGenres 
        {
            get
            {
                if (_genres == null)
                    _genres = new Repository<Genre>(_db);
                return _genres;
            }
        }

        public Repository<Work> UowRepositoryWorks 
        {
            get
            {
                if (_works == null)
                    _works = new Repository<Work>(_db);
                return _works;
            }
        }

        public Repository<Rating> UowRepositoryRatings 
        {
            get
            {
                if (_ratings == null)
                    _ratings = new Repository<Rating>(_db);
                return _ratings;
            }
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
