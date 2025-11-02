using CRM.Dtos.Common;
using deliverySystem_Sharqiya.Data;
using deliverySystem_Sharqiya.Dtos.User;
using deliverySystem_Sharqiya.Helpers;
using deliverySystem_Sharqiya.Helpers.Exceptions;
using deliverySystem_Sharqiya.Helpers.MessageHandler;
using deliverySystem_Sharqiya.Helpers.ServiceContext;
using deliverySystem_Sharqiya.Models;
using deliverySystem_Sharqiya.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace deliverySystem_Sharqiya.Services
{
    public class UserService(
        IMessageHandler messageHandler,
        DataContext dataContext,
        IServiceExecution serviceExecution
    ) : IUserService
    {
        private readonly IMessageHandler _messageHandler = messageHandler;
        private readonly DataContext _dataContext = dataContext;
        private readonly IServiceExecution _serviceExecution = serviceExecution;

        public RequestHeaderContent Header { get; set; }

        
        public async Task<ServiceResponse> CreateUserAsync(List<CreateUserInput> input)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {
                var users = new List<User>();

                foreach (var item in input)
                {
                    
                    var exists = await _dataContext.Users.AnyAsync(x => x.Email == item.Email);
                    if (exists)
                        Validation.ThrowError(ErrorMessage.AlreadyExists, "User with this email already exists.");

                    var user = new User
                    {
                        Name = item.Name,
                        Email = item.Email,
                        Password = item.Password, 
                        MobileNumber = item.MobileNumber,
                        Prefix = item.Prefix,
                        Type = item.Type,
                        Status = item.Status,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        LastLoginAt = DateTime.UtcNow
                    };

                    users.Add(user);
                }

                await _dataContext.Users.AddRangeAsync(users);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "New Users Created Successfully");
            },
            Header,
            logArgs: new { input });
        }

        
        public async Task<ServiceResponse<Pagination<GetUsersOutput>>> GetUsersAsync(GlobalFilterDto input)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {
                var query = _dataContext.Users.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(input.Search))
                {
                    query = query.Where(x =>
                        x.Name.Contains(input.Search) ||
                        x.Email.Contains(input.Search) ||
                        x.MobileNumber.Contains(input.Search));
                }

                var totalCount = await query.CountAsync();

                var users = await query
                    .OrderByDescending(x => x.Id)
                    .Skip((input.Page - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .Select(x => new GetUsersOutput
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        MobileNumber = x.MobileNumber,
                        Prefix = x.Prefix,
                        Type = x.Type.ToString(),
                        Status = x.Status.ToString(),
                        CreatedAt = x.CreatedAt,
                        LastLoginAt = x.LastLoginAt
                    })
                    .ToListAsync();

                var response = new Pagination<GetUsersOutput>(users, totalCount, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, response, "Users Retrieved Successfully");
            },
            Header,
            logArgs: new { input });
        }

        
        public async Task<ServiceResponse> UpdateUserAsync(int userId, UpdateUserInput input)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
                Validation.ThrowErrorIf(user is null, ErrorMessage.NotFound, "User");

                user.Name = input.Name ?? user.Name;
                user.Email = input.Email ?? user.Email;
                user.Password = input.Password ?? user.Password;
                user.MobileNumber = input.MobileNumber ?? user.MobileNumber;
                user.Prefix = input.Prefix ?? user.Prefix;
                user.Type = input.Type ?? user.Type;
                user.Status = input.Status ?? user.Status;
                user.UpdatedAt = DateTime.UtcNow;

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "User Updated Successfully");
            },
            Header,
            logArgs: new { userId, input });
        }

        
        public async Task<ServiceResponse> DeleteUserAsync(int userId)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
                Validation.ThrowErrorIf(user is null, ErrorMessage.NotFound, "User");

                user.Status = UserStatus.Deleted;
                user.UpdatedAt = DateTime.UtcNow;

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Deleted, "User Deleted Successfully");
            },
            Header,
            logArgs: new { userId });
        }
    }
}
