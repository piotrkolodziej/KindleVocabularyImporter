using System.Collections.Generic;

namespace KindleVocabularyImporter
{
    namespace Data
    {
        namespace Models
        {
            public class Lookup
            {
                public string Id { get; set; }
                public string BookId { get; set; }
                public string WordId { get; set; }
                public string Usage { get; set; }
                public virtual Book Book { get; set; }
                public virtual Word Word { get; set; }
            }
        }
    }
}