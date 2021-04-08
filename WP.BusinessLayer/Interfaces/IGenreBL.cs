

using System.Collections.Generic;

namespace WP.BusinessLayer.Interfaces
{
    public interface IBaseEntityBL<T> where T : class
    {
        void CreateTEntityBL(T entityBL);
        void UpdateTEntityBL(T entityBL);
        T GetTEntityBL(int id);
        IEnumerable<T> GetTEntitysBL();
        void DeleteTEntitysBL(int id);
        void Dispose();
    }
}
