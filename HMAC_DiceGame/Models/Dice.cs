namespace HMAC_DiceGame.Models;

public class Dice(int[] faces)
{
	public int[] Faces { get; } = faces;
	public int Length => Faces.Length;

	public int Roll(int index) => Faces[index];

	public override string ToString()
	{
		return string.Join(',', Faces);
	}
}
