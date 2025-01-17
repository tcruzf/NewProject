using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository )
    {
        _userRepository = userRepository;
    }
    public async Task<List<User>> FindAllAsync()
    {
        return await _userRepository.FindAllAsync();
    }

    public async Task<User> FindByIdAsync(int id)
    {
       
        return await _userRepository.FindByIdAsync(id);
       
    }

    public async Task InsertAsync(User user)
    {
        _userRepository.InsertAsync(user);
        await _userRepository.SaveChangesAsync();

        

    }

}