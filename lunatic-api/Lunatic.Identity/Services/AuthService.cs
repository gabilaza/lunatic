using Lunatic.Application.Contracts.Identity;
using Lunatic.Application.Models.Identity;
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lunatic.Application.Responses.Identity;


namespace Lunatic.Identity.Services {
    public class AuthService : IAuthService {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IUserRepository userRepository) {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.userRepository = userRepository;
        }

        public async Task<RegisterResponse> Registeration(RegistrationModel model, string role) {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if(userExists != null) {
                return new RegisterResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "User already exists" }
                };
            }

            var userDb = User.Create(model.FirstName, model.LastName, model.Email, model.Username, model.Password, Role.USER);
            if(!userDb.IsSuccess) {
                return new RegisterResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Can't create a user" }
                };
            }

            ApplicationUser user = new ApplicationUser() {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = userDb.Value.UserId.ToString(),
                UserName = model.Username,
            };

            var createUserResult = await userManager.CreateAsync(user, model.Password);
            if(!createUserResult.Succeeded) {
                return new RegisterResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "User creation failed! Please check user details and try again." }
                };
            }

            if(!await roleManager.RoleExistsAsync(role)) {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            if(await roleManager.RoleExistsAsync(UserRoles.User)) {
                await userManager.AddToRoleAsync(user, role);
            }

            await this.userRepository.AddAsync(userDb.Value);

            return new RegisterResponse {
                Success = true,
            };
        }

        public async Task<LoginResponse> Login(LoginModel model) {
            var user = await userManager.FindByNameAsync(model.Username!);
            if(user == null) {
                return new LoginResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Invalid username" }
                };
            }
            if(!await userManager.CheckPasswordAsync(user, model.Password!)) {
                return new LoginResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Invalid password" }
                };
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim> {
               new Claim(ClaimTypes.Name, user.UserName!),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles) {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string token = GenerateToken(authClaims);
            return new LoginResponse {
                Success = true,
                Token = token,
                UserId = user.Id
            };
        }

        private string GenerateToken(IEnumerable<Claim> claims) {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));

            var tokenDescriptor = new SecurityTokenDescriptor {
                Issuer = configuration["JWT:ValidIssuer"]!,
                Audience = configuration["JWT:ValidAudience"]!,
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
