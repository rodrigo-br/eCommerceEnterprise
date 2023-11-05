namespace ECE.Core.Utils
{
	public static class StringUtils
	{
		public static string DigitFilter(this string str)
		{
			return new string(str.Where(char.IsDigit).ToArray());
		}
	}
}
