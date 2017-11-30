using KindleVocabularyImporter.Services;

namespace KindleVocabularyImporter
{
    namespace Services
    {
        public interface IFlashcardsTranslatorResolver
        {
            IFlashcardTranslator Resolve(string countryCode);
        }
    }
}