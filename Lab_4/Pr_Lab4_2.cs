

/*
    Завдання 2. Вибір способу оплати (Strategy Pattern)
    Рішення: використано патерн "Стратегія".
    Кошик не містить switch/if для способу оплати: він працює лише з ICheckoutPaymentStrategy
    і викликає єдиний метод Pay(...).
*/

public interface ICheckoutPaymentStrategy
{
    string MethodName { get; }
    void Pay(decimal orderTotal);
}

public class PayPalCheckoutStrategy : ICheckoutPaymentStrategy
{
    public string MethodName => "PayPal";

    public void Pay(decimal orderTotal)
    {
        Console.WriteLine($"[PayPal] Платіж на {orderTotal:F2} грн підтверджено.");
    }
}

public class BankCardCheckoutStrategy : ICheckoutPaymentStrategy
{
    public string MethodName => "Кредитна картка";

    public void Pay(decimal orderTotal)
    {
        Console.WriteLine($"[Картка] Списано {orderTotal:F2} грн. Транзакція виконана.");
    }
}

public class CryptoCheckoutStrategy : ICheckoutPaymentStrategy
{
    public string MethodName => "Криптовалюта";

    public void Pay(decimal orderTotal)
    {
        Console.WriteLine($"[Crypto] Переказ еквіваленту {orderTotal:F2} грн завершено.");
    }
}

public class OrderBasket
{
    private ICheckoutPaymentStrategy? _activePaymentStrategy;
    private decimal _cartTotalValue;

    public void AddItemPrice(decimal itemPrice) => _cartTotalValue += itemPrice;

    public void ChoosePaymentStrategy(ICheckoutPaymentStrategy selectedStrategy) => _activePaymentStrategy = selectedStrategy;

    public void PlaceOrder()
    {
        if (_activePaymentStrategy is null)
        {
            Console.WriteLine("Не вдалося оформити замовлення: спосіб оплати не обрано.");
            return;
        }

        Console.WriteLine("\n=== Оформлення замовлення ===");
        Console.WriteLine($"Сума до сплати: {_cartTotalValue:F2} грн");
        Console.WriteLine($"Обраний метод: {_activePaymentStrategy.MethodName}");
        _activePaymentStrategy.Pay(_cartTotalValue);
        Console.WriteLine("Статус: замовлення прийнято в обробку.");
    }
}

public static class CheckoutDemoApp
{
    public static void Main()
    {
        var customerCart = new OrderBasket();
        customerCart.AddItemPrice(1200.00m);
        customerCart.AddItemPrice(349.99m);

        customerCart.ChoosePaymentStrategy(new BankCardCheckoutStrategy());
        customerCart.PlaceOrder();

        customerCart.ChoosePaymentStrategy(new CryptoCheckoutStrategy());
        customerCart.PlaceOrder();
    }
}
