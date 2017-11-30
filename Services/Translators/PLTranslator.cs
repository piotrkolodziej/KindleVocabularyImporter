using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using HtmlAgilityPack;
using KindleVocabularyImporter.Models;

namespace KindleVocabularyImporter
{
	namespace Services
	{
		public class PLTranslator : IFlashcardTranslator
		{
			#region IFlashcardsTranslator Members

			public async Task TranslateAsync(Flashcard flashcard)
			{
				var url = $"https://en.bab.la/dictionary/english-polish/{flashcard.Word}";
				var client = new System.Net.Http.HttpClient();

				using (var response = await client.GetAsync(url))
				{
					using (var content = response.Content)
					{
						var result = await content.ReadAsStringAsync();
						var doc = new HtmlDocument();
						doc.LoadHtml(result);
						var quickResultsNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'quick-results container')]");
						var quickResultsEntryNodes = quickResultsNode.SelectNodes(".//div[contains(@class, 'quick-result-entry') and .//div[contains(@class, 'quick-result-option')]]");
						if (quickResultsEntryNodes is null) return;
						var translation = new Translation();

						foreach (var quickResultsEntryNode in quickResultsEntryNodes)
						{
							var quickResultOptionNode = quickResultsEntryNode.SelectSingleNode(".//div[contains(@class, 'quick-result-option')]");
							var quickResultOverviewNode = quickResultsEntryNode.SelectSingleNode(".//div[contains(@class, 'quick-result-overview')]");
							var senseGroupResultsNode = quickResultOverviewNode.SelectSingleNode(".//ul[contains(@class, 'sense-group-results')]");
							var form = quickResultOptionNode.SelectSingleNode(".//a[contains(@class, 'babQuickResult')]");
							var pos = quickResultOptionNode.SelectSingleNode(".//span[contains(@class, 'suffix')]");

							if (form is null || pos is null) continue; 

							translation.Results.Add(new TranslationResult(form.InnerText, pos.InnerText, ExtractMeanings(senseGroupResultsNode)));
						}

						if(translation.Results.Count > 0) flashcard.Translation = translation;
					}
				}
			}

			#endregion

			#region Private Methods

			private ICollection<string> ExtractMeanings(HtmlNode listNode)
			{
				var listItemNodes = listNode.SelectNodes(".//li[//a]//a");
				var meanings = new List<string>();
				foreach (var listItemNode in listItemNodes)
				{
					meanings.Add(listItemNode.InnerText);
				}
				return meanings;
			}

			#endregion
		}
	}
}