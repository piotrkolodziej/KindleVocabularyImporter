using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using KindleVocabularyImporter.Models;
using HtmlAgilityPack;

namespace KindleVocabularyImporter
{
	namespace Services
	{
		public class HtmlFormatter : IFlashcardsFormatter
		{
			#region Private Members

			private readonly ILogger<HtmlFormatter> logger;

			#endregion

			#region Constructor

			public HtmlFormatter(ILoggerFactory loggerFactory)
			{
				this.logger = loggerFactory.CreateLogger<HtmlFormatter>();
			}

			#endregion

			#region IFlashcardsFormatter Members

			public ICollection<object> Format(IEnumerable<Flashcard> flashcards)
			{
				logger.LogInformation("Formatting has started");

				List<object> result = new List<object>();

				foreach (var flashcard in flashcards)
				{
					result.Add(new
					{
						Front = FormatFront(flashcard),
						Back = FormatBack(flashcard)
					});
				}

				logger.LogInformation("Formatting has finished");

				return result;
			}

			#endregion

			#region Private Methods

			private string FormatFront(Flashcard flashcard)
			{
				return flashcard.Word;
			}

			private string FormatBack(Flashcard flashcard)
			{
				StringWriter stringWriter = new StringWriter();
				var back = new HtmlDocument();
				back.Load(Path.GetFullPath(Path.Combine("Templates", "back.html")));
				var translationNode = back.DocumentNode.SelectSingleNode("//div[contains(@class, 'translation')]");
				var usageNode = back.DocumentNode.SelectSingleNode("//div[contains(@class, 'usages')]");
				FormatTranslation(translationNode, flashcard.Translation);
				FormatUsage(usageNode, flashcard.Usage);
				back.Save(stringWriter);
				return stringWriter.ToString().Replace("\t", "");
			}

			private void FormatTranslation(HtmlNode translationNode, Translation translation)
			{
				if (translation is null) return;

				var leftNode = translationNode.SelectSingleNode("//div[contains(@class, 'left')]");
				var rightNode = translationNode.SelectSingleNode("//div[contains(@class, 'right')]");

				foreach (var result in translation.Results)
				{
					var word = String.Format("<span class='word'>{0}</span>", result.Form);
					var pos = String.Format("<span class='pos'>{0}</span>", result.PartOfSpeech);
					var meanings = String.Format("<span class='meanings'>{0}</span>", String.Join(" · ", result.Meanings.ToArray()));
					var divLeft = HtmlNode.CreateNode($"<div>{word}{pos}</div>");
					var divRight = HtmlNode.CreateNode($"<div>{meanings}</div>");
					leftNode.AppendChild(divLeft);
					rightNode.AppendChild(divRight);
				}
			}

			private void FormatUsage(HtmlNode usageNode, ICollection<Lookup> usages)
			{
				if (usageNode is null) return;

				foreach (var usage in usages)
				{
					var usageFormatted = String.Format("<span class='quote'>'{0}'</span><span class='book'> –– {1}</span>", usage.Usage.Trim(), usage.Book);
					var divNode = HtmlNode.CreateNode($"<div>{usageFormatted}</div>");
					usageNode.AppendChild(divNode);
				}
			}

			#endregion
		}
	}
}