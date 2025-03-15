namespace HMAC_DiceGame;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            List<Dice> dices = args.ParseToDices();

            foreach (var dice in dices)
				Console.WriteLine(dice);
            
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
