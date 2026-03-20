

// Інтерфейси елементів
interface IButton
{
    void Render();
}

interface ITextField
{
    void Render();
}

interface IDropdown
{
    void Render();
}

// Темні елементи
class DarkButton : IButton
{
    public void Render() => Console.WriteLine("Темна кнопка");
}

class DarkTextField : ITextField
{
    public void Render() => Console.WriteLine("Темне текстове поле");
}

class DarkDropdown : IDropdown
{
    public void Render() => Console.WriteLine("Темний випадаючий список");
}

// Світлі елементи
class LightButton : IButton
{
    public void Render() => Console.WriteLine("Світла кнопка");
}

class LightTextField : ITextField
{
    public void Render() => Console.WriteLine("Світле текстове поле");
}

class LightDropdown : IDropdown
{
    public void Render() => Console.WriteLine("Світлий випадаючий список");
}

//  Абстрактна фабрика
interface IThemeFactory
{
    IButton CreateButton();
    ITextField CreateTextField();
    IDropdown CreateDropdown();
}


class DarkThemeFactory : IThemeFactory
{
    public IButton CreateButton() => new DarkButton();
    public ITextField CreateTextField() => new DarkTextField();
    public IDropdown CreateDropdown() => new DarkDropdown();
}

class LightThemeFactory : IThemeFactory
{
    public IButton CreateButton() => new LightButton();
    public ITextField CreateTextField() => new LightTextField();
    public IDropdown CreateDropdown() => new LightDropdown();
}


class Pr_Lab2_2
{
    static void Main()
    {
        
        Console.WriteLine("Оберіть тему: 1 - Темна, 2 - Світла");
        string choice = Console.ReadLine();

        IThemeFactory factory;

        if (choice == "1")
            factory = new DarkThemeFactory();
        else
            factory = new LightThemeFactory();

        IButton button = factory.CreateButton();
        ITextField textField = factory.CreateTextField();
        IDropdown dropdown = factory.CreateDropdown();

        Console.WriteLine("\nЕлементи інтерфейсу:");
        button.Render();
        textField.Render();
        dropdown.Render();

        Console.WriteLine("\nУсі елементи створені однією фабрикою -> стиль цілісний.");
    }
}
