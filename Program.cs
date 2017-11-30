using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CommandLine;
using KindleVocabularyImporter.Data;
using KindleVocabularyImporter.Models;
using KindleVocabularyImporter.Services;

namespace KindleVocabularyImporter
{
	class Program
	{
		// USAGE:
		//
		//  dotnet run kwi [-t | -translate] <country_iso> [-e | -exporter] <exporter> [-f | -formatter] <formatter>
		//
		static void Main(string[] args)
		{
			args = new string[] { "-t", "PL", "-e", "html", "-f", "html" };

			var serviceProvider = Configure(args);

			// Import the words from Kindle
			//
			var flashcards = serviceProvider.GetService<IFlashcardsImporter>().Import();

			// Translate words
			//
			serviceProvider.GetService<IFlashcardsTranslator>().Translate(flashcards);

			// Export flashcards
			//
			serviceProvider.GetService<IFlashcardsExporter>().Export(flashcards.Where(x => x.Translation != null));
		}

		private static IServiceProvider Configure(string[] args)
		{
			// Setup DI
			//
			var sc = new ServiceCollection()
				.Scan(x =>
					x.FromEntryAssembly()
					.AddClasses(classes => classes.AssignableTo<IFlashcardsExporter>())
					.AsSelf().WithTransientLifetime()
					.AddClasses(classes => classes.AssignableTo<IFlashcardsFormatter>())
					.AsSelf().WithTransientLifetime()
					.AddClasses(classes => classes.AssignableTo<IFlashcardTranslator>())
					.AsSelf().WithTransientLifetime())
				.AddLogging()
				.AddDbContext<KindleContext>()
				.AddTransient<IFlashcardsExporterResolver, FlashcardsExporterResolver>()
				.AddTransient<IFlashcardsFormatterResolver, FlashcardsFormatterResolver>()
				.AddTransient<IFlashcardsTranslatorResolver, FlashcardsTranslatorResolver>()
				.AddTransient<IFlashcardsImporter, FlashcardsImporter>()
				.AddTransient<IFlashcardsTranslator, FlashcardsTranslator>()
				.AddTransient<IFlashcardsExporter, FlashcardsExporter>()
				.AddTransient<IRepository<Data.Models.Word>, WordsRepository>();
				
			// Parse input arguments
			//
			var result = Parser.Default.ParseArguments<Options>(args);

			result.WithParsed(o => sc.AddSingleton<IParamsContext>(
				new ParamsContext
				{
					Language = o.Language,
					Exporter = o.Exporter,
					Formatter = o.Formatter
				}));

			var serviceProvider = sc.BuildServiceProvider();
			
			// Configure logging
			//
			serviceProvider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);

			return serviceProvider;
		}
	}
}