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
		public class FlashcardsImporter : IFlashcardsImporter
		{
			#region Private Members

			private readonly ILogger<FlashcardsImporter> logger;
			private readonly IRepository<Data.Models.Word> wordsRepository;

			#endregion

			#region Constructor

			public FlashcardsImporter(
				ILoggerFactory loggerFactory,
				IRepository<Data.Models.Word> wordsRepository)
			{
				this.logger = loggerFactory.CreateLogger<FlashcardsImporter>();
				this.wordsRepository = wordsRepository;
			}

			#endregion

			#region IFlashcardsImporter Members

			public IList<Flashcard> Import()
			{
				logger.LogInformation("Import has started");

				var words = wordsRepository.GetAll();
				var flashcards = new List<Flashcard>();

				words.ToList().ForEach(w => flashcards.Add(new Flashcard
				{
					Word = w.Name.ToLower(),
					Usage = w.Lookups.Select(l => new Lookup
					{
						Book = l.Book.Title,
						Usage = l.Usage
					}).ToList()
				}));

				logger.LogInformation("Import has finished");

				return flashcards;
			}

			#endregion
		}
	}
}