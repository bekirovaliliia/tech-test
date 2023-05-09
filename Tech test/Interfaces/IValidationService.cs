using Tech_test.Models;

namespace Tech_test.Interfaces
{
	public interface IValidationService
	{
		public List<ErrorDescription> Validate(CurrencyDeltaRequest request);
	}
}
