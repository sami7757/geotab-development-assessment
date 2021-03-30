using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JokeGenerator.Services
{
	public interface IJokeService
	{
		Task<IList<string>> GetCategories();
		Task<IList<string>> GetJokes(string category, int jokesCount);
		Task<Tuple<string, string>> GetRandomName();
	}
}