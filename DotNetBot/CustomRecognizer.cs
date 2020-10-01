// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Extensions.Configuration;

namespace DotNetBot
{
    public class CustomRecognizer : IRecognizer
    {
        public Task<RecognizerResult> RecognizeAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(this.RecognizeInternal(turnContext.Activity.Text));
        }

        public Task<T> RecognizeAsync<T>(ITurnContext turnContext, CancellationToken cancellationToken)
            where T : IRecognizerConvert, new()
        {
            cancellationToken.ThrowIfCancellationRequested();
            T result = new T();
            RecognizerResult recognizerResult = this.RecognizeInternal(turnContext.Activity.Text);
            result.Convert(recognizerResult);
            return Task.FromResult(result);
        }

        private RecognizerResult RecognizeInternal(string utterance)
        {
            string utteranceLowered = utterance.ToLower().Trim();

            if (utteranceLowered.Contains("help")) // be careful with 'Contains'!
            {
                return new RecognizerResult
                {
                    Intents = new Dictionary<string, IntentScore>
                    {
                        { DotNetBotConstants.Help, new IntentScore { Score = 1 } },
                    },
                    Text = utterance,
                };
            }
            else if (utteranceLowered == "/reset")
            {
                return new RecognizerResult
                {
                    Intents = new Dictionary<string, IntentScore>
                    {
                        { DotNetBotConstants.Reset, new IntentScore { Score = 1 } },
                    },
                    Text = utterance,
                };
            }
            else if (utteranceLowered == "/admin")
            {
                return new RecognizerResult
                {
                    Intents = new Dictionary<string, IntentScore>
                    {
                        { DotNetBotConstants.Admin, new IntentScore { Score = 1 } },
                    },
                    Text = utterance,
                };
            }
            else if (utteranceLowered == "/cardtest1")
            {
                return new RecognizerResult
                {
                    Intents = new Dictionary<string, IntentScore>
                    {
                        { DotNetBotConstants.CardTest1, new IntentScore { Score = 1 } },
                    },
                    Text = utterance,
                };
            }
            else if (utteranceLowered == "/cardtest2")
            {
                return new RecognizerResult
                {
                    Intents = new Dictionary<string, IntentScore>
                    {
                        { DotNetBotConstants.CardTest2, new IntentScore { Score = 1 } },
                    },
                    Text = utterance,
                };
            }
            else
            {
                return new RecognizerResult();
            }
        }

    }
}
