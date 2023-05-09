namespace Tech_test.Models
{
	public class CurrencyDeltaRequest
	{
		public string Baseline { get; set; }

		public List<string> Currencies { get; set; }

		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set;}
	}
}
