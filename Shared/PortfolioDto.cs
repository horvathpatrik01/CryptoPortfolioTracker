using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shared
{
    public class PortfolioDto : INotifyPropertyChanged
    {
        private string _name;
        public int Id { get; set; }
        public string Icon { get; set; }

        [MaxLength(20)]
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
        // Other properties...

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