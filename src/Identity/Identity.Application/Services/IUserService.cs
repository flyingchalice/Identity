using Identity.Domain.Entities;

namespace Identity.Application.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
    }
}
