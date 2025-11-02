namespace deliverySystem_Sharqiya.Helpers.ServiceContext
{
    public interface IServiceExecution
    {
        Task<ServiceResponse> ExecuteAsync(
            Func<Task<ServiceResponse>> action,
            RequestHeaderContent serviceHeader,
            object logArgs = null);

        Task<ServiceResponse<T>> ExecuteAsync<T>(
            Func<Task<ServiceResponse<T>>> action,
            RequestHeaderContent serviceHeader,
            object logArgs = null);
    }
}
