using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using KindleVocabularyImporter.Models;

namespace KindleVocabularyImporter
{
	namespace Services
	{
		public class FlashcardsTranslator : IFlashcardsTranslator
		{
			#region Private Fields

			private readonly IFlashcardsTranslatorResolver flashcardsTranslatorResolver;
			private readonly ILogger<FlashcardsTranslator> logger;
			private readonly IParamsContext paramsContext;

			#endregion

			#region Constructor

			public FlashcardsTranslator(
				IFlashcardsTranslatorResolver flashcardsTranslatorResolver,
				ILoggerFactory loggerFactory,
				IParamsContext paramsContext)
			{
				this.flashcardsTranslatorResolver = flashcardsTranslatorResolver;
				this.logger = loggerFactory.CreateLogger<FlashcardsTranslator>();
				this.paramsContext = paramsContext;
			}

			#endregion

			#region IFlashcardsTranslator Members

			public void Translate(ICollection<Flashcard> flashcards)
			{
				logger.LogInformation("Translation has started");

				var translator = flashcardsTranslatorResolver.Resolve(paramsContext.Language);

				foreach (var flashcard in flashcards)
				{
					logger.LogInformation("Translating '{0}'", flashcard.Word);
					int retries = 0;
					while (true)
					{
						try
						{
							var task = translator.TranslateAsync(flashcard);
							task.Wait();
							break;
						}
						catch (Exception exception)
						{
							if (++retries > 5) throw exception;
							logger.LogError("Exception occured while translating: '{0}'. Retrying: {1}/5", flashcard.Word, retries);
							Thread.Sleep(5000);
						}
					}
					if (flashcard.Translation is null) logger.LogWarning("'{0}' translation is missing", flashcard.Word);
				}

				logger.LogInformation("Translation has finished");
			}

			#endregion
		}
	}
}