namespace Identity.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}
