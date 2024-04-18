using YodaFunFactsApp2.Models;
using YodaFunFactsApp2.ViewModels;

namespace LoadSheddingApp.ViewModels
{
    [QueryProperty(nameof(Response), "Response")]
    public class YodaFunFactsApp2AnswerViewModel : BaseViewModel
    {
        private ChatMessage _questionResponseModel;

        public ChatMessage Response
        {
            get { return _questionResponseModel; }
            set
            {
                _questionResponseModel = value;

                OnPropertyChanged();
            }
        }

        public YodaFunFactsApp2AnswerViewModel ()
        {
        }
    }
}
