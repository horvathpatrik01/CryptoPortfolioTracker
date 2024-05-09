using CryptoPortfolioTracker.ViewModels;
using CryptoPortfolioTracker.Services.Navigation;
using Moq;

namespace CryptoPortfolioTracker.UnitTests.ViewModels
{
    [TestClass]
    public class AvatarIconViewModelTests
    {
        [TestMethod]
        public void SaveEmoji()
        {
            INavigationService navigationService = new NavigationService();
            //var portfolioViewModelMock = new PortfolioViewModel();
            // Arrange
            var createPortfolioViewModelMock = new Mock<CreatePortfolioViewModel>(navigationService);
            var viewModel = new AvatarIconViewModel(createPortfolioViewModel: createPortfolioViewModelMock.Object);

            // Assert
            createPortfolioViewModelMock.VerifySet(vm => vm.NewPortfolioIcon = "🚀");
            createPortfolioViewModelMock.VerifySet(vm => vm.NewPortfolioIconColor = 0);

            // Act
            viewModel.NewPortfolioIcon = "🔥";
            viewModel.NewPortfolioIconColor = 1;
            viewModel.SaveEmojiCommand.Execute(null);

            // Assert
            createPortfolioViewModelMock.VerifySet(vm => vm.NewPortfolioIcon = "🔥");
            createPortfolioViewModelMock.VerifySet(vm => vm.NewPortfolioIconColor = 1);
        }

        [TestMethod]
        public void ChangeEmoji()
        {
            // Arrange
            var viewModel = new AvatarIconViewModel();

            // Assert
            Assert.AreEqual("🚀", viewModel.NewPortfolioIcon);

            // Act
            viewModel.ChangeEmojiCommand.Execute("❤️");

            // Assert
            Assert.AreEqual("❤️", viewModel.NewPortfolioIcon);
        }
    }
}