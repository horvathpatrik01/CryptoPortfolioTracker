namespace Server.Extensions
{
    public static class DtoConverters
    {
        public static UserInfoDto ConvertToDto(this User user)
        {
            return new UserInfoDto
            {
                CreationTime = user.CreatedTimeStamp,
                Email = user.Email!,
                UserName = user.UserName!
            };
        }

        public static IEnumerable<PortfolioDto> ConvertToDto(this IEnumerable<Portfolio> portfolios)
        {
            return portfolios.Select(p => p.ConvertToDto());
        }

        public static PortfolioDto ConvertToDto(this Portfolio portfolio)
        {
            return new PortfolioDto
            {
                Id = portfolio.Id,
                Name = portfolio.Name,
                Icon = portfolio.Icon,
                PortfolioType = portfolio.PortfolioType,
                Assets = portfolio.Assets?.ConvertToDto().ToList(),
                Address = portfolio.Address?.ConvertToDto(),
                ApiKey = portfolio.ApiKey?.ConvertToDto()
            };
        }
        public static IEnumerable<AssetDto> ConvertToDto(this IEnumerable<Asset> assets)
        {
            return assets.Select(a => a.ConvertToDto());
        }

        public static AssetDto ConvertToDto(this Asset asset)
        {
            return new AssetDto
            {
                Id = asset.Id,
                Name = asset.Name,
                Symbol = asset.Symbol,
                IconUrl = asset.IconUrl,
                Amount = asset.Amount,
                AvrgBuyPrice = asset.AvrgBuyPrice,
                Transactions = asset.Transactions?.ConvertToDto().ToList()
            };
        }

        public static AddressDto ConvertToDto(this Address address)
        {
            return new AddressDto
            {
                WalletAddress= address.WalletAddress
            };
        }

        public static ApiKeyDto ConvertToDto(this ApiKey apikeys)
        {
            return new ApiKeyDto
            {
                ApiKey = apikeys.PublicKey,
                ApiSecret = apikeys.PrivateKey,
                CexIdentifier = apikeys.CexIdentifier
            };
        }

        public static IEnumerable<TransactionDto> ConvertToDto(this IEnumerable<Transaction> transactions)
        {
            return transactions.Select(t => t.ConvertToDto());
        }

        public static TransactionDto ConvertToDto(this Transaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                AssetId = transaction.AssetId,
                Amount = transaction.Amount,
                Fee = transaction.Fee,
                Note = transaction.Note,
                Price = transaction.Price,
                Time = transaction.Time,
                TransactionType = transaction.TransactionType
            };
        }

    }
}
