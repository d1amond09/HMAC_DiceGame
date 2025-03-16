using HMAC_DiceGame.Models;

namespace HMAC_DiceGame.Core;

public class TurnManager(ThrowHandler throwHandler)
{
	private readonly ThrowHandler _throwHandler = throwHandler;
	private int _currentPlayer;

	public int DetermineFirstPlayer()
	{
		Dice firstMove = new([0, 1]);
		int firstPlayer = _throwHandler.ComputerThrows(firstMove);
		_currentPlayer = firstPlayer;
		return firstPlayer;
	}

	public int CurrentPlayer => _currentPlayer;
}