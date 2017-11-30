using System.Collections.Generic;
using KindleVocabularyImporter.Models;

namespace KindleVocabularyImporter
{
    namespace Services
    {
        public interface IFlashcardsExporter
        {
            void Export(IEnumerable<Flashcard> flashcards);
        }
    }
}