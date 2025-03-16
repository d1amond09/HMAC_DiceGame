using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTable;
using HMAC_DiceGame.Models;

namespace HMAC_DiceGame.Services;

public class TableGenerator
{
	private readonly ProbabilityCalculator _probCalculator = new();

	public void RenderTable(List<Dice> diceList)
	{
		Console.WriteLine("\nProbability of the win for the user:");
		Console.WriteLine("Each cell shows the probability that the user's dice (row)");
		Console.WriteLine("will win against the computer's dice (column)\n");

		var table = new Table().AddColumn("User Dice →");

		var columnHeaders = diceList
			.Select(d => string.Join(",", d.Faces))
			.ToArray();

		table.AddColumn(columnHeaders);

		for (var i = 0; i < diceList.Count; i++)
		{
			var currentDice = diceList[i];
			var row = new List<string> { string.Join(",", currentDice.Faces) };

			foreach (var opponentDice in diceList)
			{
				var probability = _probCalculator.CalculateProbability(currentDice, opponentDice);
				row.Add(FormatCell(probability, currentDice == opponentDice));
			}

			table.AddRow([.. row]);
		}

		Console.WriteLine(table.ToString());
		Console.WriteLine();
	}

	private static string FormatCell(double probability, bool isDiagonal) =>
		isDiagonal ? $"- ({probability:F4})" : $"{probability:F4}";

}