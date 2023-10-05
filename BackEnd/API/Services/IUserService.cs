using API.Dtos;
using Dominio.Entities;

namespace API.Services;
    public interface IUserService{

        Task<string> RegisterAsync(RegisterDto model);
        Task<DataUserDto> GetTokenAsync(LoginDto model);
        Task<string> AddRoleAsync(AddRoleDto model);
        Task<Usuario> EditarUsuarioAsync(Usuario model);
        Task<DataUserDto> RefreshTokenAsync(string refreshToken);
        
    }
