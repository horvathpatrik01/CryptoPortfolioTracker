using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> logger;
        private readonly ITransactionRepository transactionRepository;

        public TransactionController(ILogger<TransactionController> logger,
                                     ITransactionRepository transactionRepository)
        {
            this.logger = logger;
            this.transactionRepository = transactionRepository;
        }

        [HttpGet]
        [Route(nameof(GetTransactionsForAsset) + "/{assetId:int}")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactionsForAsset(int assetId)
        {
            try
            {
                var transactions = await this.transactionRepository.GetTransactions(assetId);

                if (transactions == null)
                {
                    return NotFound();
                }

                var transactionsDto = transactions.ConvertToDto();

                return Ok(transactionsDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }

        [HttpGet("{transactionId:int}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int transactionId)
        {
            try
            {
                var transaction = await this.transactionRepository.GetTransaction(transactionId);

                if (transaction == null)
                {
                    return NotFound();
                }

                var transactionDto = transaction.ConvertToDto();

                return Ok(transactionDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }

        [HttpPost]
        [Route(nameof(EditTransaction))]
        public async Task<ActionResult<AssetDto>> EditTransaction(TransactionDto transactionToEditDto)
        {
            try
            {
                var editedAsset = await this.transactionRepository.EditTransaction(transactionToEditDto);

                if (editedAsset == null)
                {
                    return NotFound();
                }

                var editedAssetDto = editedAsset.ConvertToDto();

                return Ok(editedAssetDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }

        [HttpPost()]
        public async Task<ActionResult<AssetDto>> AddNewTransaction([FromBody] TransactionToAddDto transactionToAddDto)
        {
            try
            {
                var asset = await this.transactionRepository.AddTransaction(transactionToAddDto);

                if (asset == null)
                    return BadRequest("Transaction couldn't be added to the database!");

                var assetDto = asset.ConvertToDto();

                return Ok(assetDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }

        [HttpDelete("{transactionId}")]
        public async Task<ActionResult<AssetDto>> DeleteTransaction(int transactionId)
        {
            try
            {
                var transaction = await this.transactionRepository.GetTransaction(transactionId);
                if (transaction == null)
                {
                    return NotFound();
                }
                var assetBeforeDelete = await this.transactionRepository.GetAsset(transaction.AssetId);

                if (assetBeforeDelete == null)
                {
                    return NotFound();
                }
                var asset = await this.transactionRepository.DeleteTransaction(transactionId);

                var assetDto = asset?.ConvertToDto();

                return Ok(assetDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }
    }
}