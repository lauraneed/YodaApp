using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YodaFunFactsApp2.Configuration
{
    public class Settings : ISettings
    {
        public string AzureSearchEndPoint { get => "<Your Azure Cognitive Search Endpoint>"; }
        public string AzureSearchKey { get =>"<Your Azure Cognitive Search Key>"; }

        public string AzureOpenAiEndPoint { get =>"<Your Azure Open AI Enpoint>"; }
        public string AzureOpenAiKey { get =>"<Your Azure Open AI AP Key>"; }
    }
}
