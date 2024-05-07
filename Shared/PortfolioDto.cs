using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shared
{
    public class PortfolioDto : INotifyPropertyChanged
    {
        private string _name = null!;

        private int _iconcolor;
        private string _icon = null!;
        public int Id { get; set; }

        public string Icon
        {
            get => _icon;
            set
            {
                if (_icon != value)
                {
                    _icon = value;
                    OnPropertyChanged(nameof(Icon));
                }
            }
        }

        public int IconColor
        {
            get => _iconcolor;
            set
            {
                if (_iconcolor != value)
                {
                    _iconcolor = value;
                    OnPropertyChanged(nameof(IconColor));
                }
            }
        }

        [MaxLength(32)]
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

        public PortfolioType PortfolioType { get; set; }
        public List<AssetDto>? Assets { get; set; }
        public AddressDto? Address { get; set; }
        public ApiKeyDto? ApiKey { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum PortfolioType
    {
        Default,
        CexAccount,
        Wallet
    }
}