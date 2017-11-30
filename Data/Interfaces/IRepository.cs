using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace KindleVocabularyImporter
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
    }
}