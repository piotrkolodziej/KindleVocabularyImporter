using KindleVocabularyImporter.Services;

namespace KindleVocabularyImporter
{
    namespace Services
    {
        public interface IFlashcardsFormatterResolver
        {
            IFlashcardsFormatter Resolve(string formatter);
        }
    }
}