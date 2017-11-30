using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using KindleVocabularyImporter.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace KindleVocabularyImporter
{
	namespace Services
	{
		public class HtmlExporter : IFlashcardsExporter
		{
			#region Private Members

			private readonly IFlashcardsFormatterResolver flashcardsFormatterResolver;
			private readonly ILogger<HtmlExporter> logger;
			private readonly IParamsContext paramsContext;

			#endregion

			#region Constructor

			public HtmlExporter(
				IFlashcardsFormatterResolver flashcardsFormatterResolver,
				ILoggerFactory loggerFactory,
				IParamsContext paramsContext)
			{
				this.flashcardsFormatterResolver = flashcardsFormatterResolver;
				this.logger = loggerFactory.CreateLogger<HtmlExporter>();
				this.paramsContext = paramsContext;
			}

			#endregion

			#region IFlashcardsExporter Members

			public void Export(IEnumerable<Flashcard> flashcards)
			{
				var formatter = flashcardsFormatterResolver.Resolve(paramsContext.Formatter);
				var formattedFlashcards = formatter.Format(flashcards);

				using (StreamWriter writer = File.CreateText("vocab.csv"))
				{
					var csv = new CsvWriter(writer, new Configuration { Delimiter = "\t" });
					csv.WriteRecords(formattedFlashcards);
				}
			}

			#endregion
		}
	}
}