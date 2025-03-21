
using System.Text.Json.Serialization;

namespace BlogSystem.API.ErrorHandler
{
	public class ApiErrorValidationResponse : ApiRespons
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public IEnumerable<string> ValidationErrors { get; }

		public ApiErrorValidationResponse(IEnumerable<string> errors)
			: base(400, "Validation failed")
		{
			ValidationErrors = errors;
		}
	}
}
