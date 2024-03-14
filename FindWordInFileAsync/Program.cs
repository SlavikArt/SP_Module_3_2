class Program
{
    static async Task Main()
    {
        Console.Write("Word to search: ");
        string word = Console.ReadLine();

        Console.Write("Path to the file: ");
        string filePath = Console.ReadLine();

        int timesCount = await FindWordInFileAsync(word, filePath);

        if (timesCount == -1)
            Console.WriteLine("File not found.");
        else
            Console.WriteLine($"The word '{word}' is found {timesCount} times in the file.");
    }

    static async Task<int> FindWordInFileAsync(string word, string filePath)
    {
        try
        {
            string textContent = await File.ReadAllTextAsync(filePath);

            int searchedWordCount = textContent
                .Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Count(w => w.Equals(word, StringComparison.OrdinalIgnoreCase));
            return searchedWordCount;
        }
        catch (FileNotFoundException)
        {
            return -1;
        }
    }
}
