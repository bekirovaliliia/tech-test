using Tech_test.Models.ExchangeRateApi;

namespace Tech_test.Models
{
	public class CalculateDeltaRequest
	{
		public List<string> Currencies {  get; set; }
		public CurrencyApiResponse FromValue { get; set; }
		public CurrencyApiResponse ToValue { get; set; }
	}
}
