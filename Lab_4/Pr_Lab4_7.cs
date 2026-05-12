using System;
using System.Collections.Generic;

/*
    Контроль дій у документообігу (Command Pattern)
    Кожна операція представлена окремою командою: її можна виконати, залогувати та скасувати.
*/

public interface IUserAction
{
    void Run();
    void Rollback();
}

public class WorkflowStorage
{
    public void Add(string title)
    {
        Console.WriteLine($"[ADD] Створено файл: \"{title}\"");
    }

    public void Remove(string title)
    {
        Console.WriteLine($"[DEL] Видалено файл: \"{title}\"");
    }
}

public class AddFileAction : IUserAction
{
    private readonly WorkflowStorage _storage;
    private readonly string _fileTitle;

    public AddFileAction(WorkflowStorage storage, string fileTitle)
    {
        _storage = storage;
        _fileTitle = fileTitle;
    }

    public void Run()
    {
        _storage.Add(_fileTitle);
    }

    public void Rollback()
    {
        _storage.Remove(_fileTitle);
    }
}

public class RemoveFileAction : IUserAction
{
    private readonly WorkflowStorage _storage;
    private readonly string _fileTitle;

    public RemoveFileAction(WorkflowStorage storage, string fileTitle)
    {
        _storage = storage;
        _fileTitle = fileTitle;
    }

    public void Run()
    {
        _storage.Remove(_fileTitle);
    }

    public void Rollback()
    {
        _storage.Add(_fileTitle);
    }
}

public class ActionDispatcher
{
    private readonly Stack<IUserAction> _actionLog = new();

    public void Process(IUserAction action)
    {
        action.Run();
        _actionLog.Push(action);
    }

    public void UndoLast()
    {
        if (_actionLog.Count == 0)
        {
            Console.WriteLine("[UNDO] Історія порожня, скасовувати нічого.");
            return;
        }

        var lastAction = _actionLog.Pop();
        Console.WriteLine("[UNDO] Скасування останньої операції...");
        lastAction.Rollback();
    }
}

public class Program
{
    public static void Main()
    {
        var storage = new WorkflowStorage();
        var dispatcher = new ActionDispatcher();

        dispatcher.Process(new AddFileAction(storage, "Наказ_2026.pdf"));
        dispatcher.Process(new RemoveFileAction(storage, "Чернетка_договору.docx"));

        Console.WriteLine();
        dispatcher.UndoLast();
    }
}
