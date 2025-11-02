using deliverySystem_Sharqiya.Dtos.Auth;
using deliverySystem_Sharqiya.Helpers;
using deliverySystem_Sharqiya.Helpers.ServiceContext;

namespace deliverySystem_Sharqiya.Services.Common.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<LoginOutput>> LoginAsync(LoginInput input);
        Task<ServiceResponse> ResetPasswordAsync(ResetPasswordInput input);
        Task<ServiceResponse> SetNewPasswordAsync(SetNewPasswordInput input);
    }
}
