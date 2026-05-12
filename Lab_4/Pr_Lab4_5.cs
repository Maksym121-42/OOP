/*
    Завдання 5. Ланцюжок обробки звернень у сервісі онлайн-курсів (Chain of Responsibility)
    Рішення: Використано патерн "Ланцюжок обов'язків".
    Кожен учасник підтримки перевіряє, чи може опрацювати звернення.
    Якщо ні — передає його далі по ланцюжку.
*/

using System;

public class HelpTicket
{
    public string Topic { get; }
    public int Level { get; }

    public HelpTicket(string topic, int level)
    {
        Topic = topic;
        Level = level;
    }
}

public abstract class HelpDeskNode
{
    protected HelpDeskNode? NextNode;

    public void SetNext(HelpDeskNode nextNode)
    {
        NextNode = nextNode;
    }

    public abstract void Process(HelpTicket ticket);
}

public class AiAssistantNode : HelpDeskNode
{
    public override void Process(HelpTicket ticket)
    {
        if (ticket.Level == 1)
        {
            Console.WriteLine($"AI-помічник дав відповідь на тему: \"{ticket.Topic}\"");
            return;
        }

        Console.WriteLine("AI-помічник не впевнений у рішенні -> передаємо куратору курсу.");
        NextNode?.Process(ticket);
    }
}

public class CourseMentorNode : HelpDeskNode
{
    public override void Process(HelpTicket ticket)
    {
        if (ticket.Level == 2)
        {
            Console.WriteLine($"Куратор курсу вирішив звернення: \"{ticket.Topic}\"");
            return;
        }

        Console.WriteLine("Куратор не зміг вирішити питання -> ескалація до технічного інженера.");
        NextNode?.Process(ticket);
    }
}

public class PlatformEngineerNode : HelpDeskNode
{
    public override void Process(HelpTicket ticket)
    {
        Console.WriteLine($"Технічний інженер платформи закрив складний кейс: \"{ticket.Topic}\"");
    }
}

public class Program
{
    public static void Main()
    {
        HelpDeskNode aiAssistant = new AiAssistantNode();
        HelpDeskNode courseMentor = new CourseMentorNode();
        HelpDeskNode platformEngineer = new PlatformEngineerNode();

        aiAssistant.SetNext(courseMentor);
        courseMentor.SetNext(platformEngineer);

        Console.WriteLine("=== НОВЕ ЗВЕРНЕННЯ ДО ПІДТРИМКИ ===");
        aiAssistant.Process(new HelpTicket("Як відкрити домашнє завдання після дедлайну?", 1));

        Console.WriteLine("\n=== НОВЕ ЗВЕРНЕННЯ ДО ПІДТРИМКИ ===");
        aiAssistant.Process(new HelpTicket("Не зараховується прогрес по модулю", 2));

        Console.WriteLine("\n=== НОВЕ ЗВЕРНЕННЯ ДО ПІДТРИМКИ ===");
        aiAssistant.Process(new HelpTicket("Відеолекції не відтворюються на всіх браузерах", 3));
    }
}
