namespace WalletApp.Api.Dto;

public class ErrorDto
{
    public string ErrorMessage { get; set; }
    public string StackTrace { get; set; }
    public string SessionId { get; set; }
}