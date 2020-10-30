// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace COVIDBot
{
    public class AdaptiveBot<T> : ActivityHandler
        where T : Dialog
    {
        private readonly DialogManager _dialogManager;
        protected readonly ILogger _logger;
        public AdaptiveBot(ConversationState conversationState, UserState userState, T rootDialog, ILogger<AdaptiveBot<T>> logger)
        {
            _logger = logger;
            _dialogManager = new DialogManager(rootDialog)
            {
                ConversationState = conversationState,
                UserState = userState
            };
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Running dialog with Activity.");
            await _dialogManager.OnTurnAsync(turnContext, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        //protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        //{
        //    foreach (var member in membersAdded)
        //    {
        //        if (member.Id != turnContext.Activity.Recipient.Id)
        //        {
        //            await turnContext.SendActivityAsync(MessageFactory.Text($"Hello world!"), cancellationToken);
        //        }
        //    }
        //}
    }
}
