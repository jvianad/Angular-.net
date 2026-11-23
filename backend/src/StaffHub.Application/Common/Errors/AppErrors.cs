using FluentResults;

namespace StaffHub.Application.Common.Errors;

public static class AppErrors
{
    public const string NotFoundType = "NotFound";
    public const string ConflictType = "Conflict";
    public const string ValidationType = "Validation";
    public const string BadRequestType = "BadRequest";

    public static Error NotFound(string entityName, object key) =>
        new Error($"{entityName} with ID '{key}' was not found.")
            .WithMetadata("ErrorType", NotFoundType);

    public static Error Conflict(string message) =>
        new Error(message).WithMetadata("ErrorType", ConflictType);

    public static Error Validation(string message) =>
        new Error(message).WithMetadata("ErrorType", ValidationType);

    public static Error BadRequest(string message) =>
        new Error(message).WithMetadata("ErrorType", BadRequestType);
}