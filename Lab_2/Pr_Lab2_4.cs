
using System.Diagnostics;


namespace Lab2PrototypeDemo
{
    // Інтерфейс прототипу
    public interface IOrcPrototype
    {
        Orc Clone();
    }

    // Клас Orc
    public class Orc : IOrcPrototype
    {
        // Однакові для всіх 
        public string Texture { get; set; }
        public string Effects { get; set; }
        public string MoveType { get; set; }

        // Різні для кожного орка
        public int X { get; set; }
        public int Y { get; set; }
        public string Weapon { get; set; }
        public int Level { get; set; }
        public int Strength { get; set; }
        
        public Orc()
        {
            Console.WriteLine("Створення базового орка");

            Texture = "orc_texture_v1";
            Effects = "dust, blood";
            MoveType = "walk";
        }

        // Приватний конструктор копіювання 
        private Orc(Orc source)
        {
            Texture = source.Texture;
            Effects = source.Effects;
            MoveType = source.MoveType;

            X = source.X;
            Y = source.Y;
            Weapon = source.Weapon;
            Level = source.Level;
            Strength = source.Strength;
        }

        // Клонування
        public Orc Clone()
        {
            return new Orc(this);
        }

        public void Print()
        {
            Console.WriteLine(
                $"Pos=({X},{Y}), Weapon={Weapon}, Level={Level}, Strength={Strength}, " +
                $"Texture={Texture}, Effects={Effects}, Move={MoveType}");
        }
    }

    class Pr_Lab2_4
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Створили об'єкт 1 раз
            var prototype = new Orc();

            var army = new List<Orc>();
            var sw = Stopwatch.StartNew();

            // Швидке створення орків через клон
            for (int i = 1; i <= 1000; i++)
            {
                var orc = prototype.Clone();

                // Унікальні параметри
                orc.X = i * 10;
                orc.Y = i * 3;
                orc.Weapon = (i % 2 == 0) ? "Spear" : "Axe";
                orc.Level = i;
                orc.Strength = 10 + i * 2;

                army.Add(orc);
            }

            sw.Stop();

            Console.WriteLine("\nСтворені орки:");
            foreach (var orc in army)
            {
                orc.Print();
            }

            Console.WriteLine($"\nЧас створення 1000 орків через Prototype: {sw.ElapsedMilliseconds} мс");
        }
    }
}
