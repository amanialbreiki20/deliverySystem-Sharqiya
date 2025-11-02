using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using deliverySystem_Sharqiya.Data;
using deliverySystem_Sharqiya.Dtos.Auth;
using deliverySystem_Sharqiya.Helpers;
using deliverySystem_Sharqiya.Helpers.Exceptions;
using deliverySystem_Sharqiya.Helpers.MessageHandler;
using deliverySystem_Sharqiya.Helpers.ServiceContext;
using deliverySystem_Sharqiya.Models;
using deliverySystem_Sharqiya.Security.ACL;
using deliverySystem_Sharqiya.Services.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace deliverySystem_Sharqiya.Services
{
    public class AuthService(IConfiguration configuration,
        IMessageHandler messageHandler,
        DataContext dataContext,
        IPasswordHasher<Models.User> hasher,
        IServiceExecution serviceExecution
        ) : IAuthService
    {
        private readonly IMessageHandler _messageHandler = messageHandler;
        private readonly DataContext _dataContext = dataContext;
        private readonly IServiceExecution _serviceExecution = serviceExecution;
        private readonly IPasswordHasher<Models.User> _hasher = hasher;
        private readonly IConfiguration _configuration = configuration;

        public async Task<ServiceResponse<LoginOutput>> LoginAsync(LoginInput input)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {
                var user = await _dataContext
                                 .Users
                                 .FirstOrDefaultAsync(x => x.Email == input.Email);

                var isPasswordMatched = user is not null && _hasher.VerifyHashedPassword(user, user.Password, input.Password) != PasswordVerificationResult.Failed;

                Validation.ThrowErrorIf(!isPasswordMatched || user.Status != UserStatus.Active, ErrorMessage.NotFound, "User");

                //var permissionCodes = await _dataContext
                //                            .UserPermissions
                //                            .Include(x => x.Permission)
                //                            .Where(x => x.UserId == user.Id)
                //                            .ToListAsync();

                var claims = await ProcessClaims(user);

                var jwtSettings = _configuration.GetSection("Jwt");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenBody = new JwtSecurityToken(
                                 jwtSettings["Issuer"],
                                 jwtSettings["Audience"],
                                 claims,
                                 expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
                                 signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenBody);

                var loginOutput = new LoginOutput
                {
                    Token = token,
                    //Permissions = permissionCodes,
                    UserType = user.Type,
                };

                user.LastLoginAt = DateTime.UtcNow;
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.UserSuccessfullyLoggedIn, loginOutput, "User");
            },
            null,
            logArgs: new
            {
                input
            });

        }
        public async Task<ServiceResponse> ResetPasswordAsync(ResetPasswordInput input)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {
                var user = await _dataContext
                                 .Users
                                 .Where(x => x.Email == input.Email && x.Status != UserStatus.Deleted)
                                 .FirstOrDefaultAsync();

                Validation.ThrowErrorIf(user is null, ErrorMessage.NotFound, "User");

                var resetToken = Guid.NewGuid().ToString();

                user.ResetPasswordToken = resetToken;
                user.ResetPasswordSentAt = DateTime.UtcNow;
                await _dataContext.SaveChangesAsync();

                //_ = _emailService.SendResetPasswordEmailAsync(new SendResetPasswordEmailInput
                //{
                //    Email = user.Email,
                //    Path = _configuration.GetSection("Paths")["ResetPassword"] + '/' + resetToken,
                //});

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Reset Password Request");
            },
            null,
           logArgs: new
           {
               input
           });
        }
        public async Task<ServiceResponse> SetNewPasswordAsync(SetNewPasswordInput input)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {
                var user = await _dataContext
                                 .Users
                                 .Where(x => x.ResetPasswordToken == input.Token && x.Status != UserStatus.Deleted)
                                 .FirstOrDefaultAsync();

                Validation.ThrowErrorIf(user is null, ErrorMessage.DateExpired, "Token");

                Validation.ThrowErrorIf((DateTime.UtcNow - user.ResetPasswordSentAt).Value.Minutes > 10, ErrorMessage.DateExpired, "Token");


                user.Password = _hasher.HashPassword(user, input.Password);
                user.ResetPasswordToken = null;
                user.ResetPasswordSentAt = null;

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Password");
            },
            null,
           logArgs: new
           {
               input
           });
        }
        private async Task<List<Claim>> ProcessClaims(Models.User user)
        {
            var claims = new List<Claim>
            {
                new Claim(Helpers.Constants.UserId, user.Id.ToString()),
                new Claim(Helpers.Constants.UserType, user.Type.ToString()),
                new Claim(Helpers.Constants.UserMobileNumber, user.MobileNumber.ToString()),
                new Claim(Helpers.Constants.UserEmail, user.Email.ToString()),
            };

            return claims;
        }


    }
}
