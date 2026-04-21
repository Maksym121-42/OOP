using System.Collections.Concurrent;
using System.Linq;

// Demo
var world = new GameWorld();
var rnd = new Random();

string[] types = { "oak", "pine", "granite" }; // oak=дуб, pine=сосна, granite=граніт
const int totalObjects = 1_000_000;

for (int i = 0; i < totalObjects; i++)
{
    string type = types[rnd.Next(types.Length)];
    int x = rnd.Next(0, 5000);
    int y = rnd.Next(0, 5000);
    bool isDestroyed = rnd.Next(0, 100) < 10; // 10% зрубані/зламані

    world.AddObject(type, x, y, isDestroyed);
}

Console.WriteLine($"Total world objects: {world.ObjectCount}");
Console.WriteLine($"Shared type objects (flyweights): {world.SharedTypeCount}");
Console.WriteLine($"Heavy data reused for {world.ObjectCount - world.SharedTypeCount} objects");
Console.WriteLine();
world.PrintSample(5);


public sealed class ObjectTypeData
{
    public string TypeName { get; }
    public string Texture { get; }
    public string Model { get; }
    public int Durability { get; }
    public bool CanInteract { get; }

    public ObjectTypeData(string typeName, string texture, string model, int durability, bool canInteract)
    {
        TypeName = typeName;
        Texture = texture;
        Model = model;
        Durability = durability;
        CanInteract = canInteract;
    }
}


public sealed class WorldObject
{
    private readonly ObjectTypeData _sharedData;

    public int X { get; }
    public int Y { get; }
    public bool IsDestroyed { get; }

    public WorldObject(ObjectTypeData sharedData, int x, int y, bool isDestroyed)
    {
        _sharedData = sharedData;
        X = x;
        Y = y;
        IsDestroyed = isDestroyed;
    }

    public string Describe()
    {
        return $"Type={_sharedData.TypeName}, Pos=({X},{Y}), Destroyed={IsDestroyed}, Texture={_sharedData.Texture}";
    }
}


public sealed class ObjectTypeFactory
{
    private readonly ConcurrentDictionary<string, ObjectTypeData> _cache = new();

    public ObjectTypeData GetOrCreate(string typeName)
    {
        return _cache.GetOrAdd(typeName, CreateTypeDataByType);
    }

    public int Count => _cache.Count;

    private static ObjectTypeData CreateTypeDataByType(string typeName)
    {
        return typeName switch
        {
            "oak" => new ObjectTypeData("oak", "oak_texture.png", "oak_model.obj", durability: 80, canInteract: true),
            "pine" => new ObjectTypeData("pine", "pine_texture.png", "pine_model.obj", durability: 70, canInteract: true),
            "granite" => new ObjectTypeData("granite", "granite_texture.png", "granite_model.obj", durability: 200, canInteract: false),
            _ => new ObjectTypeData("unknown", "default_texture.png", "default_model.obj", durability: 50, canInteract: false)
        };
    }
}


public sealed class GameWorld
{
    private readonly ObjectTypeFactory _factory = new();
    private readonly List<WorldObject> _objects = new();

    public void AddObject(string typeName, int x, int y, bool isDestroyed = false)
    {
        var sharedData = _factory.GetOrCreate(typeName);
        _objects.Add(new WorldObject(sharedData, x, y, isDestroyed));
    }

    public int ObjectCount => _objects.Count;
    public int SharedTypeCount => _factory.Count;

    public void PrintSample(int count)
    {
        foreach (var obj in _objects.Take(count))
            Console.WriteLine(obj.Describe());
    }
}