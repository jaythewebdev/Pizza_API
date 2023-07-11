using Microsoft.AspNetCore.Mvc;
using Moq;
using UserApi.Controllers;
using UserApi.Models;
using UserApi.Models.DTO;
using UserApi.Services;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserUnitTesting
{
    [TestFixture]
    public class Tests
    {

        private UserServices _userServices;
        private UserController _userController;

        [SetUp]
        public void Setup()
        {
            _userServices=new UserServices();
        }

        //[Test]
        //public async Task Register_ValidUser_ReturnsOkResult()
        //{
        //    // Arrange
        //    var user = new UserRegisterDTO
        //    {
        //        Username = "jay",
        //        PhoneNumber = "9087654321",
        //        Name = "jayes",
        //        Age = 20,
        //        Role = "User",
        //        PasswordClear = "jay123"
        //    };

        //    // Act
        //    var result = _userServices.Register(user);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(user.Username,result.Username);
        //    Assert.AreEqual(user.Role,result.Role);

        //}

        private string GenerateJwtToken(string userId, string role)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Role, role)
        };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("My token key which is used in JWT"); // Replace with your actual secret key
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [Test]
        public void RegisterUser_ValidData_Success()
        {
            // Arrange
            var user = new UserRegisterDTO
            {
                Username = "johnDoe",
                Role = "User",
            };

            // Act
            var result = _userServices.Register(user);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Token);

            // Verify token validity and contents
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("My token key which is used in JWT")) // Replace with your actual secret key
            };

            ClaimsPrincipal claimsPrincipal;
            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(result.Token, tokenValidationParameters, out var validatedToken);
            }
            catch (Exception)
            {
                Assert.Fail("Token validation failed");
                return;
            }

            Assert.IsTrue(claimsPrincipal.Identity.IsAuthenticated);
            Assert.AreEqual(user.Name, claimsPrincipal.Identity.Name);
            Assert.IsTrue(claimsPrincipal.IsInRole("User"));
        }

        //[Test]
        //public async Task Login_ValidCredentials_ReturnsOkResult()
        //{
        //    // Arrange
        //    var user = new UserDTO
        //    {
        //        UserId = 1,
        //        Username = "johnDoe",
        //        Password="john123",
        //        Token = "ddssdfsfdsf",
        //        Role = "User",
        //    };

        //    // Act
        //    var result = _userServices.Login(user);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(user.Username, result.Username);
        //    Assert.AreEqual(user.Role, result.Role);
        //}
    }
}
