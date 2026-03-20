using System;
using System.IO;

class Pr_Lab2_3
{
    static void Main()
    {
        var moduleA = new PaymentModule();
        var moduleB = new UserModule();
        var moduleC = new ReportModule();

        moduleA.DoWork();
        moduleB.DoWork();
        moduleC.DoWork();

        // Перевірка, чи той самий об'єкт
        bool sameInstance = Object.ReferenceEquals(LoggerService.Instance, LoggerService.Instance);
        Console.WriteLine($"\nОдин і той самий Logger? {sameInstance}");
        Console.WriteLine("Логи записані у файл errors.log");
    }
}

class LoggerService
{
    private static LoggerService _instance;

  
    private LoggerService()
    {
        Console.WriteLine("Підключення до сервера логів.");
    }

    public static LoggerService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LoggerService();
            }
            return _instance;
        }
    }

    public void LogError(string moduleName, string errorText)
    {
        string line = $"[{DateTime.Now:HH:mm:ss}] {moduleName}: {errorText}";
        Console.WriteLine("LOG -> " + line);

        // Запис в один файл
        File.AppendAllText("errors.log", line + Environment.NewLine);
    }
}

class PaymentModule
{
    public void DoWork()
    {
        LoggerService.Instance.LogError("PaymentModule", "Помилка оплати");
    }
}

class UserModule
{
    public void DoWork()
    {
        LoggerService.Instance.LogError("UserModule", "Помилка авторизації");
    }
}

class ReportModule
{
    public void DoWork()
    {
        LoggerService.Instance.LogError("ReportModule", "Помилка формування звіту");
    }
}