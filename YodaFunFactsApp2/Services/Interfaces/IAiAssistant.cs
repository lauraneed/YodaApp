using Azure.Core;
using YodaFunFactsApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace YodaFunFactsApp2.Services.Interfaces
{
    public class IAiAssistant
    {
        internal object GetCompletion(List<ChatMessage> inboundMessages, ChatMessage currentChatMessage)
        {
            throw new NotImplementedException();
        }

        ChatResponseMessage GetCompletion(IList<ChatMessage> chatInBoundHistory, ChatMessage userMessage);
    }
}
