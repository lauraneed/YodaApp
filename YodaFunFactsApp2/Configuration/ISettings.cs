﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YodaFunFactsApp2.Configuration
{
    public interface ISettings
    {
        public string AzureSearchEndPoint { get;  }
        public string AzureSearchKey { get; }

        public string AzureOpenAiEndPoint { get; }
        public string AzureOpenAiKey { get; }

    }
}
