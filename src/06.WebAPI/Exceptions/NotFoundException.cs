namespace MyApp.WebAPI.Exceptions
{
  /// <summary>
  /// Not Found Exception
  /// Purpose: Indicate requested resource doesn't exist
  /// HTTP Status: 404 Not Found
  /// 
  /// When to use:
  /// - Account not found
  /// - Invoice not found
  /// - User not found
  /// 
  /// Example:
  /// throw new NotFoundException($"Account {accountNumber} not found");
  /// </summary>
  public class NotFoundException : BaseApiException
  {
    public NotFoundException(string message, object? details = null)
        : base(404, "NOT_FOUND", message, details)
    {
    }

    public NotFoundException(string message, Exception innerException, object? details = null)
        : base(404, "NOT_FOUND", message, innerException, details)
    {
    }
  }
}