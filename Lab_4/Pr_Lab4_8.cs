/*
    Лабораторна робота: Поведінкові патерни - Ітератор
    Тема: Уніфікований обхід співробітників з різних сховищ (Iterator)
*/

using System;
using System.Collections.Generic;

public class StaffMember
{
    public string FullName { get; set; }
    public string Position { get; set; }
    public bool IsInProjectNow { get; set; }
}

public interface IStaffCursor
{
    bool CanMoveNext();
    StaffMember MoveNext();
}

public interface IStaffSource
{
    IStaffCursor OpenCursor();
}

public class TeamRoster : IStaffSource
{
    private readonly List<StaffMember> _staff = new();

    public void Add(StaffMember member)
    {
        _staff.Add(member);
    }

    public IStaffCursor OpenCursor()
    {
        return new TeamRosterCursor(_staff);
    }
}

public class TeamRosterCursor : IStaffCursor
{
    private readonly List<StaffMember> _items;
    private int _index;

    public TeamRosterCursor(List<StaffMember> items)
    {
        _items = items;
        _index = 0;
    }

    public bool CanMoveNext()
    {
        return _index < _items.Count;
    }

    public StaffMember MoveNext()
    {
        if (!CanMoveNext())
        {
            return null;
        }

        StaffMember current = _items[_index];
        _index++;
        return current;
    }
}

public static class Program
{
    public static void Main()
    {
        TeamRoster productTeam = new();

        productTeam.Add(new StaffMember
        {
            FullName = "Нікіта",
            Position = "Розробник",
            IsInProjectNow = true
        });
        productTeam.Add(new StaffMember
        {
            FullName = "Владислав",
            Position = "Тестувальник",
            IsInProjectNow = true
        });
        productTeam.Add(new StaffMember
        {
            FullName = "Михайло",
            Position = "Менеджер",
            IsInProjectNow = false
        });

        IStaffCursor staffCursor = productTeam.OpenCursor();

        Console.WriteLine("HRM ЗВІТ: учасники відділу (обхід через єдиний курсор)");
        while (staffCursor.CanMoveNext())
        {
            StaffMember member = staffCursor.MoveNext();
            string projectTag = member.IsInProjectNow ? "активний у проєкті" : "не залучений до проєкту";
            Console.WriteLine($"• {member.FullName} | {member.Position} | {projectTag}");
        }
    }
}
