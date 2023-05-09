using Tech_test.Models;

namespace Tech_test.Interfaces
{
	public interface IRatesService
	{
		public Task<CalculateDeltaRequest> GetHistoryValue(CurrencyDeltaRequest request);
	}
}
