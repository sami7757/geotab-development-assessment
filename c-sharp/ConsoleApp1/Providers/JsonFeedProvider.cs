using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JokeGenerator.Providers
{
	public class JsonFeedProvider : IJsonFeedProvider
	{
		// These are probable placed inside config files per environment (TEST/STAGE/PROD), 
		// I guess its ok to keep here for this assignment
		const string URL = "https://api.chucknorris.io";
		const string NAMES_URL = "https://www.names.privserv.com/api/";
		readonly HttpClient client;
		public JsonFeedProvider()
		{
			client = new HttpClient();
		}

		public async Task<IList<dynamic>> GetRandomJokes(int jokesCount, string category = null)
		{
			var tasks = new List<Task<string>>();
			for (var i = 0; i < jokesCount; i++)
			{
				tasks.Add(client.GetStringAsync(GetJokesURL(category)));
			}

			await Task.WhenAll(tasks);

			return tasks.Select(task => JsonConvert.DeserializeObject<dynamic>(task.Result).value).ToList();
		}

		private string GetJokesURL(string category)
		{
			string url = $"{URL}/jokes/random";
			if (!string.IsNullOrEmpty(category))
			{
				url += $"?category={category}";
			}

			return url;
		}

		/// <summary>
		/// returns an object that contains name and surname
		/// </summary>
		/// <param name="client2"></param>
		/// <returns></returns>
		public async Task<dynamic> GetNames()
		{
			var result = await client.GetStringAsync(NAMES_URL);
			return JsonConvert.DeserializeObject<dynamic>(result);
		}

		public async Task<string> GetCategories()
		{
			return await client.GetStringAsync(URL + "/jokes/categories");
		}
	}
}
