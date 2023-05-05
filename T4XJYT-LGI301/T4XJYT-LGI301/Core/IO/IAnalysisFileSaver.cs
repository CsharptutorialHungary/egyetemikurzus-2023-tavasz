using T4XJYT_LGI301.Core.Models;

namespace T4XJYT_LGI301.Core.IO
{
	public interface IAnalysisFileSaver
	{
		Task SaveAnalysis(TextAnalysis textAnalysis, FileFormat format);
	}
}

