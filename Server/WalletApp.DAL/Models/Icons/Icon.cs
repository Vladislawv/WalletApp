using Microsoft.EntityFrameworkCore;

namespace WalletApp.DAL.Models.Icons;

[Index(nameof(Id))]
public class Icon
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Data { get; set; }
}