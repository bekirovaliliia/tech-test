using Microsoft.AspNetCore.Mvc;
using Tech_test.Interfaces;
using Tech_test.Models;
using Tech_test.Servises;

namespace Tech_test.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CurrencyDeltaController : ControllerBase
	{
		private readonly ILogger<CurrencyDeltaController> _logger;
		private readonly IDeltaService _deltaService;
		private readonly IRatesService _ratesService;
		private readonly IValidationService _validationService;

		public CurrencyDeltaController(ILogger<CurrencyDeltaController> logger, IDeltaService deltaService,
			IRatesService ratesService,
			IValidationService validationService)
		{
			_logger = logger;
			_deltaService = deltaService;
			_ratesService = ratesService;
			_validationService = validationService;
		}
		
// --- Test request --------

//	{
//  "baseline": "UAH",
//  "currencies": [
//    "USD", "EUR"
//  ],
//  "fromDate": "2022-05-07",
//  "toDate": "2023-05-07"
//  }

	    [HttpPost(Name = "currency-delta")]
		public async Task<IActionResult> CalculateCurrencyDelta([FromBody] CurrencyDeltaRequest request )
		{
			try
			{
				//request validation 
				List<ErrorDescription> errors = _validationService.Validate(request);

				if (errors.Count != 0)
				{
					return BadRequest(errors);
				}
				else
				{
					//get history values from rates api
					CalculateDeltaRequest calculateDeltaRequest = await _ratesService.GetHistoryValue(request);

					if (calculateDeltaRequest.FromValue.result == "error")
					{
						return BadRequest(new ErrorDescription()
								{
									ErrorCode = "rate-api-error",
									ErrorDetails = calculateDeltaRequest.FromValue.error_type
								});
					}
					else if (calculateDeltaRequest.ToValue.result == "error")
					{
						return BadRequest(new ErrorDescription()
						{
							ErrorCode = "rate-api-error",
							ErrorDetails = calculateDeltaRequest.ToValue.error_type
						});
					}

					//delta calculation
					else
					{
						calculateDeltaRequest.Currencies = request.Currencies;

						List<Delta> deltas = _deltaService.CalcucateDeltas(calculateDeltaRequest);

						_logger.Log(LogLevel.Information, "Delta`s calculation successfull");

						return Ok(deltas);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Log(LogLevel.Error, ex.ToString());
				return BadRequest(new ErrorDescription() { ErrorCode = ex.ToString() });
			}
		}
	}
}