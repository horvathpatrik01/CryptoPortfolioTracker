using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared
{
    public class AssetDto : INotifyPropertyChanged
    {
        private List<TransactionDto> _transactions = null!;
        private decimal _avrgBuyPrice = 0m;
        private decimal _amount = 0m;
        public int Id { get; set; }
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? IconUrl { get; set; }

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