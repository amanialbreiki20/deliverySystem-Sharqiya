namespace deliverySystem_Sharqiya.Helpers
{
    public class ServiceResponse : ApiResponse
    {
        public bool Succeed { get; private set; } // Contains if the functions succeed or failed.

        public ServiceResponse(bool success, int code, string description) : base(code, description)
        {
            Succeed = success;
        }

    }

    /// ServiceResponse<T> with T inherenting from ServiceResponse,
    /// <T> added to make it strong type service response which return specific type of results
    public class ServiceResponse<T> : ServiceResponse
    {
        public T Result { get; private set; } // Contains the result of the function based on their type.

        public ServiceResponse(bool success, T result, int code, string description) : base(success, code, description)
        {
            Result = result;
        }

    }

    public class ServiceResponse<T, R> : ServiceResponse
    {
        public T Result { get; private set; } // Contains the result of the function based on their type.
        public R Metadata { get; private set; } // Contains result metadata

        public ServiceResponse(bool success, T result, int code, string description, R metadata) : base(success, code, description)
        {
            Result = result;
            Metadata = metadata;
        }
    }
}
