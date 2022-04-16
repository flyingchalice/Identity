using Identity.Infrastructure.Repositories;

namespace Identity.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        void SaveChanges();
    }
}
