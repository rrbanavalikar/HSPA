using System.Text.Json;

namespace WebAPI.Errors
{
    public class ApiErrors
    {
      public ApiErrors(){}
        public ApiErrors(int errorCode, string errorMessage, string errorDetails = null)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
            this.ErrorDetails = errorDetails;

        }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public string ErrorDetails { get; set; }

    public override string ToString()
    {
      var options = new JsonSerializerOptions()
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
      };
      return JsonSerializer.Serialize(this,options);
    }

    }
}
