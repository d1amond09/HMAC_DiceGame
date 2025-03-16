using HMAC_DiceGame.Models;

namespace HMAC_DiceGame.Services;

public class ProbabilityCalculator
{
	public double CalculateProbability(Dice dice1, Dice dice2)
	{
		int wins = CountWins(dice1, dice2);
		int total = dice1.Faces.Length * dice2.Faces.Length;
		return (double)wins / total;
	}

	public int CountWins(Dice a, Dice b) =>
		a.Faces.Select(x => ~Array.BinarySearch(b.Faces, x,
			Comparer<int>.Create((x, y) => Math.Sign(x - y + 0.5)))).Sum();
}
