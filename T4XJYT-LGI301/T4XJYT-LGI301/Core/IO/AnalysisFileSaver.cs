using System;
using T4XJYT_LGI301.Core.Models;

namespace T4XJYT_LGI301.Core.IO
{
	public class AnalysisFileSaver : IAnalysisFileSaver
	{
        public async Task SaveAnalysisAsXml(TextAnalysis textAnalysis)
        {
            // TODO: Implement SaveAnalysisAsXML function
            await Task.Run(() => throw new NotImplementedException());
        }

        public async Task SaveAnalysisAsJson(TextAnalysis textAnalysis)
        {
            // TODO: Implement SaveAnalysisAsJSON function
            await Task.Run(() => throw new NotImplementedException());
        }
    }
}

