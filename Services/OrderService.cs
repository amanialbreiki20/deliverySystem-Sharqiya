using CRM.Dtos.Common;
using deliverySystem_Sharqiya.Data;
using deliverySystem_Sharqiya.Dtos.Order;
using deliverySystem_Sharqiya.Helpers;
using deliverySystem_Sharqiya.Helpers.Exceptions;
using deliverySystem_Sharqiya.Helpers.MessageHandler;
using deliverySystem_Sharqiya.Helpers.ServiceContext;
using deliverySystem_Sharqiya.Models;
using deliverySystem_Sharqiya.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace deliverySystem_Sharqiya.Services
{
    public class OrderService(
        IMessageHandler messageHandler,
        DataContext dataContext,
        IServiceExecution serviceExecution
        ) : IOrderService
    {
        private readonly IMessageHandler _messageHandler = messageHandler;
        private readonly DataContext _dataContext = dataContext;
        private readonly IServiceExecution _serviceExecution = serviceExecution;

        public RequestHeaderContent Header { get; set; }

        public async Task<ServiceResponse> CreateOrderAsync(List<CreateOrderInput> input)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {
                var orders = new List<Order>();

                foreach (var item in input)
                {
                    var order = new Order
                    {
                        Tips = item.Tips,
                        Cash = item.Cash,
                        DriverId = item.DriverId,
                        UserId = item.UserId,
                        Date = item.Date,
                        Status = OrderStatus.Completed,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    orders.Add(order);
                }

                await _dataContext.Orders.AddRangeAsync(orders);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "New Orders");
            },
            Header,
            logArgs: new { input });
        }

        public async Task<ServiceResponse<Pagination<GetOrdersOutput>>> GetOrdersAsync(GlobalFilterDto input)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {
                var query = _dataContext.Orders
                    .Include(x => x.Driver)
                    .Include(x => x.User)
                    .AsNoTracking();

                if (!string.IsNullOrWhiteSpace(input.Search))
                {
                    query = query.Where(x =>
                        x.Driver.Name.Contains(input.Search) ||
                        x.User.Name.Contains(input.Search));
                }

                var totalCount = await query.CountAsync();

                var orders = await query
                    .OrderByDescending(x => x.Id)
                    .Skip((input.Page - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .Select(x => new GetOrdersOutput
                    {
                        Id = x.Id,
                        Tips = x.Tips,
                        Cash = x.Cash,
                        Status = x.Status,
                        DriverName = x.Driver.Name,
                        UserName = x.User.Name,
                        Date = x.Date,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    })
                    .ToListAsync();

                var response = new Pagination<GetOrdersOutput>(orders, totalCount, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, response, "Orders");
            },
            Header,
            logArgs: new { input });
        }

        public async Task<ServiceResponse> UpdateOrderAsync(int orderId, UpdateOrderInput input)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {
                var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

                Validation.ThrowErrorIf(order is null, ErrorMessage.NotFound, "Order");

                order.Tips = input.Tips;
                order.Cash = input.Cash;
                order.DriverId = input.DriverId;
                order.Status = input.Status;
                order.Date = input.Date;
                order.UpdatedAt = DateTime.UtcNow;

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Order updated successfully");
            },
            Header,
            logArgs: new { orderId, input });
        }

        public async Task<ServiceResponse> DeleteOrderAsync(int orderId)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {
                var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

                Validation.ThrowErrorIf(order is null, ErrorMessage.NotFound, "Order");

                order.Status = OrderStatus.Deleted;
                order.UpdatedAt = DateTime.UtcNow;

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Deleted, "Order deleted successfully");
            },
            Header,
            logArgs: new { orderId });
        }
    }
}
