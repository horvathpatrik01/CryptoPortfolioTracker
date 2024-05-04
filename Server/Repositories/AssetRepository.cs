
using Database.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Server.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AppDbContext _appDbContext;

        public AssetRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task<Asset?> DeleteAsset(int assetId)
        {
            Asset? asset = await _appDbContext.Assets.FindAsync(assetId);
            if (asset == null)
            {
                return null;
            }
            var result=_appDbContext.Assets.Remove(asset).Entity;
            if (result != null)
            {
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Asset?> GetAsset(int assetId)
        {
            return await _appDbContext.Assets.FindAsync(assetId);
        }
    }
}
