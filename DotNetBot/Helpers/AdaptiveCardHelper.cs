using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBot
{
    public static class AdaptiveCardHelper
    {
        public static Attachment CreateAdaptiveCardAttachment(string cardResourceName, StringDictionary tokens = null)
        {
            var cardResourcePath = typeof(AdaptiveCardHelper).Assembly.GetManifestResourceNames().First(name => name.Contains(cardResourceName, StringComparison.OrdinalIgnoreCase));

            using (var stream = typeof(AdaptiveCardHelper).Assembly.GetManifestResourceStream(cardResourcePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    var adaptiveCard = reader.ReadToEnd();

                    if (tokens != null)
                        foreach (string tokenKey in tokens.Keys)
                            adaptiveCard = adaptiveCard.Replace("{{" + tokenKey + "}}", tokens[tokenKey]);

                    return new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(adaptiveCard),
                    };
                }
            }
        }


    }
}
