namespace deliverySystem_Sharqiya.Helpers.MessageHandler
{
    public interface IMessageHandler
    {
        string GetMessage(ErrorMessage code, string extraMessage = "");

        string GetMessage(SuccessMessage code, string extraMessage = "");

        ServiceResponse GetServiceResponse(SuccessMessage successMessage, string note = null);
        ServiceResponse<T> GetServiceResponse<T>(SuccessMessage successMessage, T result, string note = null);
        ServiceResponse<T> GetServiceResponse<T>(ErrorMessage errorMessage, T result, string notes = null);
        ServiceResponse GetServiceResponse(ErrorMessage errorMessage, string note = null);
    }

    // Define your error code here
    public enum ErrorMessage
    {
        BadRequest = 4000,
        Unauthorized = 4001,
        Forbidden = 4003,
        NotFound = 4004,
        AlreadyExists = 4005,



        WrongPassword = 4006,

        UnableToAdd = 4007,
        UnableToUpdate = 4008,
        UnableToRetrieve = 4009,
        UnableToDelete = 4010,
        NoPermissions = 4011,

        UnVerifiedUserLogin = 4016,
        VerificationTokenPassed30Min = 4017,
        InValidImageFormat = 4018,
        Inactive = 4033,
        UnableToParse = 4036,
        DateIsInvalid = 4040,
        DateExpired = 4041,
        Required = 4050,
        Failed = 4054,
        UserNotRegistered = 4066,
        CheckerCannotInitiateRequest = 4067,
        GenericError = 4071,

        ServerInternalError = 5000,
        NotActive = 5100,

    }

    // Define your Success code here
    public enum SuccessMessage
    {
        Retrieved = 2000,
        Created = 2001,
        Updated = 2002,
        Deleted = 2003,
        Generated = 2004,

        UserSuccessfullyLoggedIn = 2005,
        UserSuccessfullyRegistered = 2006,
        PasswordChangedSuccessfully = 2007,
        UserSuccesffulyLoggedOut = 2008,
        UserPassedVerification = 2009,
        ValidImageFormat = 2010,
        Approved = 2011,
        Rejected = 2012,

        GenericSuccessMessage = 2401,


    }
}
