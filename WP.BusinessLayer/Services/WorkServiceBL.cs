using System.Collections.Generic;
using WP.BusinessLayer.Interfaces;
using WP.BusinessLayer.ObjectValues;
using WP.DataLayer.Entities;
using WP.DataLayer.UnitOfWork;
namespace WP.BusinessLayer.Services
{
    public class WorkServiceBL: ABaseServiceBL, IWorkBL
    {
        public WorkServiceBL(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public void Create(WorkBL work)
        {
            if (null == work) return;

            var item = new Work() { Title = work.Title, DateOfPublication = work.DateOfPublication, Content = work.Content, GenreId = work.GenreId, UserId = work.UserId };
            Dbcontext.UowRepositoryWorks.Create(item);
            Dbcontext.Save();
        }

        public void DeleteWork(int id)
        {
            if (0 >= id) return;
        }

        public void Dispose()
        {
            Dbcontext.Dispose();
        }

        public WorkBL GetWork(int id)
        {
            if (0 >= id) return new WorkBL();

            return AutoMapperBL<Work, WorkBL>.Map(Dbcontext.UowRepositoryWorks.Get(id));
        }

        public IEnumerable<WorkBL> GetWorks()
        {
           return AutoMapperBL<IEnumerable<Work>, List<WorkBL>>.Map(Dbcontext.UowRepositoryWorks.GetAll);
        }

        public void Update(WorkBL work)
        {
            if (null == work) return;

            var item = AutoMapperBL<WorkBL, Work>.Map(work);
            Dbcontext.UowRepositoryWorks.Update(item);
            Dbcontext.Save();
        }
    }
}
