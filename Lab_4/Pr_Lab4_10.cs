/*
    Завдання 10. Аналіз фінансових документів (Visitor Pattern) - відвідувач
    Ідея: операції винесені в окремі "інспектори", а структуру документів не змінюється.
*/

using System;
using System.Collections.Generic;

public interface IAuditVisitor
{
    void Visit(Bill bill);
    void Visit(Agreement agreement);
}

public interface IFinancePaper
{
    void Accept(IAuditVisitor visitor);
}

public class Bill : IFinancePaper
{
    public decimal AmountUah { get; set; }

    public void Accept(IAuditVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class Agreement : IFinancePaper
{
    public string Counterparty { get; set; } = string.Empty;

    public void Accept(IAuditVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class ComplianceAuditVisitor : IAuditVisitor
{
    public void Visit(Bill bill)
    {
        Console.WriteLine($"[Рахунок] Податковий контроль: сума {bill.AmountUah:N2} грн.");
    }

    public void Visit(Agreement agreement)
    {
        Console.WriteLine($"[Договір] Юридичний аудит контрагента: {agreement.Counterparty}.");
    }
}

public static class Program
{
    public static void Main()
    {
        List<IFinancePaper> papers =
        [
            new Bill { AmountUah = 25340.50m },
            new Agreement { Counterparty = "ТОВ Альфа Консалт" }
        ];

        IAuditVisitor audit = new ComplianceAuditVisitor();

        Console.WriteLine("=== Старт фінансового аудиту документів ===");
        foreach (var paper in papers)
        {
            paper.Accept(audit);
        }
        Console.WriteLine("=== Аудит завершено ===");
    }
}
