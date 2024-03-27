namespace UlukunShopAPI.Infrastructure.Operations;

public static class NameOperation
{
    public static string CharacterRegulatory(string name)

        => name.Replace("\"", "")
            .Replace("!", "")
            .Replace("'", "")
            .Replace("^", "")
            .Replace("+", "")
            .Replace("%", "")
            .Replace("&", "")
            .Replace("/", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("=", "")
            .Replace("?", "")
            .Replace("_", "")
            .Replace("", "")
            .Replace("@", "")
            .Replace("€", "")
            .Replace("~", "")
            .Replace(",", "")
            .Replace(";", "")
            .Replace(":", "")
            .Replace(".", "-")
            .Replace("Ö", "o")
            .Replace("ö", "o")
            .Replace("Ü", "u")
            .Replace("ü", "u")
            .Replace("ı", "i")
            .Replace("İ", "i")
            .Replace("ç", "c")
            .Replace("Ç", "c")
            .Replace("ß", "")
            .Replace("æ", "")
            .Replace("â", "a")
            .Replace("î", "i")
            .Replace("ş", "s")
            .Replace("Ş", "s")
            .Replace("<", "")
            .Replace(">", "")
            .Replace("|", "")
            .Replace("[", "")
            .Replace("]", "")
            .Replace("{", "")
            .Replace("}", "");



}