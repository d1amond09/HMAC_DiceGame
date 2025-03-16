using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleTable;
using HMAC_DiceGame.Models;

namespace HMAC_DiceGame.Services;

public class TableGenerator
{
	private ProbabilityCalculator _probCalculator;

	public TableGenerator()
	{
		_probCalculator = new();
	}

	public void RenderTable(List<Dice> diceList)
	{
		var table = new Table().AddColumn("User Dice v");

		var columnHeaders = diceList.Select(d => string.Join(",", d.Faces)).ToArray();
		table.AddColumn(columnHeaders);

		for (int i = 0; i < diceList.Count; i++)
		{
			var row = new List<string> { string.Join(",", diceList[i].Faces) };
			for (int j = 0; j < diceList.Count; j++)
			{
				if (i == j)
				{
					row.Add("-");
				}
				else
				{
					var probability = _probCalculator.CalculateProbability(diceList[i], diceList[j]);
					row.Add(probability.ToString("F3"));
				}
			}
			table.AddRow([.. row]);
		}

		Console.WriteLine("Probability of the win for the user:");
		Console.WriteLine(table);
	}
}
