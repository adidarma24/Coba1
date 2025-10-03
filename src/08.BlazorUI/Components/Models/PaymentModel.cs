namespace MyApp.BlazorUI.Components.Models
{
  public class PaymentModel
  {
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; }
  }

  public enum PaymentStatus
  {
    Active,
    Inactive
  }
}