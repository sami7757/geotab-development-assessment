using System;
using System.Collections.Generic;

namespace JokeGenerator.Services
{
	public class ConsoleIOService : IConsoleIOService
	{
		public bool StartInstructions
		{
			get
			{
				Console.WriteLine("Press ? to get instructions.");
				return Console.ReadLine() == "?";
			}
		}
		public ConsoleKey GetSelectedOption(IList<string> options)
		{
			foreach (var option in options)
			{
				PrintWithNewLine(option);
			}

			return Console.ReadKey().Key;
		}

		public bool Confirm(string msg)
		{
			bool? result = null;
			do
			{
				PrintWithNewLine($"{msg} Y/N:");
				var key = Console.ReadKey().Key;
				if (key == ConsoleKey.Y) result = true;
				if (key == ConsoleKey.N) result = false;
			} while (result == null);

			return result.Value;
		}

		public int GetNumber(string msg, int max = 9)
		{
			int num;
			string input = string.Empty;
			do
			{
				PrintWithNewLine(msg);
				input = Console.ReadLine();
			} while (!int.TryParse(input, out num) || num > max || num <= 0);

			return num;
		}

		public string GetString(string msg)
		{
			PrintWithNewLine(msg);
			return Console.ReadLine();
		}

		public void Print(params string[] messages) => PrintWithNewLine(string.Join(Environment.NewLine, messages));

		private void PrintWithNewLine(string msg) => Console.WriteLine($"{Environment.NewLine}{msg}");
	}
}
