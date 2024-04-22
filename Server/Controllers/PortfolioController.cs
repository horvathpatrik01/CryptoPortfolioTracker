using Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IPortfolioRepository portfolioRepository;

        public PortfolioController(ILogger<UserController> logger,
                                   IPortfolioRepository portfolioRepository)
        {
            this.logger = logger;
            this.portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioDto>>> GetPortfolios()
        {
            try
            {
                Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out Guid UserId);
                var portfolios = await this.portfolioRepository.GetPortfolios(UserId);

                if (portfolios == null)
                {
                    return NotFound();
                }

                var portfoliosDto = portfolios.ConvertToDto();

                return Ok(portfoliosDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }

        [HttpGet("{portfolioId}")]
        public async Task<ActionResult<PortfolioDto>> GetPortfolio(int portfolioId)
        {
            try
            {
                var portfolio = await this.portfolioRepository.GetPortfolio(portfolioId);

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

        [HttpPatch]
        [Route("{portfolioId}/{newPortfolioName}")]
        public async Task<ActionResult<PortfolioDto>> ChangePortfolioName(int portfolioId,string newPortfolioName)
        {
            try
            {
                var portfolio = await this.portfolioRepository.EditPortFolioName(portfolioId,newPortfolioName);

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

        [HttpPost]
        public async Task<ActionResult<PortfolioDto>> AddNewPortfolio([FromBody]PortfolioToAddDto portfolioDto)
        {
            try
            {
                Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out Guid UserId);
                var newPortfolio = await this.portfolioRepository.AddPortfolio(UserId, portfolioDto);

                if(newPortfolio == null)
                    return BadRequest("Portfolio couldn't be added to the database!");

                var newportfolioDto = newPortfolio.ConvertToDto();

                return Ok(newportfolioDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }

        [HttpDelete("{portfolioId}")]
        public async Task<ActionResult<PortfolioDto>> DeletePortfolio(int portfolioId)
        {
            try
            {
                Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out Guid UserId);
                var deletedPortfolio = await this.portfolioRepository.DeletePortfolio(UserId,portfolioId);

                if (deletedPortfolio == null)
                {
                    return NotFound();
                }

                var deletedPortfolioDto = deletedPortfolio.ConvertToDto();

                return Ok(deletedPortfolioDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }
    }
}
