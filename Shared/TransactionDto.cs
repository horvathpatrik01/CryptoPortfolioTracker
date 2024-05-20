using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Shared
{
    public class TransactionDto : INotifyPropertyChanged
    {
        private DateTime _time;
        private decimal _price;
        private decimal _amount;
        private decimal _fee;
        private string? _note;
        private TransactionType _transactionType;
        public int Id { get; set; }

        public DateTime Time
        {
            get => _time;
            set
            {
                if (_time != value)
                {
                    _time = value;
                    OnPropertyChanged(nameof(Time));
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

        public decimal Fee
        {
            get => _fee;
            set
            {
                if (_fee != value)
                {
                    _fee = value;
                    OnPropertyChanged(nameof(Fee));
                }
            }
        }

        public string? Note
        {
            get => _note;
            set
            {
                if (_note != value)
                {
                    _note = value;
                    OnPropertyChanged(nameof(Note));
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

        public int AssetId { get; set; }

        public TransactionType TransactionType
        {
            get => _transactionType;
            set
            {
                if (_transactionType != value)
                {
                    _transactionType = value;
                    OnPropertyChanged(nameof(TransactionType));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum TransactionType
    {
        Buy,
        Sell,
        TransferIn,
        TransferOut
    }
}