using FluentValidation;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using BlogSystem.API.ErrorHandler;
using System.Text.Json.Serialization;

public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly IWebHostEnvironment _env;

	public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
	{
		_next = next;
		_env = env;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (ValidationException ex)
		{
			var response = new ApiErrorValidationResponse(ex.Errors.Select(e => e.ErrorMessage));
			await HandleExceptionAsync(context, HttpStatusCode.BadRequest, response);
		}
		catch (ArgumentException ex)
		{
			var response = new ApiExceptionResponse(400, "Bad Request", ex.Message);
			await HandleExceptionAsync(context, HttpStatusCode.BadRequest, response);
		}
		catch (UnauthorizedAccessException ex)
		{
			var statusCode = context.User.Identity?.IsAuthenticated == true ? HttpStatusCode.Forbidden : HttpStatusCode.Unauthorized;
			var response = new ApiExceptionResponse((int)statusCode, statusCode.ToString(), ex.Message);
			await HandleExceptionAsync(context, statusCode, response);
		}
		catch (KeyNotFoundException ex)
		{
			var response = new ApiExceptionResponse(404, "Not Found", ex.Message);
			await HandleExceptionAsync(context, HttpStatusCode.NotFound, response);
		}
		catch (Exception ex)
		{
			var response = new ApiExceptionResponse(500, "Internal Server Error", ex.Message);

			if (_env.IsDevelopment())
			{
				response.StackTrace = ex.StackTrace;
			}

			await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, response);
		}
	}

	private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, object response)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)statusCode;

		var jsonOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			WriteIndented = true,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // ✅ تجاهل القيم null لمنع مشاكل التسلسل
		};

		var json = JsonSerializer.Serialize(response, jsonOptions);
		await context.Response.WriteAsync(json);
	}
}
