using Tech_test.Models;

namespace Tech_test.Interfaces
{
	public interface IDeltaService
	{
		public List<Delta> CalcucateDeltas(CalculateDeltaRequest request);
	}
}
