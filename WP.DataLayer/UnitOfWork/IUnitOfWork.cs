
using System;
using WP.DataLayer.Entities;
using WP.DataLayer.Repository;

namespace WP.DataLayer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Repository<User> UowRepositoryUsers { get; }
        Repository<Comment> UowRepositoryComments { get; }
        Repository<Genre> UowRepositoryGenres { get; }
        Repository<Work> UowRepositoryWorks { get; }
        Repository<Rating> UowRepositoryRatings { get; }
        void Save();
    }
}
