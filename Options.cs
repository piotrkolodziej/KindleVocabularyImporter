using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace KindleVocabularyImporter
{
    public class Options
    {
        [Option('t', "translate", Required = true, HelpText = "Option followed by ISO country code.")]
        public string Language { get; set; }

        [Option('e', "exporter", Required = true, HelpText = "Exporter to use (e.g. 'csv').")]
        public string Exporter { get; set; }

        [Option('f', "format", Required = true, HelpText = "Formatter to use (e.g. 'html').")]
        public string Formatter { get; set; }
    }
}