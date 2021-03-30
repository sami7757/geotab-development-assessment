using System.Threading.Tasks;

namespace JokeGenerator.Controllers
{
	public interface IJokesController
	{
		/// <summary>
		/// Entry point to start the console interaction to show jokes
		/// </summary>
		/// <returns></returns>
		Task StartJokes();
	}
}