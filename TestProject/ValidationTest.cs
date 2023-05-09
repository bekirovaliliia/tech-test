using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech_test.Models;
using Tech_test.Servises;

namespace TestProject
{

	[TestClass]
	public class ValidationTest
	{
		[TestMethod]
		public void Validate_ValidData_ReturnsEmptyList()
		{
			CurrencyDeltaRequest request = new CurrencyDeltaRequest()
			{
				Baseline = "UAH",
				Currencies = new List<string>() { "USD", "EUR" },
				FromDate = DateTime.Today.AddDays(-30),
				ToDate = DateTime.Today
			};
			var validationService = new ValidationService();


			var actual = validationService.Validate(request);

			Assert.AreEqual(0, actual.Count);
		}

		[TestMethod]
		public void Validate_NotValidData_ReturnsTwoErrors()
		{
			CurrencyDeltaRequest request = new CurrencyDeltaRequest()
			{
				Baseline = "ABC",
				Currencies = new List<string>() { "USD", "EUR" },
				FromDate = DateTime.Today,
				ToDate = DateTime.Today
			};
			var validationService = new ValidationService();


			var actual = validationService.Validate(request);

			Assert.AreEqual(2, actual.Count);
		}

		//---------- Currencies ---------
		[TestMethod]
		public void ValidateCurrencies_AllUniqueAndExist_ReturnsNull()
		{
			CurrencyDeltaRequest request = new CurrencyDeltaRequest()
			{
				Baseline = "UAH",
				Currencies = new List<string>() { "USD", "EUR" },
				FromDate = DateTime.Today.AddDays(-30),
				ToDate = DateTime.Today
			};
			var validationService = new ValidationService();


			var actual = validationService.ValidateCurrencies(request);

			Assert.IsNull(actual);
		}

		[TestMethod]
		public void ValidateCurrencies_BaselineCurrencyNotExists_ReturnsError()
		{
			CurrencyDeltaRequest request = new CurrencyDeltaRequest()
			{
				Baseline = "ABC",
				Currencies = new List<string>() { "USD", "EUR" },
				FromDate = DateTime.Today.AddDays(-30),
				ToDate = DateTime.Today
			};
			var validationService = new ValidationService();


			var actual = validationService.ValidateCurrencies(request);

			Assert.AreEqual("invalid-currency-baseline", actual.ErrorCode);
		}

		[TestMethod]
		public void ValidateCurrencies_CurrencyNotExists_ReturnsError()
		{
			CurrencyDeltaRequest request = new CurrencyDeltaRequest()
			{
				Baseline = "UAH",
				Currencies = new List<string>() { "ABC", "EUR" },
				FromDate = DateTime.Today.AddDays(-30),
				ToDate = DateTime.Today
			};
			var validationService = new ValidationService();


			var actual = validationService.ValidateCurrencies(request);

			Assert.AreEqual("invalid-currency", actual.ErrorCode);
		}

		[TestMethod]
		public void ValidateCurrencies_CurrenciesNotUnique_ReturnsError()
		{
			CurrencyDeltaRequest request = new CurrencyDeltaRequest()
			{
				Baseline = "UAH",
				Currencies = new List<string>() { "USD", "UAH" },
				FromDate = DateTime.Today.AddDays(-30),
				ToDate = DateTime.Today
			};
			var validationService = new ValidationService();


			var actual = validationService.ValidateCurrencies(request);

			Assert.AreEqual("not-unique-currency", actual.ErrorCode);
		}

		//---------- Dates ---------
		[TestMethod]
		public void ValidateDates_AllDatesValid_ReturnsNull()
		{
			CurrencyDeltaRequest request = new CurrencyDeltaRequest()
			{
				Baseline = "UAH",
				Currencies = new List<string>() { "USD", "EUR" },
				FromDate = DateTime.Today.AddDays(-30),
				ToDate = DateTime.Today
			};
			var validationService = new ValidationService();


			var actual = validationService.ValidateDates(request);

			Assert.IsNull(actual);
		}

		[TestMethod]
		public void ValidateDates_DatesEqual_ReturnsError()
		{
			CurrencyDeltaRequest request = new CurrencyDeltaRequest()
			{
				Baseline = "UAH",
				Currencies = new List<string>() { "USD", "EUR" },
				FromDate = DateTime.Today,
				ToDate = DateTime.Today
			};
			var validationService = new ValidationService();


			var actual = validationService.ValidateDates(request);

			Assert.AreEqual("dates-equal", actual.ErrorCode);
		}

		[TestMethod]
		public void ValidateDates_ToDateAfterFrom_ReturnsError()
		{
			CurrencyDeltaRequest request = new CurrencyDeltaRequest()
			{
				Baseline = "UAH",
				Currencies = new List<string>() { "USD", "EUR" },
				FromDate = DateTime.Today,
				ToDate = DateTime.Today.AddDays(-30)
			};
			var validationService = new ValidationService();


			var actual = validationService.ValidateDates(request);

			Assert.AreEqual("dates-range", actual.ErrorCode);
		}
	}
}
