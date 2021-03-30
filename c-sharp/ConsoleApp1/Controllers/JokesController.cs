using JokeGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeGenerator.Controllers
{
	public class JokesController : IJokesController
	{
		private readonly IConsoleIOService ioService;
		private readonly IJokeService jokeService;
		public JokesController(IConsoleIOService ioService, IJokeService jokeService)
		{
			this.ioService = ioService;
			this.jokeService = jokeService;
		}

		/// <summary>
		/// Entry point to start the console interaction to show jokes
		/// </summary>
		/// <returns></returns>
		public async Task StartJokes()
		{
			if (!ioService.StartInstructions) return;
			while (true)
			{
				var options = new List<string> { $"Press {ConsoleKey.C} to get categories",
												$"Press {ConsoleKey.R} to get random jokes" };
				var key = ioService.GetSelectedOption(options);
				switch(key)
				{
					case ConsoleKey.C:
						await DisplayCategories();
						break;
					case ConsoleKey.R:
						await DisplayRandomJokes();
						break;
					default: 
						ioService.Print("Invalid option!"); 
						break;
				}

				if (!ioService.Confirm("Do you want to continue?")) break;
			}
		}

		private async Task DisplayCategories()
		{
			var categories = await jokeService.GetCategories();
			ioService.Print(categories.ToArray());
		}

		private async Task DisplayRandomJokes()
		{
			var category = string.Empty;
			Task<Tuple<string, string>> namesTask = null;
			if (ioService.Confirm("Want to use a random name?"))
			{
				namesTask = jokeService.GetRandomName(); // gets the names in the background
			}

			if (ioService.Confirm("Want to specify a category?"))
			{
				category = await GetValidCategory();
			}

			int jokesCount = ioService.GetNumber("How many jokes do you want? (1-9) [Followed by Enter key]");
			var jokes = await jokeService.GetJokes(category, jokesCount);
			
			if (namesTask != null)
			{
				var names = await namesTask; // get response or wait if not finished
				jokes = ReplaceName(jokes, names);
			}

			ioService.Print(jokes.ToArray());
		}

		private IList<string> ReplaceName(IList<string> jokes, Tuple<string, string> names)
		{
			return jokes.Select(joke => joke.Replace("Chuck", names?.Item1).Replace("Norris", names?.Item2)).ToList();
		}

		private async Task<string> GetValidCategory()
		{
			var category = string.Empty;
			var categories = await jokeService.GetCategories();
			do
			{
				if (!string.IsNullOrEmpty(category))
				{
					ioService.Print($"{category} is not a valid category.");
				}

				category = ioService.GetString("Enter a category.[Followed by Enter key]");
			} while (!IsValid(category, categories));

			return category;
		}

		private bool IsValid(string category, IList<string> categories)
		{
			return categories.Any(cat => string.Equals(cat, category, StringComparison.CurrentCultureIgnoreCase));
		}
	}
}
