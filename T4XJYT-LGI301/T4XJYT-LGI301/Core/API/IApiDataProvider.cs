using System;

namespace T4XJYT_LGI301.Core.API
{
	public interface IApiDataProvider
	{
		Task<string> GetTextFromAPI();
	}
}

