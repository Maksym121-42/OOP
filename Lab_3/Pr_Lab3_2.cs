using System;
using System.Collections.Generic;
using System.Linq;

public class Pr_Lab3_2
{
    public static void Main()
    {
        var order = new OrderRequest(new List<OrderItem>
        {
            new("Ноутбук", 30000m),
            new("Мишка", 800m)
        });

        var facade = new OrderFacade(
            new PricingService(),
            new DiscountService(),
            new InventoryService(),
            new PaymentGateway(),
            new ReservationService(),
            new DeliveryService());

        bool success = facade.PlaceOrder(order);
        Console.WriteLine($"Результат: {(success ? "успішно" : "помилка")}");
    }
}

public record OrderItem(string Name, decimal Price);

public class OrderRequest
{
    public List<OrderItem> Items { get; }

    public OrderRequest(List<OrderItem> items)
    {
        Items = items;
    }
}

public class PricingService
{
    public decimal CalculateTotal(OrderRequest order) =>
        order.Items.Sum(i => i.Price);
}

public class DiscountService
{
    public decimal ApplyDiscount(decimal total) =>
        total >= 20000m ? total * 0.95m : total;
}

public class InventoryService
{
    public bool CheckAvailability(OrderRequest order) =>
        order.Items.Count > 0;
}

public class PaymentGateway
{
    public bool Pay(decimal amount)
    {
        Console.WriteLine($"Оплата: {amount} грн");
        return true;
    }
}

public class ReservationService
{
    public void Reserve(OrderRequest order) =>
        Console.WriteLine("Товар заброньовано");
}

public class DeliveryService
{
    public void Arrange(OrderRequest order) =>
        Console.WriteLine("Доставку створено");
}

public class OrderFacade
{
    private readonly PricingService _pricingService;
    private readonly DiscountService _discountService;
    private readonly InventoryService _inventoryService;
    private readonly PaymentGateway _paymentGateway;
    private readonly ReservationService _reservationService;
    private readonly DeliveryService _deliveryService;

    public OrderFacade(
        PricingService pricingService,
        DiscountService discountService,
        InventoryService inventoryService,
        PaymentGateway paymentGateway,
        ReservationService reservationService,
        DeliveryService deliveryService)
    {
        _pricingService = pricingService;
        _discountService = discountService;
        _inventoryService = inventoryService;
        _paymentGateway = paymentGateway;
        _reservationService = reservationService;
        _deliveryService = deliveryService;
    }

    public bool PlaceOrder(OrderRequest order)
    {
        Console.WriteLine("=== Оформлення замовлення ===");

        decimal total = _pricingService.CalculateTotal(order);
        Console.WriteLine($"Сума без знижки: {total} грн");

        total = _discountService.ApplyDiscount(total);
        Console.WriteLine($"Сума зі знижкою: {total} грн");

        if (!_inventoryService.CheckAvailability(order))
        {
            Console.WriteLine("Помилка: товар недоступний");
            return false;
        }

        if (!_paymentGateway.Pay(total))
        {
            Console.WriteLine("Помилка: оплата не пройшла");
            return false;
        }

        _reservationService.Reserve(order);
        _deliveryService.Arrange(order);

        Console.WriteLine("Замовлення оформлено");
        return true;
    }
}
