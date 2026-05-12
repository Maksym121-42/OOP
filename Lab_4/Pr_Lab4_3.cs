/*
    Завдання 3. Режими робота-прибирача (State Pattern) - патерн Стан.
    Поведінка винесена в окремі стани, а контекст просто делегує виконання
    поточного режиму без великої кількості умов if/else.
*/

using System;

public interface ICleanerMode
{
    void Execute(SmartCleaner cleaner);
}

public sealed class CleaningMode : ICleanerMode
{
    public void Execute(SmartCleaner cleaner)
    {
        Console.WriteLine("Режим ПРИБИРАННЯ: пилосос активно чистить підлогу.");
    }
}

public sealed class DockingMode : ICleanerMode
{
    public void Execute(SmartCleaner cleaner)
    {
        Console.WriteLine("Режим ПОВЕРНЕННЯ: заряду мало, рух до док-станції.");
    }
}

public sealed class AlertMode : ICleanerMode
{
    public void Execute(SmartCleaner cleaner)
    {
        Console.WriteLine("Режим ТРИВОГИ: робот заблокований, подає звуковий сигнал.");
    }
}

public class SmartCleaner
{
    private ICleanerMode _currentMode;

    public SmartCleaner(ICleanerMode startMode)
    {
        _currentMode = startMode;
    }

    public void ChangeMode(ICleanerMode nextMode)
    {
        _currentMode = nextMode;
        Console.WriteLine("[Контролер]: внутрішній режим успішно оновлено.");
    }

    public void PerformStep()
    {
        _currentMode.Execute(this);
    }
}

public static class Program
{
    public static void Main()
    {
        var cleanerBot = new SmartCleaner(new CleaningMode());

        cleanerBot.PerformStep();

        cleanerBot.ChangeMode(new DockingMode());
        cleanerBot.PerformStep();

        cleanerBot.ChangeMode(new AlertMode());
        cleanerBot.PerformStep();
    }
}