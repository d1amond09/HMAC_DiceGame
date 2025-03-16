namespace HMAC_DiceGame.Core;

public class TurnManager(ThrowHandler throwHandler)
{
	private readonly ThrowHandler _throwHandler = throwHandler;
	private int _currentPlayer;

	public int DetermineFirstPlayer()
	{
		int firstPlayer = _throwHandler.ComputerThrows(2, null);
		_currentPlayer = firstPlayer;
		return firstPlayer;
	}

	public int CurrentPlayer => _currentPlayer;
}