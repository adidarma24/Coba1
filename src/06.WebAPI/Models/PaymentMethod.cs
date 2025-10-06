namespace MyApp.WebAPI.Models
{
  public class PaymentMethod : BaseModel
  {
    public int PaymentMethodId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; } = PaymentStatus.Active;
  }

  public enum PaymentStatus
  {
    Active,
    Inactive
  }
}
