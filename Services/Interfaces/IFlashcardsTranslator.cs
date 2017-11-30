using System.Collections.Generic;
using KindleVocabularyImporter.Models;

namespace KindleVocabularyImporter
{
    namespace Services
    {
        public interface IFlashcardsTranslator
        {
            void Translate(ICollection<Flashcard> flashcards);
        }
    }
}