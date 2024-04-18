using YodaFunFactsApp2.Models;
using YodaFunFactsApp2.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Platform;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using System.Collections.Generic;

namespace YodaFunFactsApp2.ViewModels
{
    public partial class YodaFunFactsQuestionViewModel : BaseViewModel
    {
        private IAiAssistant _assistant;
        private IYodaFunFactsDataService _dataservice;

        private ObservableCollection<ChatMessage> _chatHistory;

        public ObservableCollection<ChatMessage> ChatHistory
        {
            get { return _chatHistory; }
            set
            {
                _chatHistory = value;
                OnPropertyChanged();
            }
        }

        private string _currentQuestion;
        public string CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                OnPropertyChanged();
            }
        }

        public YodaFunFactsQuestionViewModel(IAiAssistant assistant, IYodaFunFactsDataService dataService)
        {
            _assistant = assistant;
            _dataservice = dataService;
            ChatHistory = new ObservableCollection<ChatMessage>
            {
                new ChatMessage { MessageType = Enums.ChatMessageTypeEnum.Inbound, MessageBody = "Help you today, how can I? Hm, hmm." }
            };
        }

        [RelayCommand]
        public async Task ChatSelected(ChatMessage message)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Response", message }
            };
            await Shell.Current.GoToAsync($"FunFactanswer", navigationParameter);
        }
        public async ValueTask AskQuestion(ITextInput view, CancellationToken token)
        {
            var inboundMessages = ChatHistory.Where(x => x.MessageType == Enums.ChatMessageTypeEnum.Inbound).ToList();
            var currentChatMessage = new ChatMessage { MessageType = Enums.ChatMessageTypeEnum.Outbound, MessageBody = CurrentQuestion };

            try
            {
                var response =_assistant.GetCompletion(inboundMessages, currentChatMessage);
                ChatHistory.Add(currentChatMessage);

                //var responseChatMessage = new ChatMessage { MessageType = Enums.ChatMessageTypeEnum.Inbound, MessageBody = response.Content };
                //ChatHistory.Add(responseChatMessage);

                CurrentQuestion = string.Empty;
            }
            catch (Exception ex)
            {
                
            }

            bool isSuccessful = await view.HideKeyboardAsync(token);
        }

        public async override void OnAppearing()
        {
            base.OnAppearing();
            await _dataservice.SyncSettings();
        }



    }
}
