namespace ShelterCare.IntegrationTests;

public static class SingleQuoteExtension
{
    /// <summary>
    /// It is forbiden to use single quote while insering string values
    /// instead of ' it should be ''
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string SingleQuotes(this string value)
    {
        return value.Replace("'", "''");
    }
}
