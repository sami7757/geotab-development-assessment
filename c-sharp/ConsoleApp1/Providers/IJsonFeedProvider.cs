using System.Collections.Generic;
using System.Threading.Tasks;

namespace JokeGenerator.Providers
{
	public interface IJsonFeedProvider
	{
		Task<string> GetCategories();
		Task<dynamic> GetNames();
		Task<IList<dynamic>> GetRandomJokes(int jokesCount, string category = null);
	}
}