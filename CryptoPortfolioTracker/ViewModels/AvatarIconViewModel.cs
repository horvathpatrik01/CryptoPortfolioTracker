using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoPortfolioTracker.Services.Navigation;
using CryptoPortfolioTracker.ViewModels.Base;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CryptoPortfolioTracker.ViewModels
{
    public partial class AvatarIconViewModel : ObservableObject
    {
        private readonly CreatePortfolioViewModel? createPortfolioViewModel;
        private readonly EditPortfolioViewModel? editPortfolioViewModel;

        [ObservableProperty]
        private string newPortfolioIcon = "";

        [ObservableProperty]
        private int newPortfolioIconColor = 0;

        public ObservableCollection<int> ColorIndexes { get; }

        public ObservableCollection<String> EmojiItemSource { get; }

        public AvatarIconViewModel(CreatePortfolioViewModel? createPortfolioViewModel = null, EditPortfolioViewModel? editPortfolioViewModel = null)
        {
            ColorIndexes = new ObservableCollection<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
            EmojiItemSource = new ObservableCollection<string> { "🚀", "🔥", "❤️", "👻", "⚡", "🔑", "⚒️", "🔶", "🔷", "💎", "💰", "🏦", "💵", "🔔", "🦄",
            "🦊", "🐶", "🐰", "🐯", "🐻", "🐮", "🍕", "🍔", "💊", "👑", "🌈", "🤖", "🌕" };

            NewPortfolioIcon = createPortfolioViewModel?.NewPortfolioIcon ?? editPortfolioViewModel?.NewPortfolioIcon ?? "🚀";
            NewPortfolioIconColor = createPortfolioViewModel?.NewPortfolioIconColor ?? editPortfolioViewModel?.NewPortfolioIconColor ?? 0;
            this.createPortfolioViewModel = createPortfolioViewModel;
            this.editPortfolioViewModel = editPortfolioViewModel;
        }

        [RelayCommand]
        private void SaveEmoji()
        {
            if (createPortfolioViewModel != null)
            {
                createPortfolioViewModel.NewPortfolioIcon = NewPortfolioIcon;
                createPortfolioViewModel.NewPortfolioIconColor = NewPortfolioIconColor;
            }
            else if (editPortfolioViewModel != null)
            {
                editPortfolioViewModel.NewPortfolioIcon = NewPortfolioIcon;
                editPortfolioViewModel.NewPortfolioIconColor = NewPortfolioIconColor;
            }
        }

        [RelayCommand]
        private void ChangeEmoji(string? emoji)
        {
            if (emoji is not null)
                NewPortfolioIcon = emoji;
        }
    }
}