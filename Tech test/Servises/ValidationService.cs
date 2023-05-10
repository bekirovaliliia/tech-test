using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Tech_test.Interfaces;
using Tech_test.Models;
using Tech_test.Models.ExchangeRateApi;

namespace Tech_test.Servises
{
	public class ValidationService : IValidationService
	{
		public List<ErrorDescription> Validate(CurrencyDeltaRequest request)
		{
			try
			{
				List<ErrorDescription> errors = new List<ErrorDescription>();

				ErrorDescription currenciesValidationResult = ValidateCurrencies(request);

				if (currenciesValidationResult != null)
				{
					errors.Add(currenciesValidationResult);
				}

				ErrorDescription datesValidationResult = ValidateDates(request);

				if (datesValidationResult != null)
				{
					errors.Add(datesValidationResult);
				}

				return errors;
			}
			catch(Exception ex)
			{
				throw new Exception("Validation error: "+ex.Message);
			}
		}

		public ErrorDescription ValidateCurrencies(CurrencyDeltaRequest request)
		{
			if (typeof(ConversionRate).GetProperty(request.Baseline) is null)
				return new ErrorDescription()
				{
					ErrorCode = "invalid-currency-baseline",
					ErrorDetails = "Currency doesn`t exist"
				};
			foreach (string cur in request.Currencies)
			{
				var prop = typeof(ConversionRate).GetProperty(cur);
				if (prop is null)

					return new ErrorDescription()
					{
						ErrorCode = "invalid-currency",
						ErrorDetails = "Currency doesn`t exist"
					};
			}

			if (request.Currencies.Contains(request.Baseline))
				return new ErrorDescription()
				{
					ErrorCode = "not-unique-currency",
					ErrorDetails = "Currencies are not unique"
				};

			if (request.Currencies.Count != request.Currencies.Distinct().Count())
				return new ErrorDescription()
				{
					ErrorCode = "not-unique-currency",
					ErrorDetails = "Currencies are not unique"
				};
			else return null;
		}

		public ErrorDescription ValidateDates(CurrencyDeltaRequest request)
		{
			if (request.FromDate == request.ToDate)
				return new ErrorDescription()
				{
					ErrorCode = "dates-equal",
					ErrorDetails = "From and to dates are equal"
				};

			if (request.FromDate > request.ToDate)
				return new ErrorDescription()
				{
					ErrorCode = "dates-range",
					ErrorDetails = "To date is after from date"
				};
			else return null;
		}
	}
}
