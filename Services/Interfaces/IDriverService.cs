using CRM.Dtos.Common;
using deliverySystem_Sharqiya.Dtos.Driver;
using deliverySystem_Sharqiya.Helpers;
using deliverySystem_Sharqiya.Helpers.ServiceContext;

namespace deliverySystem_Sharqiya.Services.Interfaces
{
    public interface IDriverService : IRequestHeader
    {
        Task<ServiceResponse> CreateDriverAsync(List<CreateDriverInput> input);
        Task<ServiceResponse<Pagination<GetDriversOutput>>> GetDriversAsync(GlobalFilterDto input);
        Task<ServiceResponse> UpdateDriverAsync(int driverId, UpdateDriverInput input);
    }
}
