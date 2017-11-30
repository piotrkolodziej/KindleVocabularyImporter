using System.Collections.Generic;
using KindleVocabularyImporter.Models;

namespace KindleVocabularyImporter
{
    namespace Services
    {
        public interface IFlashcardsImporter
        {
            IList<Flashcard> Import();
        }
    }
}