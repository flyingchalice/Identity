using Identity.Domain.Exceptions;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityContext _context;

        private IUserRepository _userRepository;

        public UnitOfWork(IdentityContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new BadRequestException($"Entity data is not valid. {e.InnerException?.Message}");
            }
        }
    }
}
