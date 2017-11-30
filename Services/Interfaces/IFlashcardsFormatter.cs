using System.Collections.Generic;
using KindleVocabularyImporter.Models;

namespace KindleVocabularyImporter
{
    namespace Services
    {
        public interface IFlashcardsFormatter
        {
            ICollection<object> Format(IEnumerable<Flashcard> flashcards);
        }
    }
}