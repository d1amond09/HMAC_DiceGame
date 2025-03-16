using HMAC_DiceGame.Models;

namespace HMAC_DiceGame.Core;

public class RoundHandler(ThrowHandler throwHandler)
{
	private readonly ThrowHandler _throwHandler = throwHandler;

	public (int computerResult, int userResult) PlayRound(int currentPlayer, Dice computerDice, Dice userDice)
	{
		int computerThrow, userThrow;

		if (currentPlayer == 0)
		{
			computerThrow = _throwHandler.ComputerThrows(computerDice);
			userThrow = _throwHandler.PlayerThrows(userDice);
		}
		else
		{
			userThrow = _throwHandler.ComputerThrows(userDice);
			computerThrow = _throwHandler.PlayerThrows(computerDice);
		}

		return (computerThrow, userThrow);
	}
}
