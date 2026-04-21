using System;
using System.Collections.Generic;

public interface IModule
{
    string Name { get; }
    decimal GetLicensePrice();
    void Enable();
    void Disable();
    void PrintTree(string indent = "");
}

public class BasicModule : IModule // Leaf
{
    public string Name { get; }
    private readonly decimal _price;
    private bool _enabled = true;

    public BasicModule(string name, decimal price)
    {
        Name = name;
        _price = price;
    }

    public decimal GetLicensePrice() => _enabled ? _price : 0;

    public void Enable() => _enabled = true;
    public void Disable() => _enabled = false;

    public void PrintTree(string indent = "")
    {
        Console.WriteLine($"{indent}- {Name} [{(_enabled ? "ON" : "OFF")}] {_price} грн");
    }
}

public class ModulePack : IModule // Composite
{
    public string Name { get; }
    private readonly List<IModule> _items = new();
    private bool _enabled = true;

    public ModulePack(string name) => Name = name;

    public void Add(IModule module) => _items.Add(module);

    public decimal GetLicensePrice()
    {
        if (!_enabled) return 0;

        decimal total = 0;
        foreach (var item in _items) total += item.GetLicensePrice();
        return total;
    }

    public void Enable()
    {
        _enabled = true;
        foreach (var item in _items) item.Enable();
    }

    public void Disable()
    {
        _enabled = false;
        foreach (var item in _items) item.Disable();
    }

    public void PrintTree(string indent = "")
    {
        Console.WriteLine($"{indent}+ {Name} [{(_enabled ? "ON" : "OFF")}]");
        foreach (var item in _items) item.PrintTree(indent + "  ");
    }
}

class Pr_Lab3_4
{
    static void Main()
    {
        var core = new BasicModule("Базове ядро", 5000);
        var analytics = new BasicModule("Аналітика", 1800);
        var apiExt = new BasicModule("API-розширення", 1200);

        var integrations = new ModulePack("Зовнішні інтеграції");
        integrations.Add(new BasicModule("Пошта", 400));
        integrations.Add(new BasicModule("Платіжний шлюз", 700));

        var crm = new ModulePack("CRM-система");
        crm.Add(core);
        crm.Add(analytics);
        crm.Add(integrations);
        crm.Add(apiExt);

        crm.PrintTree();
        Console.WriteLine($"Загальна вартість: {crm.GetLicensePrice()} грн");

        Console.WriteLine("\nВимикаємо підсистему 'Зовнішні інтеграції'");
        integrations.Disable();

        crm.PrintTree();
        Console.WriteLine($"Загальна вартість: {crm.GetLicensePrice()} грн");
    }
}
