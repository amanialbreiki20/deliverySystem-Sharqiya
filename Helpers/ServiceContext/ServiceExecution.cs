using deliverySystem_Sharqiya.Helpers.Exceptions;
using deliverySystem_Sharqiya.Helpers.MessageHandler;

namespace deliverySystem_Sharqiya.Helpers.ServiceContext
{
    public class ServiceExecution : IServiceExecution
    {
        private readonly ILogger<ServiceExecution> _logger;
        private readonly IMessageHandler _messageHandler;

        public ServiceExecution(ILogger<ServiceExecution> logger, IMessageHandler messageHandler)
        {
            _logger = logger;
            _messageHandler = messageHandler;
        }

        public async Task<ServiceResponse> ExecuteAsync(
            Func<Task<ServiceResponse>> action,
            RequestHeaderContent header,
            object logArgs = null)
        {
            try
            {
                _logger.LogInformation("{EndpointName} ({UserPhone}) Start Execution \n{@Details}", header?.EndPointName, header?.UserEmail, logArgs);

                var response = await action();


                return response;
            }
            catch (Validation err)
            {
                _logger.LogWarning("{EndpointName} ({UserPhone}) Validation Error: {ErrorMessage}\n{@Details}", header?.EndPointName, header?.UserEmail, err.Message, logArgs);


                return _messageHandler.GetServiceResponse(err.ErrorCode, err.Args);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{EndpointName} ({UserPhone}) Exception Error: {ExceptionMessage}\n{@Details}", header?.EndPointName, header?.UserEmail, ex.Message, logArgs);


                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError);
            }
        }

        public async Task<ServiceResponse<T>> ExecuteAsync<T>(
            Func<Task<ServiceResponse<T>>> action,
            RequestHeaderContent header,
            object logArgs = null)
        {
            try
            {
                _logger.LogInformation("{EndpointName} ({UserPhone}) Start Execution \n{@Details}", header?.EndPointName, header?.UserEmail, logArgs);

                var response = await action();


                return response;
            }
            catch (Validation err)
            {
                _logger.LogWarning("{EndpointName} ({UserPhone}) Validation Error: {ErrorMessage}\n{@Details}", header?.EndPointName, header?.UserEmail, err.Message, logArgs);

                return _messageHandler.GetServiceResponse<T>(err.ErrorCode, default, err.Args);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{EndpointName} ({UserPhone}) Exception Error: {ExceptionMessage}\n{@Details}", header?.EndPointName, header?.UserEmail, ex.Message, logArgs);

                return _messageHandler.GetServiceResponse<T>(ErrorMessage.ServerInternalError, default);
            }
        }


    }
}
