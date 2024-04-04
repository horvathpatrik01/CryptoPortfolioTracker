using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

		public TransactionType TransactionType { get; set; }

		[ForeignKey(nameof(AssetId))]
		public int AssetId { get; set; }

		// Navigation property
		public Asset Asset { get; set; } = null!;
    }

	public enum TransactionType
	{
		Buy,
		Sell,
		TransferIn,
		TransferOut
	}
}
