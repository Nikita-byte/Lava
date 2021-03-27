using System.Collections.Generic;


public sealed class AssetsPath
{
    public static readonly Dictionary<GameObjectType, string> Path = new Dictionary<GameObjectType, string>()
    {
        { GameObjectType.Preferences, "Data/Preferences" },
        { GameObjectType.Canvas, "Canvas"},
        { GameObjectType.Bullet, "Bullet"},
    };
}