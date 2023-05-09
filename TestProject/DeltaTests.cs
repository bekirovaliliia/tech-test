using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tech_test;
using Tech_test.Models;
using Tech_test.Models.ExchangeRateApi;
using Tech_test.Servises;

namespace TestProject
{
	[TestClass]
	public class DeltaTests
	{
		[TestMethod]
		public void CalculateDeltas_OneCurrency_ReturnsDelta()
		{
			CalculateDeltaRequest request = new CalculateDeltaRequest()
			{
				Currencies = new List<string>(){ "USD" },
				FromValue = new CurrencyApiResponse()
				{
					conversion_rates = new ConversionRate()
					{
						USD = 25.5
					}
				},
				ToValue = new CurrencyApiResponse()
				{
					conversion_rates = new ConversionRate()
					{
						USD = 26
					}
				}
			};

			DeltaService service = new DeltaService();

			List<Delta> actual = service.CalcucateDeltas(request);

			Assert.AreEqual(-0.5, actual[0].Value);
		}

		[TestMethod]
		public void CalculateDeltas_TwoCurrencies_ReturnsTwoDeltas()
		{
			CalculateDeltaRequest request = new CalculateDeltaRequest()
			{
				Currencies = new List<string>() { "USD" , "EUR"},
				FromValue = new CurrencyApiResponse()
				{
					conversion_rates = new ConversionRate()
					{
						USD = 25.5,
						EUR = 26
					}
				},
				ToValue = new CurrencyApiResponse()
				{
					conversion_rates = new ConversionRate()
					{
						USD = 26,
						EUR = 26.7
					}
				}
			};

			DeltaService service = new DeltaService();

			List<Delta> actual = service.CalcucateDeltas(request);

			Assert.AreEqual(request.Currencies.Count, actual.Count);
		}

		[TestMethod]
		public void CalculateDeltas_EmptyCurrencies_ReturnsEmptyDeltas()
		{
			CalculateDeltaRequest request = new CalculateDeltaRequest()
			{
				Currencies = new List<string>() {},
				FromValue = new CurrencyApiResponse()
				{
					conversion_rates = new ConversionRate()
					{
						USD = 25.5,
						EUR = 26
					}
				},
				ToValue = new CurrencyApiResponse()
				{
					conversion_rates = new ConversionRate()
					{
						USD = 26,
						EUR = 26.7
					}
				}
			};

			DeltaService service = new DeltaService();

			List<Delta> actual = service.CalcucateDeltas(request);

			Assert.AreEqual(request.Currencies.Count, actual.Count);
		}
	}
}