namespace Skyblock_Bazzar_Tracker
{
    public static class AssistFunctions
    {
        public static string ConvertToCamelCase(string input)
        {
            input = input.ToLower();
            string[] words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
            }
            return string.Join(" ", words);
        }
    }
}
