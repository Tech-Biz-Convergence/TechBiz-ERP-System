namespace AspnetCoreMvcFull.Models
{
  public class ResponseApiModel
  {
    public bool Status { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public object? Data { get; set; }
  }

}
