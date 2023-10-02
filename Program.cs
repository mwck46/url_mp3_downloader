// 'Top-level statements' is supported by .NET6+

using System;

string binDir = Directory.GetCurrentDirectory();
//Console.WriteLine(binDir);
string workDir = binDir + "/../../../";
//Console.WriteLine(workDir);

using var client = new HttpClient();
Random rnd = new Random();

for (int idx = 2; idx <= 100; idx++)
{
    string fn = idx.ToString("D3");
    try
    {
        string url = $"https://www.targetpage.com/audio/{fn}.mp3";
        Console.WriteLine(url);
        using var s = await client.GetStreamAsync(url);
        using var fs = new FileStream($"{workDir}Downloads/{fn}.mp3", FileMode.OpenOrCreate);
        await s.CopyToAsync(fs);
        fs.Close();

        // To prevent the website think that we are DDoS it and block our IP
        int waitTime = rnd.Next(50, 150);
        Console.WriteLine($"Wait {waitTime} sec");
        Thread.Sleep(waitTime * 100);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}