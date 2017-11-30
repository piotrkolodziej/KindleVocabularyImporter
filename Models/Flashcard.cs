using System.Collections.Generic;
using KindleVocabularyImporter.Data.Models;

namespace KindleVocabularyImporter
{
    namespace Models
    {
        public class Flashcard
        {
            #region Properties

            public string Word { get; set; }

            public Translation Translation { get; set; }

            public ICollection<Lookup> Usage { get; set; }

            #endregion
        }
    }
}