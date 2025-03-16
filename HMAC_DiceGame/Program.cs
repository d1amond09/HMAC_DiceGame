using HMAC_DiceGame.Core;
using HMAC_DiceGame.Models;
using HMAC_DiceGame.Utilities;

namespace HMAC_DiceGame;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            List<Dice> dices = args.ParseToDices();
            Game game = new(dices);
            game.Run();
        }
        catch (ArgumentException ex)
		{
			Console.WriteLine(ex.Message);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Unexpected error: {ex.Message}");
		}
	}
}
