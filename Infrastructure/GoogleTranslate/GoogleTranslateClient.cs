using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Majako.Plugin.Common.Abstractions.GoogleTranslate;
using Majako.Plugin.Common.Extensions;
using Newtonsoft.Json.Linq;

namespace Majako.Plugin.Common.Infrastructure.GoogleTranslate
{
    public sealed class GoogleTranslateClient : IGoogleTranslateClient
    {
        private readonly HttpClient httpClient;

        public GoogleTranslateClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<GoogleTranslateResult> Translate(string term, CultureInfo targetLanguage, CultureInfo inputLanguage = null, CancellationToken cancellationToken = default)
        {
            targetLanguage.NotNull(nameof(targetLanguage));
            term.NotNull(nameof(term)); //It can be empty, null is just for error checking.
            string sourceLang = (inputLanguage == null) ? default : inputLanguage.Name;

            string url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={sourceLang}&tl={targetLanguage.Name}&dt=t&q={HttpUtility.UrlEncode(term)}";

            var result = await httpClient.GetAsync(url); //Exception handling should be handled in user code
            result.EnsureSuccessStatusCode();
            var resultText = await result.Content.ReadAsStringAsync();
            var jsonResult = JObject.Parse(resultText);

            var translatedText = jsonResult.First.First.First;
            return new GoogleTranslateResult(targetLanguage, inputLanguage, translatedText.Value<string>(), term);
        }
    }
}
