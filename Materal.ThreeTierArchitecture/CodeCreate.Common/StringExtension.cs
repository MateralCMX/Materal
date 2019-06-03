namespace CodeCreate.Common
{
    public static class StringExtension
    {
        public static string FirstLower(this string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString)) return inputString;
            return inputString[0].ToString().ToLower() + inputString.Substring(1);
        }
    }
}
