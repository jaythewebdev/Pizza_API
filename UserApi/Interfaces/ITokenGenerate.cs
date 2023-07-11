using UserApi.Models.DTO;

namespace UserApi.Interfaces
{
    public interface ITokenGenerate
    {
        public string GenerateToken(UserDTO user);
    }
}
