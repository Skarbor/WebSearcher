using WebSearcher.DataAccess.Abstract;

namespace WebSearcher.DataAccess.Concrete
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new UnitOfWork();
        }
    }
}
