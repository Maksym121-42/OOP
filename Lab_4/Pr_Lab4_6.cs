using System;

/*
    Поведінкові патерни (Template Method) - шаблонний метод
    Загальний сценарій проходження модуля винесений у базовий клас.
    Відмінності між типами курсів реалізовано у перевизначуваному кроці.
*/

public abstract class LearningPathTemplate
{
    public void RunStudyFlow()
    {
        OpenModule();
        StudyTheory();
        CompletePractice();
        PerformUniqueStage();
        PassAssessment();
        DefendCapstone();
    }

    private void OpenModule()
    {
        Console.WriteLine("1) Платформа відкриває навчальний модуль.");
    }

    private void StudyTheory()
    {
        Console.WriteLine("2) Студент опрацьовує теоретичні матеріали.");
    }

    private void CompletePractice()
    {
        Console.WriteLine("3) Студент виконує серію практичних вправ.");
    }

    private void PassAssessment()
    {
        Console.WriteLine("5) Студент проходить підсумкове тестування.");
    }

    private void DefendCapstone()
    {
        Console.WriteLine("6) Студент презентує фінальний результат.");
    }

    protected abstract void PerformUniqueStage();
}

public sealed class CodingTrack : LearningPathTemplate
{
    protected override void PerformUniqueStage()
    {
        Console.WriteLine("4) Додатковий етап: автоматичний аналіз та перевірка коду.");
    }
}

public sealed class DesignTrack : LearningPathTemplate
{
    protected override void PerformUniqueStage()
    {
        Console.WriteLine("4) Додатковий етап: пітч макета перед викладачем у live-сесії.");
    }
}

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("=== Трек: Програмування ===");
        LearningPathTemplate codingScenario = new CodingTrack();
        codingScenario.RunStudyFlow();

        Console.WriteLine();

        Console.WriteLine("=== Трек: Дизайн ===");
        LearningPathTemplate designScenario = new DesignTrack();
        designScenario.RunStudyFlow();
    }
}
