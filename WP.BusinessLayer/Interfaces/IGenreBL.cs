

using System.Collections.Generic;
using WP.BusinessLayer.ObjectValues;

namespace WP.BusinessLayer.Interfaces
{
    public interface IGenreBL
    {
        void Create(GenreBL genre);
        void Update(GenreBL genre);
        GenreBL GetGenre(int id);
        IEnumerable<GenreBL> GetGenres();
        void DeleteGenre(int id);
        void Dispose();
    }
}
