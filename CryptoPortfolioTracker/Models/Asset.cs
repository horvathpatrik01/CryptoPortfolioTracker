using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.Models
{
    public class Asset : INotifyPropertyChanged
    {
        private List<TransactionDto> _transactions = null!;
        private decimal _avrgBuyPrice = 0m;
        private decimal _price = 0m;
        private double _change_1h = 0f;
        private double _change_24h = 0f;
        private double _change_7d = 0f;
        private decimal _amount = 0m;
        private int _id { get; set; }
        private string _symbol { get; set; } = null!;
        private string _name { get; set; } = null!;
        private string? _iconUrl { get; set; }

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string Symbol
        {
            get => _symbol;
            set
            {
                if (_symbol != value)
                {
                    _symbol = value;
                    OnPropertyChanged(nameof(Symbol));
                }
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public double Percent_Change_1h
        {
            get => _change_1h;
            set
            {
                if (_change_1h != value)
                {
                    _change_1h = value;
                    OnPropertyChanged(nameof(Percent_Change_1h));
                }
            }
        }

        public double Percent_Change_24h
        {
            get => _change_24h;
            set
            {
                if (_change_24h != value)
                {
                    _change_24h = value;
                    OnPropertyChanged(nameof(Percent_Change_24h));
                }
            }
        }

        public double Percent_Change_7d
        {
            get => _change_7d;
            set
            {
                if (_change_7d != value)
                {
                    _change_7d = value;

                    OnPropertyChanged(nameof(Percent_Change_7d));
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string? IconUrl
        {
            get => _iconUrl;
            set
            {
                if (_iconUrl != value)
                {
                    _iconUrl = value;
                    OnPropertyChanged(nameof(IconUrl));
                }
            }
        }

        public decimal AvrgBuyPrice
        {
            get => _avrgBuyPrice;
            set
            {
                if (_avrgBuyPrice != value)
                {
                    _avrgBuyPrice = value;
                    OnPropertyChanged(nameof(AvrgBuyPrice));
                }
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }

        public List<TransactionDto> Transactions
        {
            get => _transactions;
            set
            {
                if (_transactions != value)
                {
                    _transactions = value;
                    OnPropertyChanged(nameof(Transactions));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}