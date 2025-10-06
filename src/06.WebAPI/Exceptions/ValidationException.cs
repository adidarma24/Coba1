namespace MyApp.WebAPI.Exceptions
{
  /// <summary>
  /// Validation Exception
  /// Purpose: Indicate input validation failures
  /// HTTP Status: 400 Bad Request
  /// 
  /// When to use:
  /// - Model validation failures
  /// - Invalid format (email, phone, etc)
  /// - Required fields missing
  /// - Value out of range
  /// 
  /// Example with field-specific errors:
  /// var errors = new Dictionary<string, string[]>
  /// {
  ///     ["Amount"] = new[] { "Amount must be greater than 0" },
  ///     ["AccountNumber"] = new[] { "Account number is required" }
  /// };
  /// throw new ValidationException(errors);
  /// </summary>
  public class ValidationException : BaseApiException
  {
    public ValidationException(string message, object? details = null)
        : base(400, "VALIDATION_ERROR", message, details)
    {
    }

    /// <summary>
    /// Constructor for field-specific validation errors
    /// </summary>
    public ValidationException(Dictionary<string, string[]> errors)
        : base(400, "VALIDATION_ERROR", "One or more validation errors occurred.", errors)
    {
    }
  }
}