namespace KindleVocabularyImporter
{
    namespace Services
    {
        public interface IFlashcardsExporterResolver
        {
            IFlashcardsExporter Resolve(string exporter);
        }
    }
}