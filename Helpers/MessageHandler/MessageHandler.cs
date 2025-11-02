using deliverySystem_Sharqiya.Helpers;

namespace deliverySystem_Sharqiya.Helpers.MessageHandler
{
    public class MessageHandler : IMessageHandler
    {
        // Define you error message here based on your error code
        public string GetMessage(ErrorMessage code, string extraMessage = "")
        {
            return code switch
            {
                ErrorMessage.BadRequest => extraMessage,

                ErrorMessage.Unauthorized => "Invalid username or password",

                ErrorMessage.NotFound => GetFormat("{0} not found", extraMessage),

                ErrorMessage.Forbidden => "Access denied",

                ErrorMessage.AlreadyExists => GetFormat("{0} already exists", extraMessage),

                ErrorMessage.WrongPassword => "Old Password Is not correct",

                ErrorMessage.UnableToAdd => GetFormat("Unable to add {0}", extraMessage),

                ErrorMessage.UnableToUpdate => GetFormat("Unable to update {0}", extraMessage),

                ErrorMessage.UnableToRetrieve => GetFormat("Unable to retrieve {0}", extraMessage),

                ErrorMessage.UnableToDelete => GetFormat("Unable to delete {0}", extraMessage),

                ErrorMessage.NoPermissions => "You don't have permissions to perform this action",

                ErrorMessage.UnVerifiedUserLogin => "You need to confirm your account. We have sent you an activation code, please check your email",

                ErrorMessage.VerificationTokenPassed30Min => "Verification token passed 30 min",

                ErrorMessage.InValidImageFormat => "Invalid image file format",

                ErrorMessage.Inactive => "Inactive",

                ErrorMessage.UnableToParse => GetFormat("Unable to parse {0}", extraMessage),

                ErrorMessage.DateIsInvalid => GetFormat("{0} Is Invaild ", extraMessage),

                ErrorMessage.DateExpired => GetFormat("{0} Date Expired", extraMessage),

                ErrorMessage.Required => GetFormat("{0} is Required", extraMessage),

                ErrorMessage.Failed => GetFormat("{0} Failed", extraMessage),

                ErrorMessage.UserNotRegistered => GetFormat("User Not Registered {0}", extraMessage),

                ErrorMessage.CheckerCannotInitiateRequest => "Checker Cannot Initiate Request",

                ErrorMessage.ServerInternalError => GetFormat("There is an Error happend during proccesing the request {0}", extraMessage),
                ErrorMessage.NotActive => GetFormat("{0} is not active", extraMessage),
                ErrorMessage.GenericError => GetFormat("{0}", extraMessage),
                _ => "Unkown Error Message",
            };
        }

        // Define your success here based on your success code
        public string GetMessage(SuccessMessage code, string extraMessage = "")
        {
            return code switch
            {
                SuccessMessage.Retrieved => GetFormat("{0} retrieved successfully", extraMessage),

                SuccessMessage.Created => GetFormat("{0} added successfully", extraMessage),

                SuccessMessage.Updated => GetFormat("{0} updated successfully", extraMessage),

                SuccessMessage.Deleted => GetFormat("{0} deleted successfully", extraMessage),

                SuccessMessage.Generated => GetFormat("{0} generated successfully", extraMessage),

                SuccessMessage.UserSuccessfullyLoggedIn => "Loggedin successfully",

                SuccessMessage.UserSuccessfullyRegistered => "User registered successully",

                SuccessMessage.PasswordChangedSuccessfully => "Password changed successfully",

                SuccessMessage.UserSuccesffulyLoggedOut => "User loggedout succesffuly",

                SuccessMessage.UserPassedVerification => "User passed verification",

                SuccessMessage.ValidImageFormat => "Image sent is valid",

                SuccessMessage.Approved => GetFormat("{0} Approved successfully", extraMessage),

                SuccessMessage.Rejected => GetFormat("{0} Rejected successfully", extraMessage),

                SuccessMessage.GenericSuccessMessage => GetFormat("{0}", extraMessage),
                _ => "Unkown Success Message",
            };
        }

        public string GetFormat(string message, string extraMessage)
        {
            return string.Format(message, extraMessage);
        }
        public ServiceResponse<T> GetServiceResponse<T>(ErrorMessage errorMessage, T result, string notes = null)
        {
            return new ServiceResponse<T>(
                success: false,
                result: result,
                code: (int)errorMessage,
                description: GetMessage(errorMessage, notes));
        }
        public ServiceResponse GetServiceResponse(ErrorMessage errorMessage, string note = null)
        {
            return new ServiceResponse(
                success: false,
                code: (int)errorMessage,
                description: GetMessage(errorMessage, note));
        }
        public ServiceResponse GetServiceResponse(SuccessMessage successMessage, string note = null)
        {
            return new ServiceResponse(
                success: true,
                code: (int)successMessage,
                description: GetMessage(successMessage, note));
        }
        public ServiceResponse<T> GetServiceResponse<T>(SuccessMessage successMessage, T result, string note = null)
        {
            return new ServiceResponse<T>(
                success: true,
                result: result,
                code: (int)successMessage,
                description: GetMessage(successMessage, note));
        }
    }
}
