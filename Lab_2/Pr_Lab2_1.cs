using System;
using System.Text;

class Character
{
    public string Name;
    public string Gender;
    public string ClassType;   // воїн/маг
    public int Strength;
    public int Intelligence;
    public int Agility;
    public string EyeColor;
    public string Hairstyle;
    public string Weapon;      // меч/кинджал/лук

    public void Show()
    {
        Console.WriteLine("\n=== Персонаж ===");
        Console.WriteLine("Ім'я: " + Name);
        Console.WriteLine("Стать: " + Gender);
        Console.WriteLine("Клас: " + ClassType);
        Console.WriteLine("Сила: " + Strength);
        Console.WriteLine("Інтелект: " + Intelligence);
        Console.WriteLine("Спритність: " + Agility);
        Console.WriteLine("Колір очей: " + EyeColor);
        Console.WriteLine("Зачіска: " + Hairstyle);
        Console.WriteLine("Зброя: " + Weapon);
    }
}

class CharacterBuilder
{
    private Character character = new Character();

        
    public CharacterBuilder SetName(string name) { character.Name = name; return this; }
    public CharacterBuilder SetGender(string gender) { character.Gender = gender; return this; }
    public CharacterBuilder SetClassType(string classType) { character.ClassType = classType; return this; }
    public CharacterBuilder SetStats(int strength, int intelligence, int agility)
    {
        character.Strength = strength;
        character.Intelligence = intelligence;
        character.Agility = agility;
        return this;
    }
    public CharacterBuilder SetEyeColor(string eyeColor) { character.EyeColor = eyeColor; return this; }
    public CharacterBuilder SetHairstyle(string hairstyle) { character.Hairstyle = hairstyle; return this; }
    public CharacterBuilder SetWeapon(string weapon) { character.Weapon = weapon; return this; }

    public Character Build() { return character; }
}

class CharacterDirector
{
    public Character CreateStandardFighter(string name)
    {
        return new CharacterBuilder()
            .SetName(name)
            .SetGender("чоловік")
            .SetClassType("воїн")
            .SetStats(8, 2, 5)
            .SetEyeColor("карі")
            .SetHairstyle("коротка")
            .SetWeapon("меч")
            .Build();
    }
}

class Pr_Lab2_1
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.WriteLine("Оберіть режим:");
        Console.WriteLine("1 - Стандартний боєць");
        Console.WriteLine("2 - Налаштувати вручну");
        Console.Write("Ваш вибір: ");
        string mode = Console.ReadLine();

        Character result;

        if (mode == "1")
        {
            Console.Write("Введіть ім'я: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = "Герой";

            CharacterDirector director = new CharacterDirector();
            result = director.CreateStandardFighter(name);
        }
        else
        {
            Console.Write("Ім'я: ");
            string name = Console.ReadLine();

            Console.Write("Стать (чоловік/жінка): ");
            string gender = Console.ReadLine();

            Console.Write("Клас (воїн/маг): ");
            string classType = Console.ReadLine();

            Console.Write("Сила: ");
            int str = int.Parse(Console.ReadLine());

            Console.Write("Інтелект: ");
            int intel = int.Parse(Console.ReadLine());

            Console.Write("Спритність: ");
            int agi = int.Parse(Console.ReadLine());

            Console.Write("Колір очей: ");
            string eyes = Console.ReadLine();

            Console.Write("Зачіска: ");
            string hair = Console.ReadLine();

            Console.Write("Зброя (меч/кинджал/лук): ");
            string weapon = Console.ReadLine();

            result = new CharacterBuilder()
                .SetName(name)
                .SetGender(gender)
                .SetClassType(classType)
                .SetStats(str, intel, agi)
                .SetEyeColor(eyes)
                .SetHairstyle(hair)
                .SetWeapon(weapon)
                .Build();
        }

        result.Show();

        Console.WriteLine("\nНатисніть Enter для виходу...");
        Console.ReadLine();
    }
}
