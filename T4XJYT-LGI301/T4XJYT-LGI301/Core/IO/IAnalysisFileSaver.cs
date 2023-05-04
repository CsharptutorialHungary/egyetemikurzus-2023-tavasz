using System;

namespace T4XJYT_LGI301.Core.IO
{
	public interface IAnalysisFileSaver
	{
		Task SaveAnalysisAsXML();

		Task SaveAnalysisAsJSON();
	}
}

