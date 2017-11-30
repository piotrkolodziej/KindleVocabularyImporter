using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using KindleVocabularyImporter.Data;
using KindleVocabularyImporter.Data.Models;

namespace KindleVocabularyImporter
{
	public class WordsRepository : IRepository<Word>
	{
		#region Private Fields

		private readonly KindleContext context;

		#endregion

		#region Constructor

		public WordsRepository(KindleContext context)
		{
			this.context = context;
		}

		#endregion

		#region IRepository<T> Members

		public IQueryable<Word> GetAll()
		{
			return context.Set<Word>()
                .Include(x => x.Lookups)
				    .ThenInclude(y => y.Book);
		}

		#endregion
	}
}