
using WP.DataLayer.UnitOfWork;

namespace WP.BusinessLayer.Services
{
    public abstract class ABaseServiceBL
    {
        protected readonly IUnitOfWork Dbcontext;
        public ABaseServiceBL(IUnitOfWork unitOfWork)
        {
            Dbcontext = unitOfWork;
        }
    }
}
