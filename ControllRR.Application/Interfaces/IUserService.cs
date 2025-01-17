using ControllRR.Domain.Entities;

namespace ControllRR.Application.Interfaces;

public interface IUserService
{
     Task<List<User>> FindAllAsync();
    Task<User> FindByIdAsync(int id);
    Task InsertAsync(User user);
}