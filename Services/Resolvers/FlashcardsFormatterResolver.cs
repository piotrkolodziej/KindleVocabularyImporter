using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using KindleVocabularyImporter.Models;

namespace KindleVocabularyImporter
{
	namespace Services
	{
		public class FlashcardsFormatterResolver : IFlashcardsFormatterResolver
		{
			#region Private Members

            private readonly ILogger<FlashcardsFormatterResolver> logger;
			private readonly IServiceProvider serviceProvider;

			#endregion

			#region Constructor

			public FlashcardsFormatterResolver(
                ILoggerFactory loggerFactory,
                IServiceProvider serviceProvider)
			{
                this.logger = loggerFactory.CreateLogger<FlashcardsFormatterResolver>();
				this.serviceProvider = serviceProvider;
			}

			#endregion

			#region IFlashcardsFormatterResolver Members

			public IFlashcardsFormatter Resolve(string formatter)
			{
				switch (formatter.ToLower())
				{
					case "html":
						return serviceProvider.GetService<HtmlFormatter>();
					default:
						throw new NotImplementedException("Unknown formatter.");
				}
			}

			#endregion
		}
	}
}