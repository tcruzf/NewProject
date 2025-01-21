using AutoMapper;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Application.Dto;

namespace ControllRR.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<List<UserDto>> FindAllAsync()
    {
        var user = await  _userRepository.FindAllAsync();
        return _mapper.Map<List<UserDto>>(user);

    }

    public async Task<UserDto> FindByIdAsync(int id)
    {
       
        var user = await _userRepository.FindByIdAsync(id);
        return _mapper.Map<UserDto>(user);
        
       
    }

    public async Task InsertAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);

       await _userRepository.InsertAsync(user);
        await _userRepository.SaveChangesAsync();

        

    }

    public async Task RemoveAsync(int id)
    {
       await _userRepository.RemoveAsync(id);
      // await _userRepository.SaveChangesAsync();

    }

}