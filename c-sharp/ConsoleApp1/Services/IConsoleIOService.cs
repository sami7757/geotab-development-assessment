using System;
using System.Collections.Generic;

namespace JokeGenerator.Services
{
	public interface IConsoleIOService
	{
		bool StartInstructions { get; }

		bool Confirm(string msg);
		ConsoleKey GetSelectedOption(IList<string> options);
		int GetNumber(string msg, int max = 9);
		string GetString(string msg);
		void Print(params string[] messages);
	}
}