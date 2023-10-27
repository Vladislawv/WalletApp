using WalletApp.Domain.Aggregates.TransactionAggregate;

namespace WalletApp.Api.Controllers.Transaction.Dto;

public class TransactionDto
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public Guid IconId { get; set; }
    public TransactionType TransactionType { get; set; }
    public string Total { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Date { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsPending { get; set; }
}