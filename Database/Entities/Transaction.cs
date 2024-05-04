using Microsoft.EntityFrameworkCore;
using Shared;
using System.ComponentModel.DataAnnotations.Schema;


namespace Database.Entities
{
	public class Transaction
	{
		public int Id { get; set; }
		public DateTime Time { get; set; }
		[Precision(32,16)]
		public decimal Price { get; set; }
        [Precision(32, 16)]
        public decimal Fee { get; set; }
		public string? Note { get; set; }
        [Precision(32, 16)]
        public decimal Amount { get; set; }

		public TransactionType TransactionType { get; set; }

		[ForeignKey(nameof(AssetId))]
		public int AssetId { get; set; }

		// Navigation property
		public Asset Asset { get; set; } = null!;
    }

    
}
