using CryptoPortfolioTracker.Models;
using CryptoPortfolioTracker.Services.Auth;
using Newtonsoft.Json;
using RestSharp;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace CryptoPortfolioTracker.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly IAuthService _authService;
        private const string BaseApiEndpoint = "api/Transaction";
        private const string BaseCmcEndpoint = "https://pro-api.coinmarketcap.com";
        private const string CmcEndpoint = "/v2/cryptocurrency/info";
        private const string CmcPriceEndpoint = "/v2/cryptocurrency/quotes/latest";
        private const string CmcKey = "b5465e01-dbce-4e1d-9446-de90a4d5333f";
        private const string CmcCoinsEndpoint = "/v1/cryptocurrency/listings/latest";

        public TransactionService(IAuthService authService)
        {
            this._authService = authService;
        }

        public async Task<AssetDto?> AddTransaction(TransactionToAddDto transactionToAddDto, int selectedCoinId)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient == null) return null;

                if (transactionToAddDto == null) return null;
                else if (transactionToAddDto.IconUrl == null)
                {
                    transactionToAddDto.IconUrl = await GetIconForCoin(selectedCoinId);
                }

                var response = await httpClient.PostAsJsonAsync<TransactionToAddDto>(BaseApiEndpoint, transactionToAddDto);

                if (response.IsSuccessStatusCode)
                {
                    AssetDto? assetWithaddedTransaction = await response.Content.ReadFromJsonAsync<AssetDto>();
                    return assetWithaddedTransaction;
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

        public async Task<AssetDto?> EditTransaction(TransactionDto editedTransactionDto)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient == null) return null;

                var response = await httpClient.PostAsJsonAsync<TransactionDto>($"{BaseApiEndpoint}/EditTransaction", editedTransactionDto);

                if (response.IsSuccessStatusCode)
                {
                    AssetDto? assetWithEditedTransaction = await response.Content.ReadFromJsonAsync<AssetDto>();
                    return assetWithEditedTransaction;
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

        public async Task<TransactionDto?> GetTransaction(int transactionId)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient == null) return null;

                var response = await httpClient.GetAsync($"{BaseApiEndpoint}/{transactionId}");

                if (response.IsSuccessStatusCode)
                {
                    TransactionDto? transaction = await response.Content.ReadFromJsonAsync<TransactionDto>();
                    return transaction;
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

        public async Task<IEnumerable<TransactionDto>?> GetTransactionsForAsset(int assetId)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient == null) return null;

                var response = await httpClient.GetAsync($"{BaseApiEndpoint}/GetTransactionsForAsset/{assetId}");

                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<TransactionDto>? transactions = await response.Content.ReadFromJsonAsync<IEnumerable<TransactionDto>>();
                    return transactions;
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

        public async Task<AssetDto?> RemoveTransaction(int transactionId)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient == null) return null;

                var response = await httpClient.DeleteAsync($"{BaseApiEndpoint}/{transactionId}");

                if (response.IsSuccessStatusCode)
                {
                    AssetDto? assetWithDeletedTransaction = await response.Content.ReadFromJsonAsync<AssetDto>();
                    if (assetWithDeletedTransaction is null)
                    {
                        AssetDto dummyDto = new()
                        {
                            Id = 0
                        };

                        return dummyDto;
                    }
                    return assetWithDeletedTransaction;
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

        public async Task<AssetDto?> DeleteAsset(int assetId)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient == null) return null;

                var response = await httpClient.DeleteAsync($"{BaseApiEndpoint}/DeleteAsset/{assetId}");

                if (response.IsSuccessStatusCode)
                {
                    AssetDto? deletedAsset = await response.Content.ReadFromJsonAsync<AssetDto>();
                    return deletedAsset;
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

        public async Task<string?> GetIconForCoin(int id)
        {
            try
            {
                var URL = new UriBuilder(BaseCmcEndpoint + CmcEndpoint);

                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString["id"] = id.ToString();

                URL.Query = queryString.ToString();

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", CmcKey);
                httpClient.DefaultRequestHeaders.Add("Accepts", "application/json");
                var response = await httpClient.GetAsync(URL.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    using JsonDocument document = JsonDocument.Parse(result);

                    JsonElement dataElement = document.RootElement.GetProperty("data");
                    string? logo = string.Empty;

                    logo = dataElement.GetProperty($"{id}").GetProperty("logo").GetString();

                    return logo;
                }

                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Coin>?> GetSupportedCoins()
        {
            try
            {
                UriBuilder URL = new(BaseCmcEndpoint + CmcCoinsEndpoint);

                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString["limit"] = "200";
                URL.Query = queryString.ToString();

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", CmcKey);
                httpClient.DefaultRequestHeaders.Add("Accepts", "application/json");
                HttpResponseMessage response = await httpClient.GetAsync(URL.ToString());
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                    List<Coin>? coins = JsonConvert.DeserializeObject<RootCoinListings>(result)?.Data;
                    return coins;
                }

                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Coin>?> GetCoinPrices(List<int> ids)
        {
            try
            {
                UriBuilder URL = new(BaseCmcEndpoint + CmcPriceEndpoint);

                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString["id"] = "2,";
                ids.ForEach(id =>
                {
                    if (id != -1)
                    {
                        queryString["id"] += id.ToString() + ',';
                    }
                });
                queryString["id"] = queryString["id"]!.Remove(queryString["id"]!.Length - 1);
                URL.Query = queryString.ToString();

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", CmcKey);
                httpClient.DefaultRequestHeaders.Add("Accepts", "application/json");
                HttpResponseMessage response = await httpClient.GetAsync(URL.ToString());
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                    List<Coin>? coins = JsonConvert.DeserializeObject<RootCoinQuotes>(result)?.Data?.Values.ToList();
                    return coins;
                }

                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}