Kindle Vocabulary Importer (With Translation)
=======================

Background
============

Every time you are checking a word in Kindle built-in dictionary, the device remembers the word in a small SQLite database that this project utilizes. 

The idea behind the project was to export those words (along with translation) into CSV file that contains flashcards recognizable by Anki app.

![alt text](https://github.com/piotrkolodziej/KindleVocabularyImporter/blob/master/Flashcard01.PNG "Polish Flashcard")

Requirements
============

* .NET Core
* vocab.db (/Volumes/Kindle/system/vocabulary/vocab.db)
* Anki (https://apps.ankiweb.net/)

Translation
============

At the moment the only supported language is Polish. I am not planning on extending it to support other languages anytime soon, but would gladly appreciate contributors––adding a translation for other languages is as simple as implementing **IFlashcardTranslator** interface.

Installation
============

Just clone the repository and you're good to go.

Usage
=====

1. Put your vocab.db inside the project folder (sample vocab.db is included in the project). 
2. Run the program with parameters:

		dotnet run KindleVocabularyImporter [-t | -translate] <country_iso> [-e | -exporter] <exporter> [-f | -formatter] <formatter>
		
		Example:
		
		dotnet run KindleVocabularyImporter -t "PL" -e "html" -f "html"
