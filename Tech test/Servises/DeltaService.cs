using Tech_test.Interfaces;
using Tech_test.Models;
using Tech_test.Models.ExchangeRateApi;

namespace Tech_test.Servises
{
    public class DeltaService : IDeltaService
	{
		public List<Delta> CalcucateDeltas(CalculateDeltaRequest request)
		{
			try
			{
				List<Delta> response = new List<Delta>();
				foreach (string cur in request.Currencies)
				{
					var prop = request.FromValue.conversion_rates.GetType().GetProperty(cur);
					var from = Convert.ToDouble(prop.GetValue(request.FromValue.conversion_rates, null));
					var to = Convert.ToDouble(prop.GetValue(request.ToValue.conversion_rates, null));

					double delta = Math.Round(from - to, 3);
					Delta res = new Delta()
					{
						Currency = cur,
						Value = delta
					};
					response.Add(res);
				}

				return response;
			}
			catch
			{
				return new List<Delta>();
			}
		}
	}
}
