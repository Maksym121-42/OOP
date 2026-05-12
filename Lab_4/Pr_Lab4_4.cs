using System;
using System.Collections.Generic;
using System.Text;

/*
    Ctrl+Z у текстовому редакторі (Memento)
    Ідея: редактор зберігає свій стан у знімок, а стек історії
    повертає попередню версію без відкриття внутрішніх полів об'єкта.
*/

public class EditorSnapshot
{
    public string Content { get; }
    public string InkColor { get; }

    public EditorSnapshot(string content, string inkColor)
    {
        Content = content;
        InkColor = inkColor;
    }
}

public class DraftEditor
{
    private string _content;
    private string _inkColor;

    public DraftEditor()
    {
        _content = string.Empty;
        _inkColor = "Сірий";
    }

    public void AddFragment(string fragment)
    {
        _content += fragment;
        ShowState("Додано фрагмент");
    }

    public void ReplaceColor(string color)
    {
        _inkColor = color;
        ShowState("Оновлено колір");
    }

    public EditorSnapshot CaptureState()
    {
        return new EditorSnapshot(_content, _inkColor);
    }

    public void Rollback(EditorSnapshot snapshot)
    {
        if (snapshot == null)
        {
            return;
        }

        _content = snapshot.Content;
        _inkColor = snapshot.InkColor;
        ShowState("Повернення до попереднього стану");
    }

    private void ShowState(string action)
    {
        Console.WriteLine($"{action}: \"{_content}\" | Тон: {_inkColor}");
    }
}

public class UndoStack
{
    private readonly Stack<EditorSnapshot> _snapshots = new();

    public void Remember(DraftEditor editor)
    {
        _snapshots.Push(editor.CaptureState());
    }

    public void CtrlZ(DraftEditor editor)
    {
        if (_snapshots.Count == 0)
        {
            Console.WriteLine("Відкат неможливий: історія порожня.");
            return;
        }

        var previous = _snapshots.Pop();
        editor.Rollback(previous);
    }
}

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var writer = new DraftEditor();
        var timeline = new UndoStack();

        timeline.Remember(writer);
        writer.AddFragment("Прошу ");

        timeline.Remember(writer);
        writer.ReplaceColor("Зелений");
        writer.AddFragment("поставити 7 балів.");

        Console.WriteLine("\nНатиснуто Ctrl+Z");
        timeline.CtrlZ(writer);

        Console.WriteLine("\nНатиснуто Ctrl+Z");
        timeline.CtrlZ(writer);
    }
}
