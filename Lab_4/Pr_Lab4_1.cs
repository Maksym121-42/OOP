namespace YouTubeObserverDemo;

public readonly record struct VideoPost(string Title, DateTime PublishedAt);

public interface IChannelObserver
{
    void OnVideoPublished(string channelName, VideoPost post);
}

public interface IChannelSubject
{
    void Register(IChannelObserver observer);
    void Unregister(IChannelObserver observer);
    void PublishVideo(string title);
}

public sealed class Subscriber : IChannelObserver
{
    public string Nickname { get; }

    public Subscriber(string nickname)
    {
        Nickname = nickname;
    }

    public void OnVideoPublished(string channelName, VideoPost post)
    {
        Console.WriteLine($"[Оповіщення] Користувач \"{Nickname}\": на каналі \"{channelName}\" додано ролик \"{post.Title}\" ({post.PublishedAt:HH:mm:ss})");
    }
}

public sealed class CreatorChannel : IChannelSubject
{
    private readonly HashSet<IChannelObserver> _observers = new();

    public string Name { get; }

    public CreatorChannel(string name)
    {
        Name = name;
    }

    public void Register(IChannelObserver observer)
    {
        if (_observers.Add(observer))
        {
            Console.WriteLine($"[Система] Підписку активовано. Кількість підписників: {_observers.Count}");
        }
    }

    public void Unregister(IChannelObserver observer)
    {
        if (_observers.Remove(observer))
        {
            Console.WriteLine($"[Система] Підписку вимкнено. Кількість підписників: {_observers.Count}");
        }
    }

    public void PublishVideo(string title)
    {
        var post = new VideoPost(title, DateTime.Now);

        Console.WriteLine();
        Console.WriteLine($"[Канал: {Name}] Опубліковано новий матеріал: {post.Title}");

        foreach (var observer in _observers.ToList())
        {
            observer.OnVideoPublished(Name, post);
        }
    }
}

public static class Pr_Lab4_1
{
    public static void Main()
    {
        var channel = new CreatorChannel("TechWave Studio");

        var userA = new Subscriber("Олена_27");
        var userB = new Subscriber("Максим.dev");
        var userC = new Subscriber("IraSky");

        channel.Register(userA);
        channel.Register(userB);
        channel.PublishVideo("Огляд бюджетного мікрофона");

        channel.Unregister(userB);
        channel.Register(userC);
        channel.PublishVideo("5 порад для чистого звуку");
    }
}
