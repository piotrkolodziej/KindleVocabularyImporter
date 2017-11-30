
using System.Collections.Generic;

namespace KindleVocabularyImporter
{
	namespace Models
	{
		public class Translation
		{
			#region Constructor

			public Translation()
			{
				Results = new List<TranslationResult>();
			}

			#endregion

			#region Properties

			public IList<TranslationResult> Results { get; set; }

			#endregion
		}

		public class TranslationResult
		{
			#region Constructor

			public TranslationResult(string form, string pos, ICollection<string> meanings)
			{
				Form = form;
				Meanings = meanings;
				PartOfSpeech = pos;
			}

			#endregion

			#region Properties

			public string Form { get; set; }

			public ICollection<string> Meanings { get; set; }

			public string PartOfSpeech { get; set; }

			#endregion
		}
	}
}