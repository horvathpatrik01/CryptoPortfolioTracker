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
        public async Task<ActionResult<TransactionDto>> EditTransaction(TransactionDto transactionToEditDto)
        {
            try
            {
                var portfolio = await this.transactionRepository.EditTransaction(transactionToEditDto);

                if (portfolio == null)
                {
                    return NotFound();
                }

                var portfolioDto = portfolio.ConvertToDto();

                return Ok(portfolioDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }

        [HttpPost()]
        public async Task<ActionResult<TransactionDto>> AddNewTransaction([FromBody] TransactionToAddDto transactionToAddDto)
        {
            try
            {
                var newTransaction = await this.transactionRepository.AddTransaction(transactionToAddDto);

                if (newTransaction == null)
                    return BadRequest("Transaction couldn't be added to the database!");

                var newTransactionDto = newTransaction.ConvertToDto();

                return Ok(newTransactionDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }

        [HttpDelete("{transactionId}")]
        public async Task<ActionResult<TransactionDto>> DeleteTransaction(int transactionId)
        {
            try
            {
                var deletedTransaction = await this.transactionRepository.DeleteTransaction(transactionId);

                if (deletedTransaction == null)
                {
                    return NotFound();
                }

                var deletedTransactionDto = deletedTransaction.ConvertToDto();

                return Ok(deletedTransactionDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }
    }
}