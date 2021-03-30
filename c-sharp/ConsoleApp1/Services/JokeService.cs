using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JokeGenerator.Providers;

namespace JokeGenerator.Services
{
	public class JokeService : IJokeService
	{
		private readonly IJsonFeedProvider feed;
		public JokeService(IJsonFeedProvider feed)
		{
			this.feed = feed;
		}

		public async Task<Tuple<string, string>> GetRandomName()
		{
			dynamic result = await feed.GetNames();
			return Tuple.Create(result.name.ToString(), result.surname.ToString());
		}

		public async Task<IList<string>> GetJokes(string category, int jokesCount)
		{
			var jokesResult = await feed.GetRandomJokes(jokesCount, category);
			return jokesResult.Select(joke => joke.Value as string).ToList();
		}

		public async Task<IList<string>> GetCategories()
		{
			var response = await feed.GetCategories();
			return JsonConvert.DeserializeObject<IList<string>>(response);
		}
	}
}
