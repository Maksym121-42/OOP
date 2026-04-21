/*
    Decorator (Декоратор)
    Документ має базовий функціонал, а додаткові можливості
    підключаються окремими обгортками: шифрування, стиснення, водяний знак, логування.

*/


public interface IDocument
{
    string GetContent();
}

public class BasicDocument : IDocument
{
    private readonly string _content;

    public BasicDocument(string content)
    {
        _content = content;
    }

    public string GetContent()
    {
        return _content;
    }
}

public abstract class DocumentDecorator : IDocument
{
    protected readonly IDocument InnerDocument;

    protected DocumentDecorator(IDocument innerDocument)
    {
        InnerDocument = innerDocument;
    }

    public virtual string GetContent()
    {
        return InnerDocument.GetContent();
    }
}

public class EncryptionDecorator : DocumentDecorator
{
    public EncryptionDecorator(IDocument innerDocument) : base(innerDocument) { }

    public override string GetContent()
    {
        return $"[Encrypted]{base.GetContent()}[/Encrypted]";
    }
}

public class CompressionDecorator : DocumentDecorator
{
    public CompressionDecorator(IDocument innerDocument) : base(innerDocument) { }

    public override string GetContent()
    {
        return $"[Compressed]{base.GetContent()}[/Compressed]";
    }
}

public class WatermarkDecorator : DocumentDecorator
{
    private readonly string _watermark;

    public WatermarkDecorator(IDocument innerDocument, string watermark) : base(innerDocument)
    {
        _watermark = watermark;
    }

    public override string GetContent()
    {
        return $"{base.GetContent()} [Watermark: {_watermark}]";
    }
}

public class AccessLogDecorator : DocumentDecorator
{
    private readonly string _user;

    public AccessLogDecorator(IDocument innerDocument, string user) : base(innerDocument)
    {
        _user = user;
    }

    public override string GetContent()
    {
        Console.WriteLine($"LOG: User '{_user}' requested document at {DateTime.Now:HH:mm:ss}");
        return base.GetContent();
    }
}

public class Pr_Lab3_5
{
    public static void Main()
    {
        IDocument doc1 = new BasicDocument("Contract #123");
        doc1 = new CompressionDecorator(doc1);
        doc1 = new EncryptionDecorator(doc1);
        doc1 = new WatermarkDecorator(doc1, "CONFIDENTIAL");
        doc1 = new AccessLogDecorator(doc1, "student_user");

        Console.WriteLine("Document 1:");
        Console.WriteLine(doc1.GetContent());
        Console.WriteLine();

        IDocument doc2 = new BasicDocument("Invoice #77");
        doc2 = new EncryptionDecorator(doc2);
        doc2 = new CompressionDecorator(doc2);

        Console.WriteLine("Document 2:");
        Console.WriteLine(doc2.GetContent());
    }
}
