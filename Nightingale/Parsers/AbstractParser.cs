﻿using System;
using System.Linq;
using System.Text;
using Nightingale.Domain;
using System.Threading.Tasks;
using Nightingale.Extensions;
using System.Collections.Generic;

namespace Nightingale.Parsers
{
    public abstract class AbstractParser
    {
        protected FeatherLogger _logger;

        protected ISet<Source> SourcesToInsert = new HashSet<Source>();

        private Source _currentSource;
        private Quote _currentQuote;
        private Word _currentWord;

        public abstract bool ImportFile(string databasePath, string filePath);

        protected void AddNewSource(Source s)
        {
            SourcesToInsert.Add(s);
            _currentSource = s;
            _currentQuote = null;
            _currentWord = null;
        }

        protected void AddNewQuote(Quote q)
        {
            _currentSource.Quotes.Add(q);
            _currentQuote = q;
            _currentWord = null;
        }

        protected void AddNewWord (Word w)
        {
            _currentQuote.Words.Add(w);
            _currentWord = w;
        }

        protected bool LineContainsKana(string oneLine)
        {
            bool containsKana = false;
            var charArray =
                "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわゐゑをんがぎぐげござじずぜぞだぢづでどばびぶべぼぱぴぷぺぽ"
                .ToCharArray();

            for (int i = 0; i < charArray.Length && !containsKana; i++)
                containsKana = oneLine.Contains(charArray[i]);

            return containsKana;
        }

        protected bool LineContainsAlphabetLetters(string oneLine)
        {
            bool containsAlphabetLetters = false;
            var charArray = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            for (int i = 0; i < charArray.Length && !containsAlphabetLetters; i++)
                containsAlphabetLetters = oneLine.Contains(charArray[i]);

            return containsAlphabetLetters;
        }

    }


}
