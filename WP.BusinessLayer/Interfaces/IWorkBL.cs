
using System.Collections.Generic;
using WP.BusinessLayer.ObjectValues;

namespace WP.BusinessLayer.Interfaces
{
    public interface IWorkBL
    {
        void Create(WorkBL work);
        void Update(WorkBL work);
        WorkBL GetWork(int id);
        IEnumerable<WorkBL> GetWorks();
        void DeleteWork(int id);
        void Dispose();
    }
}
