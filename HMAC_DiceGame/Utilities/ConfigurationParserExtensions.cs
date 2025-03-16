using HMAC_DiceGame.Models;

namespace HMAC_DiceGame.Utilities;

public static class ConfigurationParserExtensions
{
	private static void ValidateConfigurations(string[] configs)
	{
		if (configs.Length < 3) 
			throw new ArgumentException("ERROR: At least 3 dice are required");
	}

	public static List<Dice> ParseToDices(this string[] configs, char separator = ',')
	{
		ValidateConfigurations(configs);
		return [.. configs.Select(config => config.ParseToDice(separator))];
	}

	public static Dice ParseToDice(this string config, char separator)
	{
		try
		{
			var faces = config
				.Split(separator)
				.Select(s => int.Parse(s.Trim()));

			return new Dice([..faces]);
		}
		catch (FormatException)
		{
			throw new ArgumentException($"ERROR: Configuration contains invalid integers: {config}");
		}
		catch (OverflowException)
		{
			throw new ArgumentException($"ERROR: Configuration '{config}' contains values that are too large or too small");
		}
	}
}
