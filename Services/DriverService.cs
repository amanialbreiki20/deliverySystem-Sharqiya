using CRM.Dtos.Common;
using deliverySystem_Sharqiya.Data;
using deliverySystem_Sharqiya.Dtos.Driver;
using deliverySystem_Sharqiya.Helpers;
using deliverySystem_Sharqiya.Helpers.Exceptions;
using deliverySystem_Sharqiya.Helpers.MessageHandler;
using deliverySystem_Sharqiya.Helpers.ServiceContext;
using deliverySystem_Sharqiya.Models;
using deliverySystem_Sharqiya.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace deliverySystem_Sharqiya.Services
{
    public class DriverService(IMessageHandler messageHandler,
        DataContext dataContext,
        IServiceExecution serviceExecution
        ) : IDriverService
    {
        private readonly IMessageHandler _messageHandler = messageHandler;
        private readonly DataContext _dataContext = dataContext;
        private readonly IServiceExecution _serviceExecution = serviceExecution;

        public RequestHeaderContent Header { get; set; }

        public async Task<ServiceResponse> CreateDriverAsync(List<CreateDriverInput> input)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {

                var drivers = new List<Driver>();

                foreach (var item in input)
                {
                    var driver = new Driver
                    {
                        Name = item.Name,
                        PersonalNumber = item.PersonalNumber,
                        ResidentId = item.ResidentId,
                        Status = DriverStatus.Active,
                        Talabatid = item.Talabatid,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    };

                    drivers.Add(driver);
                }


                await _dataContext.Drivers.AddRangeAsync(drivers);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "New Drivers");
            },
            Header,
            logArgs: new
            {
                input
            });
        }
        public async Task<ServiceResponse<Pagination<GetDriversOutput>>> GetDriversAsync(GlobalFilterDto input)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {
                var query = _dataContext
                           .Drivers
                           .AsNoTracking();

                if (!string.IsNullOrWhiteSpace(input.Search))
                {
                    query = query.Where(x => x.Name.Contains(input.Search) || x.PersonalNumber.Contains(input.Search) 
                                          || x.ResidentId.Contains(input.Search) || x.Talabatid.Contains(input.Search));
                }

                var totalCount = await query.CountAsync();

                var drivers = await query
                                    .OrderByDescending(x => x.Id)
                                    .Skip((input.Page - 1) * input.PageSize)
                                    .Take(input.PageSize)
                                    .Select(x => new GetDriversOutput
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                        Talabatid = x.Talabatid,
                                        ResidentId = x.ResidentId,
                                        PersonalNumber = x.PersonalNumber,
                                        Status = x.Status,
                                        CreatedAt = x.CreatedAt,
                                        UpdatedAt = x.UpdatedAt,
                                    })
                                    .ToListAsync();

                var response = new Pagination<GetDriversOutput>(drivers, totalCount, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, response, "Projects");
            },
            Header,
            logArgs: new
            {
                input
            });
        }
        public async Task<ServiceResponse> UpdateDriverAsync(int driverId, UpdateDriverInput input)
        {
            return await _serviceExecution.ExecuteAsync(async () =>
            {
                var driver = await _dataContext.Drivers.FirstOrDefaultAsync(x => x.Id == driverId);

                Validation.ThrowErrorIf(driver is null, ErrorMessage.NotFound, "Driver");

                driver.Name = input.Name;
                driver.PersonalNumber = input.PersonalNumber;
                driver.ResidentId = input.ResidentId;
                driver.Talabatid = input.Talabatid;
                driver.UpdatedAt = DateTime.UtcNow;

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Driver updated successfully");
            },
            Header,
            logArgs: new
            {
                driverId,
                input
            });
        }
    }
}
