using System.Security.Cryptography;
using System.Text;
using UserApi.Interfaces;
using UserApi.Models.DTO;
using UserApi.Models;
using System.Collections.Generic;

namespace UserApi.Services
{
    public class UserServices
    {
        private IUser<int, User> _repo;
        private ITokenGenerate _tokenService;

        public UserServices()
        {
        }

        public UserServices(IUser<int, User> iUser, ITokenGenerate tokenGenerate)
        {
            _repo = iUser;
            _tokenService = tokenGenerate;
        }
        public UserDTO Login(UserDTO userDTO)
        {
            UserDTO user= new UserDTO();
            var userData = _repo.Get(userDTO.UserId);
            if (userData != null)
            {
                var hmac = new HMACSHA512(userData.HashKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userPass[i] != userData.Password[i])
                        return null;
                }
                //user = new UserDTO();
                user.UserId = userDTO.UserId;
                user.Username = userData.Username;
                user.Role = userData.Role;
                user.Token = _tokenService.GenerateToken(user);
            }
            return user;
        }
        public UserDTO Register(UserRegisterDTO userDTO)
        {
            UserDTO user = null;
            var hmac = new HMACSHA512();
            userDTO.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.PasswordClear));
            userDTO.HashKey = hmac.Key;
            var resultUser = _repo.Add(userDTO);
            if (resultUser != null)
            {
                user = new UserDTO();
                user.UserId = resultUser.UserId;
                user.Username = resultUser.Username;
                user.Role = resultUser.Role;
                user.Token = _tokenService.GenerateToken(user);
            }
            return user;
        }
    }
}
