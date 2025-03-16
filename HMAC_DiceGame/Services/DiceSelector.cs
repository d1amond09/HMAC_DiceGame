using HMAC_DiceGame.Models;
using HMAC_DiceGame.Utilities;

namespace HMAC_DiceGame.Services;

public class DiceSelector(List<Dice> dices, List<Dice> dicesForTable, TableGenerator table)
{
	private readonly List<Dice> _dices = dices;
	private readonly List<Dice> _dicesForTable = dicesForTable;
	private readonly TableGenerator _table = table;

	public Dice ComputerSelectsDice()
	{
		var selectedDice = _dices[new Random().Next(_dices.Count)];
		Console.WriteLine($"I choose the [{string.Join(",", selectedDice.Faces)}] dice.");
		_dices.Remove(selectedDice);
		return selectedDice;
	}

	public Dice UserSelectsDice()
	{
		OutputChoices();

		var userChoice = Console.ReadLine() ?? "";
		if (userChoice == "X") GameExiter.Exit();

		if (userChoice == "?")
		{
			_table.RenderTable(_dicesForTable);
			return UserSelectsDice();
		}

		int selectedIndex = int.Parse(userChoice);
		Dice selectedDice = _dices[selectedIndex];
		Console.WriteLine($"You choose the [{string.Join(",", selectedDice.Faces)}] dice.");
		_dices.RemoveAt(selectedIndex);
		return selectedDice;
	}

	private void OutputChoices()
	{
		Console.WriteLine("Choose your dice:");
		for (int i = 0; i < _dices.Count; i++)
		{
			Console.WriteLine($"{i} - [{string.Join(",", _dices[i].Faces)}]");
		}
		Console.WriteLine("X - exit");
		Console.WriteLine("? - help");
	}

}