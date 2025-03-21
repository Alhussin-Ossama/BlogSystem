using System.Text.Json.Serialization;

namespace BlogSystem.API.ErrorHandler
{
	public class ApiExceptionResponse : ApiRespons
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? Details { get; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? StackTrace { get; set; }

		public ApiExceptionResponse(int statusCode, string? message = null, string? details = null)
			: base(statusCode, message)
		{
			Details = details;
		}
	}
}
