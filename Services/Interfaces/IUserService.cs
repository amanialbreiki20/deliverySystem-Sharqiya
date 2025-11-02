using CRM.Dtos.Common;
using deliverySystem_Sharqiya.Dtos.User;
using deliverySystem_Sharqiya.Helpers;
using deliverySystem_Sharqiya.Helpers.ServiceContext;

namespace deliverySystem_Sharqiya.Services.Interfaces
{
    public interface IUserService : IRequestHeader
    {
        Task<ServiceResponse> CreateUserAsync(List<CreateUserInput> input);
        Task<ServiceResponse<Pagination<GetUsersOutput>>> GetUsersAsync(GlobalFilterDto input);
        Task<ServiceResponse> UpdateUserAsync(int userId, UpdateUserInput input);
        Task<ServiceResponse> DeleteUserAsync(int userId);
    }
}
