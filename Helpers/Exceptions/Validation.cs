using deliverySystem_Sharqiya.Helpers.MessageHandler;

namespace deliverySystem_Sharqiya.Helpers.Exceptions
{
    public class Validation : Exception
    {
        public ErrorMessage ErrorCode { get; set; }
        public string Args { get; set; }
        public bool AuditEnabled { get; set; }

        public Validation(ErrorMessage errorMessage, string args, bool audit)
        : base(GenerateMessage(errorMessage, args))
        {
            ErrorCode = errorMessage;
            Args = args;
            AuditEnabled = audit;
        }

        public static void ThrowErrorIf(bool? predicate, ErrorMessage errorMessage, string args = null, bool audit = true)
        {
            if (predicate == true)
            {
                throw new Validation(errorMessage, args, audit);
            }
        }

        public static void ThrowError(ErrorMessage errorMessage, string args = null, bool audit = true)
        {
            throw new Validation(errorMessage, args, audit);
        }

        private static string GenerateMessage(ErrorMessage errorMessage, string args)
        {
            var messageHandler = new deliverySystem_Sharqiya.Helpers.MessageHandler.MessageHandler();
            return $"{messageHandler.GetMessage(errorMessage, args)}";
        }
    }
}
