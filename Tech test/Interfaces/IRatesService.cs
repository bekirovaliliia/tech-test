using Tech_test.Models;
using Tech_test.Models.ExchangeRateApi;

namespace Tech_test.Interfaces
{
	public interface IRatesService
	{
		public Task<CalculateDeltaRequest> GetHistoryValue(CurrencyDeltaRequest request);
		public Task<CurrencyApiResponse> ImportFromApi(DateTime date, string baseCurrency);
	}
}
