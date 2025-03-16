using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text;

namespace HMAC_DiceGame.Services;

public class RandomGenerator
{
	private const int MIN_LENGTH = 32;

	private byte[] _key;
	private byte[] _hmac;
	private int _number;

	public string HMAC => FromByteToString(_hmac);
	public string Key => FromByteToString(_key);
	public int Number => _number;


	public RandomGenerator()
	{
		_key = new byte[MIN_LENGTH];
		_hmac = new byte[MIN_LENGTH];
	}

	public void GenerateKey()
	{
		_key = new byte[MIN_LENGTH];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(_key);
	}

	public void CalculateHMACSHA256(int range)
	{
		using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(Key));
		_number = RandomNumberGenerator.GetInt32(range);
		byte[] message = Encoding.UTF8.GetBytes(_number.ToString());
		_hmac = hmac.ComputeHash(message);
	}

	public int GetResult(int userNumber, int range) => 
		(_number + userNumber) % range;

	public string FromByteToString(byte[] bytes) =>
		BitConverter.ToString(bytes).Replace("-", "").ToLower();
}