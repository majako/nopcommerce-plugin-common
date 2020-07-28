using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Majako.Plugin.Common.Extensions;

namespace Majako.Plugin.Common.Abstractions.GoogleTranslate
{
    public interface IGoogleTranslateClient
    {
        Task<GoogleTranslateResult> Translate(string term, CultureInfo targetLanguage, CultureInfo inputLanguage = null, CancellationToken cancellationToken = default);
    }

    public readonly struct GoogleTranslateResult
    {
        public GoogleTranslateResult(CultureInfo outputLanguage, CultureInfo inputLanguage, string output, string input)
        {
            OutputLanguage = outputLanguage.NotNull(nameof(outputLanguage));
            InputLanguage = inputLanguage.NotNull(nameof(inputLanguage));
            Output = output.NotNull(nameof(output));
            Input = input.NotNull(nameof(input));
        }

        public CultureInfo OutputLanguage { get; }
        public CultureInfo InputLanguage { get; }
        public string Output { get; }
        public string Input { get; }
    }

}
