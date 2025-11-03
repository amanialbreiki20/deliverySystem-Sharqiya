using CRM.Dtos.Common;
using deliverySystem_Sharqiya.Dtos.Order;
using deliverySystem_Sharqiya.Helpers;
using deliverySystem_Sharqiya.Helpers.MessageHandler;
using deliverySystem_Sharqiya.Helpers.ServiceContext;

namespace deliverySystem_Sharqiya.Services.Interfaces
{
    public interface IOrderService
    {
        RequestHeaderContent Header { get; set; }

        Task<ServiceResponse> CreateOrderAsync(List<CreateOrderInput> input);
        Task<ServiceResponse<Pagination<GetOrdersOutput>>> GetOrdersAsync(GlobalFilterDto input);
        Task<ServiceResponse> UpdateOrderAsync(int orderId, UpdateOrderInput input);
        Task<ServiceResponse> DeleteOrderAsync(int orderId);
    }
}
