using System.ComponentModel.DataAnnotations.Schema;

namespace Shared
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public decimal Price { get; set; }
        public decimal Fee { get; set; }
        public string? Note { get; set; }
        public decimal Amount { get; set; }

        public int AssetId { get; set; }

        public TransactionType TransactionType { get; set; }

    }

    public enum TransactionType
    {
        Buy,
        Sell,
        TransferIn,
        TransferOut
    }
}
