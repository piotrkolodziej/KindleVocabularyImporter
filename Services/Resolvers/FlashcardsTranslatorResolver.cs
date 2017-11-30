using System;
using Microsoft.Extensions.DependencyInjection;

namespace KindleVocabularyImporter
{
	namespace Services
	{
		public class FlashcardsTranslatorResolver : IFlashcardsTranslatorResolver
		{
			#region Private Fields

			private readonly IServiceProvider serviceProvider;

			#endregion

			#region Constructor

			public FlashcardsTranslatorResolver(IServiceProvider serviceProvider)
			{
				this.serviceProvider = serviceProvider;
			}

			#endregion

			#region FlashcardsTranslatorResolver Members

			public IFlashcardTranslator Resolve(string countryCode)
			{
				switch (countryCode.ToLower())
				{
					case "pl":
						return serviceProvider.GetService<PLTranslator>();
					default:
						throw new NotImplementedException("Unknown adapter.");
				}
			}

			#endregion
		}
	}
}