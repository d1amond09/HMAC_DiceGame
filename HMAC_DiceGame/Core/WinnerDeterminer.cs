namespace HMAC_DiceGame.Core;

public class WinnerDeterminer
{
	public void Determine(int computerThrowResult, int userThrowResult)
	{
		if (computerThrowResult == -1 || userThrowResult == -1)
		{
			Console.WriteLine("Game was exited.");
			return;
		}

		if (userThrowResult > computerThrowResult)
		{
			Console.WriteLine($"You win ({userThrowResult} > {computerThrowResult})!");
		}
		else if (userThrowResult < computerThrowResult)
		{
			Console.WriteLine($"I win ({computerThrowResult} > {userThrowResult})!");
		}
		else
		{
			Console.WriteLine($"It's a draw ({userThrowResult} = {computerThrowResult}).");
		}
	}
}
