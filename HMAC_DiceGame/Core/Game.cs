using HMAC_DiceGame.Models;
using HMAC_DiceGame.Services;

namespace HMAC_DiceGame.Core;

public class Game
{
	private readonly TurnManager _turnManager;
	private readonly DiceSelector _diceSelector;
	private readonly RoundHandler _roundHandler;
	private readonly WinnerDeterminer _winnerDeterminer;
	private Dice _computerDice;
	private Dice _userDice;
	private int _computerThrowResult;
	private int _userThrowResult;

	public Game(List<Dice> dices)
	{
		var dicesForTable = new List<Dice>(dices);
		var rndGenerator = new RandomGenerator();
		var table = new TableGenerator();
		var throwHandler = new ThrowHandler(rndGenerator, table, dicesForTable);

		_turnManager = new TurnManager(throwHandler);
		_diceSelector = new DiceSelector(dices, dicesForTable, table);
		_roundHandler = new RoundHandler(throwHandler);
		_winnerDeterminer = new WinnerDeterminer();
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
		(_computerThrowResult, _userThrowResult) = _roundHandler.PlayRound(_turnManager.CurrentPlayer, _computerDice, _userDice);
	}

	private void DetermineWinner()
	{
		_winnerDeterminer.Determine(_computerThrowResult, _userThrowResult);
	}
}