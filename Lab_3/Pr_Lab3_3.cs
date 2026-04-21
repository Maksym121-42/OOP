using System;
using System.Collections.Generic;

public interface IVideo
{
    string GetTitle();
    void ShowCard();
    void ShowDetails(bool userHasPremium);
}

public class RealVideo : IVideo
{
    private readonly string _movieName;
    private string _fullInfo;

    public RealVideo(string movieName)
    {
        _movieName = movieName;
        LoadFullInfo();
    }

    private void LoadFullInfo()
    {
        Console.WriteLine($"[RealVideo] Завантаження важких даних для '{_movieName}'...");
        _fullInfo = "Опис, актори, рейтинг, рекомендації";
    }

    public string GetTitle() => _movieName;

    public void ShowCard()
    {
        Console.WriteLine($"- {_movieName}");
    }

    public void ShowDetails(bool userHasPremium)
    {
        Console.WriteLine($"[RealVideo] Деталі '{_movieName}': {_fullInfo}");
    }
}

public class VideoProxy : IVideo
{
    private readonly string _movieName;
    private readonly bool _onlyForPremium;
    private RealVideo _loadedVideo;

    public VideoProxy(string movieName, bool onlyForPremium)
    {
        _movieName = movieName;
        _onlyForPremium = onlyForPremium;
    }

    public string GetTitle() => _movieName;

    public void ShowCard()
    {
        string premiumText = _onlyForPremium ? " [PREMIUM]" : "";
        Console.WriteLine($"- {_movieName}{premiumText}");
    }

    public void ShowDetails(bool userHasPremium)
    {
        if (_onlyForPremium && !userHasPremium)
        {
            Console.WriteLine($"[Proxy] Відмовлено! '{_movieName}' лише для Premium.");
            return;
        }

        if (_loadedVideo == null)
        {
            Console.WriteLine($"[Proxy] Дані ще не завантажені -> створюємо RealVideo");
            _loadedVideo = new RealVideo(_movieName);
        }
        else
        {
            Console.WriteLine($"[Proxy] Беремо дані з кешу для '{_movieName}'");
        }

        _loadedVideo.ShowDetails(userHasPremium);
    }
}

class Pr_Lab3_3
{
    static void Main()
    {
        var movieList = new List<IVideo>
        {
            new VideoProxy("Термінатор", false),
            new VideoProxy("Dune 2", true)
        };

        Console.WriteLine("=== Каталог (лише список) ===");
        foreach (var movieItem in movieList) movieItem.ShowCard();

        Console.WriteLine("\n=== Звичайний користувач відкриває 'Термінатор' ===");
        movieList[0].ShowDetails(false);

        Console.WriteLine("\n=== Звичайний користувач відкриває 'Dune 2' ===");
        movieList[1].ShowDetails(false);

        Console.WriteLine("\n=== Premium користувач відкриває 'Dune 2' ===");
        movieList[1].ShowDetails(true);

        Console.WriteLine("\n=== Premium користувач відкриває 'Dune 2' ще раз ===");
        movieList[1].ShowDetails(true);
    }
}
