using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.Models
{
    public class RootCoinListings
    {
        [JsonProperty("data")]
        public List<Coin>? Data { get; set; }
    }

    public class RootCoinQuotes
    {
        public Dictionary<string, Coin>? Data { get; set; }
    }

    public class Coin
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public DateTime? Last_Updated { get; set; }
        public Dictionary<string, SymbolQuote> Quote { get; set; } = null!;
    }

    public class SymbolQuote
    {
        public decimal Price { get; set; }
        public long Volume_24h { get; set; }
        public double Volume_Change_24h { get; set; }
        public double Percent_Change_1h { get; set; }
        public double Percent_Change_24h { get; set; }
        public double Percent_Change_7d { get; set; }
        public double Market_Cap { get; set; }
        public double Market_Cap_Dominance { get; set; }
        public double Fully_Diluted_Market_Cap { get; set; }
        public DateTime Last_Updated { get; set; }
    }
}