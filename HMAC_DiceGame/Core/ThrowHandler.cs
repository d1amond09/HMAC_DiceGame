using HMAC_DiceGame.Models;
using HMAC_DiceGame.Services;
using HMAC_DiceGame.Utilities;

namespace HMAC_DiceGame.Core;

public class ThrowHandler(RandomGenerator rndGenerator, TableGenerator table, List<Dice> dicesForTable)
{
	private readonly RandomGenerator _rndGenerator = rndGenerator;
	private readonly TableGenerator _table = table;
	private readonly List<Dice> _dicesForTable = dicesForTable;

	public int ComputerThrows(Dice? dice)
	{
		return Throws(dice, true);
	}

	public int PlayerThrows(Dice? dice)
	{
		return Throws(dice, false);
	}

	private int Throws(Dice? dice, bool isComputer)
	{
		int range = dice!.Length;
		_rndGenerator.GenerateKey();
		_rndGenerator.CalculateHMACSHA256(range);
		var hmac = _rndGenerator.HMAC;

		Console.WriteLine($"I selected a random value in the range 0..{range - 1} (HMAC={hmac}).");
		Console.WriteLine("Try to guess my selection.");
		for (int i = 0; i < range; i++)
			Console.WriteLine($"{i} - {i}");

		Console.WriteLine("X - exit");
		Console.WriteLine("? - help");

		var userChoice = Console.ReadLine() ?? "";
		if (userChoice == "X") GameExiter.Exit();

		if (userChoice == "?")
		{
			_table.RenderTable(_dicesForTable);
			return Throws(dice, isComputer);
		}

		int userNumber = int.Parse(userChoice);
		int computerNumb = _rndGenerator.Number;

		Console.WriteLine($"My number is: {computerNumb} (KEY={_rndGenerator.Key}).");

		if (range == 2)
		{
			int currentPlayer = userNumber.Equals(computerNumb) ? 1 : 0;
			return currentPlayer;
		}

		var result = _rndGenerator.GetResult(userNumber, range);
		Console.WriteLine($"The result is {computerNumb} + {userNumber} = {result} (mod 6).");
		
		ArgumentNullException.ThrowIfNull(dice);
		int throwResult = dice.Roll(result);


		string who = isComputer ? "My" : "Your";
		Console.WriteLine($"{who} throw is {throwResult}.");

		return throwResult;
	}
}