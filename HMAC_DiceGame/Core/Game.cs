using HMAC_DiceGame.Models;
using HMAC_DiceGame.Services;

namespace HMAC_DiceGame.Core;

public class Game
{
	private readonly TurnManager _turnManager;
	private readonly DiceSelector _diceSelector;
	private readonly ThrowHandler _throwHandler;
	private Dice? _computerDice;
	private Dice? _userDice;
	private int _computerThrowResult;
	private int _userThrowResult;

	

	public Game(List<Dice> dices)
	{
		var dicesForTable = new List<Dice>(dices);
		var rndGenerator = new RandomGenerator();
		var table = new TableGenerator();
		_throwHandler = new ThrowHandler(rndGenerator, table, dicesForTable);

		_turnManager = new TurnManager(_throwHandler);
		_diceSelector = new DiceSelector(dices, dicesForTable, table);
	}

	public void Run()
	{
		Start();
		PlayRound();
		DetermineWinner();
	}

	private void Start()
	{
		Console.WriteLine("Let's determine who makes the first move.");
		int firstPlayer = _turnManager.DetermineFirstPlayer();

		if (firstPlayer == 0)
		{
			Console.WriteLine("I make the first move.");
			_computerDice = _diceSelector.ComputerSelectsDice();
			_userDice = _diceSelector.UserSelectsDice();
		}
		else
		{
			Console.WriteLine("You make the first move.");
			_userDice = _diceSelector.UserSelectsDice();
			_computerDice = _diceSelector.ComputerSelectsDice();
		}
	}

	private void PlayRound()
	{
		Console.WriteLine("It's time for the throws.");
		(_computerThrowResult, _userThrowResult) = PlayRound(_turnManager.CurrentPlayer);
	}

	public (int computerResult, int userResult) PlayRound(int currentPlayer)
	{
		int computerThrow, userThrow;

		if (currentPlayer == 0)
		{
			computerThrow = _throwHandler.ComputerThrows(_computerDice);
			userThrow = _throwHandler.PlayerThrows(_userDice);
		}
		else
		{
			userThrow = _throwHandler.ComputerThrows(_computerDice);
			computerThrow = _throwHandler.PlayerThrows(_userDice);
		}

		return (computerThrow, userThrow);
	}

	private void DetermineWinner()
	{
		if (_computerThrowResult == -1 || _userThrowResult == -1)
		{
			Console.WriteLine("Game was exited.");
			return;
		}

		if (_userThrowResult > _computerThrowResult)
		{
			Console.WriteLine($"You win ({_userThrowResult} > {_computerThrowResult})!");
		}
		else if (_userThrowResult < _computerThrowResult)
		{
			Console.WriteLine($"I win ({_computerThrowResult} > {_userThrowResult})!");
		}
		else
		{
			Console.WriteLine($"It's a draw ({_userThrowResult} = {_computerThrowResult}).");
		}
	}
}