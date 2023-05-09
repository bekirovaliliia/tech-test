using System.Reflection;
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
				var response = new List<Delta>();
				foreach (string cur in request.Currencies)
				{
					PropertyInfo prop = request.FromValue.ConversionRates.GetType().GetProperty(cur);
					var from = Convert.ToDouble(prop.GetValue(request.FromValue.ConversionRates));
					var to = Convert.ToDouble(prop.GetValue(request.ToValue.ConversionRates));

					double delta = Math.Round(from - to, 3);
					var res = new Delta()
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
