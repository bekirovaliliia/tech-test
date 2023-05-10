using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tech_test.Interfaces;
using Tech_test.Models;
using Tech_test.Models.ExchangeRateApi;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tech_test.Servises
{
    public class RatesService : IRatesService
    {
        public async Task<CalculateDeltaRequest> GetHistoryValue(CurrencyDeltaRequest request)
        {
            try {
				var calculateDeltaRequest = new CalculateDeltaRequest();
				calculateDeltaRequest.FromValue = await ImportFromApi(request.FromDate, request.Baseline);
				calculateDeltaRequest.ToValue = await ImportFromApi(request.ToDate, request.Baseline);

                return calculateDeltaRequest;
			}
			catch (Exception ex)
            {
				throw new Exception("Get history error:" + ex.Message);
			}
        }
		private async Task<CurrencyApiResponse> ImportFromApi(DateTime date, string baseCurrency)
        {
            try
            {
                string URLString = $"{GlobalVariables.apiUrl}/{GlobalVariables.apiKey}/history/{baseCurrency}/{date.Year}/{date.Month}/{date.Day}";
                using var webClient = new HttpClient() ;
                    var httpResponse = await webClient.GetAsync(URLString);
                    var json = await httpResponse.Content.ReadAsStringAsync();
					CurrencyApiResponse currencyResponse = JsonConvert.DeserializeObject<CurrencyApiResponse>(json);

				return currencyResponse;
            }
            catch (Exception ex)
			{
				throw new Exception("Import from API error:" + ex.Message);
			}
        }
    }
}
