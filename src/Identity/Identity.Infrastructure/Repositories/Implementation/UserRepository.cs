using Identity.Domain.Entities;

namespace Identity.Infrastructure.Repositories.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IdentityContext context) : base(context)
        {
        }
    }
}
