using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using KindleVocabularyImporter.Data;
using KindleVocabularyImporter.Models;

namespace KindleVocabularyImporter
{
	namespace Services
	{
		public class FlashcardsExporter : IFlashcardsExporter
		{
			#region Private Members

			private readonly IFlashcardsExporterResolver flashcardsExporterResolver;
			private readonly ILogger<FlashcardsExporter> logger;
			private readonly IParamsContext paramsContext;

			#endregion

			#region Constructor

			public FlashcardsExporter(
				IFlashcardsExporterResolver flashcardsExporterResolver,
				ILoggerFactory loggerFactory,
				IParamsContext paramsContext)
			{
				this.logger = loggerFactory.CreateLogger<FlashcardsExporter>();
				this.flashcardsExporterResolver = flashcardsExporterResolver;
				this.paramsContext = paramsContext;
			}

			#endregion

			#region IFlashcardsExporter Members

			public void Export(IEnumerable<Flashcard> flashcards)
			{
				logger.LogInformation("Export has started");

				var flashcardsExporter = flashcardsExporterResolver.Resolve(paramsContext.Exporter);
				flashcardsExporter.Export(flashcards);

				logger.LogInformation("Export has finished");
			}

			#endregion
		}
	}
}