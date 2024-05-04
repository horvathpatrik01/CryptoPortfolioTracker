using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared
{
    public class AssetDto : INotifyPropertyChanged
    {
        private List<TransactionDto> _transactions = null!;
        private decimal _avrgBuyPrice = 0m;
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