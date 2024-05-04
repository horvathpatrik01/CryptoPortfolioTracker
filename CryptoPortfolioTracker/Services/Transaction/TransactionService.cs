using CryptoPortfolioTracker.Services.Auth;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.Services.Transaction
{
    internal class TransactionService : ITransactionService
    {
        private readonly IAuthService _authService;
        private const string BaseApiEndpoint = "api/Transaction";

        public TransactionService(IAuthService authService)
        {
            this._authService = authService;
        }

        public async Task<TransactionDto?> AddTransaction(TransactionToAddDto transactionToAddDto)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient == null) return null;

                var response = await httpClient.PostAsJsonAsync<TransactionToAddDto>(BaseApiEndpoint, transactionToAddDto);

                if (response.IsSuccessStatusCode)
                {
                    TransactionDto? addedTransaction = await response.Content.ReadFromJsonAsync<TransactionDto>();
                    return addedTransaction;
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

        public async Task<TransactionDto?> EditTransaction(TransactionDto editedTransactionDto)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient == null) return null;

                var response = await httpClient.PostAsJsonAsync<TransactionDto>($"{BaseApiEndpoint}/EditTransaction", editedTransactionDto);

                if (response.IsSuccessStatusCode)
                {
                    TransactionDto? editedTransaction = await response.Content.ReadFromJsonAsync<TransactionDto>();
                    return editedTransaction;
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

        public async Task<TransactionDto?> RemoveTransaction(int transactionId)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient == null) return null;

                var response = await httpClient.DeleteAsync($"{BaseApiEndpoint}/{transactionId}");

                if (response.IsSuccessStatusCode)
                {
                    TransactionDto? deletedTransaction = await response.Content.ReadFromJsonAsync<TransactionDto>();
                    return deletedTransaction;
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