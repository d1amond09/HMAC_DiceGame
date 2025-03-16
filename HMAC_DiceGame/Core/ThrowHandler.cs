using HMAC_DiceGame.Models;
using HMAC_DiceGame.Services;
using HMAC_DiceGame.Utilities;

namespace HMAC_DiceGame.Core;

public class ThrowHandler(RandomGenerator rndGenerator, TableGenerator table, List<Dice> dicesForTable)
{
	private readonly RandomGenerator _rndGenerator = rndGenerator;
	private readonly TableGenerator _table = table;
	private readonly List<Dice> _dicesForTable = dicesForTable;

	public int ComputerThrows(int range, Dice? dice)
	{
		return Throws(range, dice);
	}

	public int PlayerThrows(int range, Dice dice)
	{
		return Throws(range, dice);
	}

	private int Throws(int range, Dice? dice)
	{
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
			return Throws(range, dice);
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

		Console.WriteLine($"My throw is {throwResult}.");

		return throwResult;
	}
}