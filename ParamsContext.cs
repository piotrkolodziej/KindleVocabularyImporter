namespace KindleVocabularyImporter
{
	public interface IParamsContext
	{
		string Language { get; set; }

		string Exporter { get; set; }
		
		string Formatter { get; set; }
	}

	public class ParamsContext : IParamsContext
	{
		#region IParamsContext Members

		public string Language { get; set; }

		public string Exporter { get; set; }

		public string Formatter { get; set; }

		#endregion
	}
}