using ControllRR.Domain.Entities;

namespace ControllRR.Application.Interfaces;

public interface IUserService
{
     Task<List<UserDto>> FindAllAsync();
    Task<UserDto> FindByIdAsync(int id);
    Task InsertAsync(UserDto user);
}