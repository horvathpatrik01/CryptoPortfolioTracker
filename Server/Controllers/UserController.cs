using Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Security.Claims;

namespace Server.Controllers
{
    [Authorize]
    [ValidateAntiForgeryToken]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository userRepository;

        public UserController(ILogger<UserController> logger, 
                              IUserRepository userRepository,
                              IPortfolioRepository portfolioRepository)
        {
            _logger = logger;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<UserInfoDto>> GetUserInfo()
        {
            try
            {
                Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out Guid UserId);
                var userInfo = await this.userRepository.GetUser(UserId);

                if(userInfo == null)
                {
                    return NotFound();
                }

                var userInfoDto = userInfo.ConvertToDto();
                
                return Ok(userInfoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }

        [HttpPatch("{newUsername}")]
        public async Task<ActionResult<UserInfoDto>> ChangeUserName(string newUsername)
        {
            try
            {
                Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out Guid UserId);
                var userInfo = await this.userRepository.ChangeUserName(UserId, newUsername);

                if (userInfo == null)
                {
                    return NotFound();
                }

                var userInfoDto = userInfo.ConvertToDto();

                return Ok(userInfoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }

        [HttpPatch]
        [Route(nameof(ChangeEmailAddress)+"/{newEmailAddress}")]
        public async Task<ActionResult<UserInfoDto>> ChangeEmailAddress(string newEmailAddress)
        {
            try
            {
                Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out Guid UserId);
                var userInfo = await this.userRepository.ChangeEmailAddress(UserId, newEmailAddress);

                if (userInfo == null)
                {
                    return NotFound();
                }

                var userInfoDto = userInfo.ConvertToDto();

                return Ok(userInfoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<UserInfoDto>> DeleteAccount()
        {
            try
            {
                Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out Guid UserId);
                User? userInfo = await this.userRepository.DeleteUser(UserId);

                if (userInfo == null)
                {
                    return NotFound();
                }

                var userInfoDto = userInfo.ConvertToDto();

                return Ok(userInfoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("zsupsz");
            }
        }

    }
}
