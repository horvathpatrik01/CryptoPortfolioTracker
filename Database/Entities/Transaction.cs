using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
	public class Transaction
	{
		public int Id { get; set; }
		public int PortfolioId { get; set; }
		public int Direction { get; set; }
		public string Coin { get; set; }
		public DateTime Time { get; set; }
		public double Price { get; set; }
		public double Fee { get; set; }
		public string Note { get; set; }
	}
}
