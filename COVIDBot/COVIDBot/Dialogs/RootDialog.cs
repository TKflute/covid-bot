using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Adaptive;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Actions;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Conditions;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Recognizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COVIDBot.Dialogs
{
    public class RootDialog : ComponentDialog
    {
        public RootDialog() : base(nameof(RootDialog))
        {
            var rootDialog = new AdaptiveDialog
            {
                Recognizer = CreateRecognizer(),
                Triggers = new List<OnCondition>()
                {
                    new OnIntent()
                    {
                        Intent = "tests",
                        Actions = new List<Dialog>()
                        {
                            new SendActivity(MessageFactory.Text("Hey I am a COVID tests info bot"))
                        }
                    },
                    new OnIntent()
                    {
                        Intent = "cases",
                        Actions = new List<Dialog>()
                        {
                            new SendActivity(MessageFactory.Text("Hey I am a COVID cases info bot"))
                        }
                    },
                    new OnIntent()
                    {
                        Intent = "symptoms",
                        Actions = new List<Dialog>()
                        {
                            new SendActivity(MessageFactory.Text("Hey I am a COVID symptoms info bot"))
                        }
                    }, 
                    new OnUnknownIntent()
                    {
                        Actions = new List<Dialog>()
                        {
                            new SendActivity(MessageFactory.Text("I'm sorry; I do not understand. I can " +
                                "help with information about COVID tests, cases, or symptoms."))
                        }
                    }
                }
            };

            AddDialog(rootDialog);
            InitialDialogId = nameof(AdaptiveDialog);
        }

        private Recognizer CreateRecognizer()
        {
            var regex = new RegexRecognizer
            {
                Intents = new List<IntentPattern>()
                {
                    new IntentPattern("tests", "(?i)test(?i)"),
                    new IntentPattern("cases", "(?i)case(?i)"),
                    new IntentPattern("symptoms", "(?i)symptom(?i)")
                }
            };
            return regex;
        }
    }
}
