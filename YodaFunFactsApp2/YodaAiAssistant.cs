using Azure;
using Azure.AI.OpenAI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YodaFunFactsApp2.Configuration;
using YodaFunFactsApp2.Models;
using YodaFunFactsApp2.Services.Interfaces;

namespace YodaFunFactsApp2.Services
{
    public class YodaAiAssistant : IAiAssistant
    {
        private readonly ISettings _settings;
        private const string AssistantBehaviorDescription = "With your fun fact questions, help you, I can, hmm.";
        public YodaAiAssistant(ISettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        private IList<ChatRequestMessage> BuildChatContext(IList<ChatMessage> chatInboundHistory, ChatMessage userMessage)
        {
            var chatContext = new List<ChatRequestMessage>();

            chatContext.Add(new ChatRequestSystemMessage(AssistantBehaviorDescription));

            foreach (var chatMessage in chatInboundHistory)
                chatContext.Add(new ChatRequestAssistantMessage(chatMessage.MessageBody));

            chatContext.Add(new ChatRequestUserMessage(userMessage.MessageBody));

            return chatContext;
        }

        public Azure.AI.OpenAI.ChatResponseMessage GetCompletionAsync(IList<ChatMessage> chatInboundHistory, ChatMessage userMessage)
        {
            var messages = BuildChatContext(chatInboundHistory, userMessage);

            var client = new OpenAIClient(new Uri(_settings.AzureSearchEndPoint),  new AzureKeyCredential(_settings.AzureOpenAiKey));
            string deploymentName = "gpt38turbo18";
            string searchIndex = "index";


            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                AzureExtensionsOptions = new AzureChatExtensionsOptions()
                {
                    Extensions =
{
    new AzureSearchChatExtensionConfiguration()
    {
        SearchEndpoint = new Uri(_settings.AzureSearchEndPoint),
        Authentication = new OnYourDataApiKeyAuthenticationOptions(_settings.AzureSearchKey),

        IndexName = searchIndex,
    },
            }
                },
                DeploymentName = deploymentName
            };

            foreach (var message in messages)
                chatCompletionsOptions.Messages.Add(message);

            Response<ChatCompletions> response = client.GetChatCompletions(chatCompletionsOptions);

            Azure.AI.OpenAI.ChatResponseMessage responseMessage = response.Value.Choices[0].Message;

            return responseMessage;

        }


    }
}

