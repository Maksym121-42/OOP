using System.Threading.Tasks;
using System.Xml.Linq;

// 1) Що хоче ваша внутрішня система (єдиний формат)
public interface IPaymentInfo
{
    decimal GetAmount();
    string GetCurrency();
    string GetStatus();
}

// 2) Перший провайдер (умовно JSON-модель)
public class JsonProviderPayment
{
    public decimal amount_value;
    public string currency_code = "";
    public string transaction_state = "";
}

// 3) Адаптер для першого провайдера
public class JsonPaymentAdapter : IPaymentInfo
{
    private readonly JsonProviderPayment payment;

    public JsonPaymentAdapter(JsonProviderPayment paymentData)
    {
        payment = paymentData;
    }

    public decimal GetAmount()
    {
        return payment.amount_value;
    }

    public string GetCurrency()
    {
        return payment.currency_code;
    }

    public string GetStatus()
    {
        return payment.transaction_state;
    }
}

// 4) Другий провайдер (повертає XML)
public class XmlProvider
{
    public string paymentXml = "<payment><amount>150.25</amount><currency>EUR</currency><status>PENDING</status></payment>";
}

// 5) Адаптер для XM L-провайдера
public class XmlPaymentAdapter : IPaymentInfo
{
    private readonly XDocument document;

    public XmlPaymentAdapter(string xmlText)
    {
        document = XDocument.Parse(xmlText);
    }

    public decimal GetAmount()
    {
        return decimal.Parse(document.Root!.Element("amount")!.Value);
    }

    public string GetCurrency()
    {
        return document.Root!.Element("currency")!.Value;
    }

    public string GetStatus()
    {
        return document.Root!.Element("status")!.Value;
    }
}

// 6) Третій провайдер (інші назви методів + async статус)
public class StrangeProvider
{
    public decimal ReadMoney()
    {
        return 300.00m;
    }

    public string ReadCurrencyCode()
    {
        return "USD";
    }

    public async Task<string> ReadStateAsync()
    {
        await Task.Delay(10);
        return "done";
    }
}

// 7) Адаптер для третього провайдера
public class StrangePaymentAdapter : IPaymentInfo
{
    private readonly StrangeProvider provider;

    public StrangePaymentAdapter(StrangeProvider strangeProvider)
    {
        provider = strangeProvider;
    }

    public decimal GetAmount()
    {
        return provider.ReadMoney();
    }

    public string GetCurrency()
    {
        return provider.ReadCurrencyCode();
    }

    public string GetStatus()
    {
        string providerState = provider.ReadStateAsync().GetAwaiter().GetResult();

        if (providerState == "done") return "SUCCESS";
        if (providerState == "wait") return "PENDING";
        return "FAILED";
    }
}
