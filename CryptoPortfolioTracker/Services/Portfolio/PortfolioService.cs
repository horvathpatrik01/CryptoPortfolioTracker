using CryptoPortfolioTracker.Services.Auth;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.Services.Portfolio
{
    internal class PortfolioService : IPortfolioSevice
    {
        private readonly IAuthService _authService;

        public PortfolioService(IAuthService authService)
        {
            this._authService = authService;
        }

        public async Task<PortfolioDto?> AddPortfolio(PortfolioToAddDto portfolioToAddDto)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient is null) return null;

                var response = await httpClient.PostAsJsonAsync<PortfolioToAddDto>("api/Portfolio", portfolioToAddDto);

                if (response.IsSuccessStatusCode)
                {
                    PortfolioDto? newPortfolio = await response.Content.ReadFromJsonAsync<PortfolioDto>();
                    return newPortfolio;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<PortfolioDto?> ChangePortfolioName(int portfolioId, string newPortfolioName)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient is null) return null;

                var response = await httpClient.PatchAsync($"api/Portfolio/{portfolioId}/{newPortfolioName}", null);

                if (response.IsSuccessStatusCode)
                {
                    PortfolioDto? editedPortfolio = await response.Content.ReadFromJsonAsync<PortfolioDto>();
                    return editedPortfolio;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<PortfolioDto?> GetPortfolio(int portfolioId)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient is null) return null;

                var response = await httpClient.GetAsync($"api/Portfolio/{portfolioId}");

                if (response.IsSuccessStatusCode)
                {
                    PortfolioDto? portfolio = await response.Content.ReadFromJsonAsync<PortfolioDto>();
                    return portfolio;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<PortfolioDto>?> GetPortfolios()
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient is null) return null;

                var response = await httpClient.GetAsync($"api/Portfolio");

                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<PortfolioDto>? portfolios = await response.Content.ReadFromJsonAsync<IEnumerable<PortfolioDto>>();
                    return portfolios;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<PortfolioDto?> RemovePortfolio(int portfolioId)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient is null) return null;

                var response = await httpClient.DeleteAsync($"api/Portfolio/{portfolioId}");

                if (response.IsSuccessStatusCode)
                {
                    PortfolioDto? deletedPortfolio = await response.Content.ReadFromJsonAsync<PortfolioDto>();
                    return deletedPortfolio;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}