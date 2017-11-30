using System.Collections.Generic;

namespace KindleVocabularyImporter
{
    namespace Data
    {
        namespace Models
        {
            public class Word
            {
                public string Id { get; set; }

                public string Name { get; set; }
                
                public virtual ICollection<Lookup> Lookups { get; set; }
            }
        }
    }
}