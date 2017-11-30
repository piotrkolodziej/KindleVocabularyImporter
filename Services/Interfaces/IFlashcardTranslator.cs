using System.Collections.Generic;
using System.Threading.Tasks;
using KindleVocabularyImporter.Models;

namespace KindleVocabularyImporter
{
    namespace Services
    {
        public interface IFlashcardTranslator
        {
            Task TranslateAsync(Flashcard flashcard);
        }
    }
}