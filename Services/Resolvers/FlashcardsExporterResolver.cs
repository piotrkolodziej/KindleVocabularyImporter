using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using KindleVocabularyImporter.Models;

namespace KindleVocabularyImporter
{
	namespace Services
	{
		public class FlashcardsExporterResolver : IFlashcardsExporterResolver
		{
			#region Private Members

			private readonly ILogger<FlashcardsExporterResolver> logger;
			private readonly IServiceProvider serviceProvider;

			#endregion

			#region Constructor

			public FlashcardsExporterResolver(
				ILoggerFactory loggerFactory,
				IServiceProvider serviceProvider)
			{
				this.logger = loggerFactory.CreateLogger<FlashcardsExporterResolver>();
				this.serviceProvider = serviceProvider;
			}

			#endregion

			#region IFlashcardsExporterResolver Members

			public IFlashcardsExporter Resolve(string format)
			{
				switch (format.ToLower())
				{
					case "html":
						return serviceProvider.GetService<HtmlExporter>();
					default:
						throw new NotImplementedException("Unknown exporter.");
				}
			}

			#endregion
		}
	}
}