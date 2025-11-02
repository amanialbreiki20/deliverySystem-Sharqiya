using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace deliverySystem_Sharqiya.Helpers
{
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }

    public static class ErrorExtractor
    {
        public static object GetErrors(ModelStateDictionary modelState)
        {
            return new
            {
                Error = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList()
            };
        }
    }
}
