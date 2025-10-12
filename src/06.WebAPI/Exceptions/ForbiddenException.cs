namespace MyApp.WebAPI.Exceptions
{
  /// <summary>
  /// Forbidden Exception
  /// Purpose: Indicates that the user is authenticated but not authorized
  /// to perform the requested operation.
  /// 
  /// HTTP Status: 403 Forbidden
  /// 
  /// When to use:
  /// - User tries to access another user's data
  /// - Non-admin tries to perform admin-only actions
  /// - Access control or role-based restriction violations
  /// 
  /// Example:
  /// throw new ForbiddenException("You are not allowed to access this resource.");
  /// </summary>
  public class ForbiddenException : BaseApiException
  {
    /// <summary>
    /// Creates a new ForbiddenException with a message and optional details.
    /// </summary>
    public ForbiddenException(string message, object? details = null)
        : base(403, "FORBIDDEN", message, details)
    {
    }

    /// <summary>
    /// Creates a new ForbiddenException wrapping another exception.
    /// Useful for wrapping lower-level authorization exceptions.
    /// </summary>
    public ForbiddenException(string message, Exception innerException, object? details = null)
        : base(403, "FORBIDDEN", message, innerException, details)
    {
    }
  }
}
