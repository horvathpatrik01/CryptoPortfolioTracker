namespace Server.Repositories.Contracts
{
    public interface IAssetRepository
    {
        Task<Asset?> GetAsset(int assetId);
        Task<Asset?> DeleteAsset(int assetId);
    }
}
