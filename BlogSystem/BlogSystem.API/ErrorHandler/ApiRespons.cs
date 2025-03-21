
using System.Text.Json.Serialization;

namespace BlogSystem.API.ErrorHandler
{
	public class ApiRespons
	{
		public int StatusCode { get; }
		public string Message { get; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
		public object? Data { get; }

		public ApiRespons(int statusCode, string? message = null, object? data = null)
		{
			StatusCode = statusCode;
			Message = message ?? GetDefaultMessageForStatusCode(statusCode);
			Data = data;
		}

		private static string GetDefaultMessageForStatusCode(int statusCode) => statusCode switch
		{
			400 => "Bad Request",
			401 => "Unauthorized",
			403 => "Forbidden",
			404 => "Resource Not Found",
			500 => "Internal Server Error",
			_ => "An error occurred"
		};
	}
}
